using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Playaway_Custom_Print_Buddy.Models;
using Playaway_Custom_Print_Buddy.Services;
using Playaway_Custom_Print_Buddy.Utilities;

namespace Playaway_Custom_Print_Buddy.Forms
{
    public partial class MainForm : Form
    {
        private List<JobItem> _jobs;
        private ProductType _currentProductType;
        private ProcessingMode _currentProcessingMode;
        private string _connectionString;
        private PDFProcessorService _pdfProcessor;

        public MainForm()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            _jobs = new List<JobItem>();
            _currentProductType = ProductType.Playaway;
            _currentProcessingMode = ProcessingMode.LP;
            
            // Initialize connection string
            _connectionString = "Server=PASQL16;Database=dbProduction;Trusted_Connection=true;";
            
            // Initialize PDF processor
            _pdfProcessor = new PDFProcessorService();
            
            // Update UI state
            UpdateUIForProductType();
        }

        private void ProductTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                if (rb == radioButtonPlayaway)
                    _currentProductType = ProductType.Playaway;
                else if (rb == radioButtonLaunchpad)
                    _currentProductType = ProductType.Launchpad;
                else if (rb == radioButtonWhazoodle)
                    _currentProductType = ProductType.Whazoodle;

                UpdateUIForProductType();
            }
        }

        private void UpdateUIForProductType()
        {
            // Update processing mode based on product type
            if (_currentProductType == ProductType.Whazoodle)
            {
                _currentProcessingMode = ProcessingMode.WZ;
            }
            else
            {
                _currentProcessingMode = ProcessingMode.LP;
            }

            // Update status
            toolStripStatusLabel1.Text = $"Ready - {_currentProductType} Mode";
        }

        private async void buttonProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSKU.Text))
            {
                MessageBox.Show("Please enter a SKU to process.", "Input Required", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await ProcessSingleSKU(textBoxSKU.Text.Trim());
        }

        private async void buttonBatchProcess_Click(object sender, EventArgs e)
        {
            var selectedJobs = _jobs.Where(j => j.IsSelected).ToList();
            if (selectedJobs.Count == 0)
            {
                MessageBox.Show("Please select jobs to process.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await ProcessBatchJobs(selectedJobs);
        }

        private async Task ProcessSingleSKU(string sku)
        {
            try
            {
                buttonProcess.Enabled = false;
                progressBar1.Value = 0;
                toolStripStatusLabel1.Text = $"Processing {sku}...";

                // Create job item
                var jobItem = new JobItem
                {
                    SKU = sku,
                    ProductType = _currentProductType,
                    ProcessingMode = _currentProcessingMode,
                    Status = "Processing"
                };

                // Add to jobs list
                _jobs.Add(jobItem);
                UpdateJobsList();

                // Process based on product type
                bool success = false;
                switch (_currentProductType)
                {
                    case ProductType.Playaway:
                        success = await ProcessPlayawayJob(jobItem);
                        break;
                    case ProductType.Launchpad:
                    case ProductType.Whazoodle:
                        success = await ProcessLaunchpadJob(jobItem);
                        break;
                }

                // Update job status
                jobItem.Status = success ? "Completed" : "Failed";
                UpdateJobsList();

                progressBar1.Value = 100;
                toolStripStatusLabel1.Text = success ? 
                    $"Successfully processed {sku}" : 
                    $"Failed to process {sku}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing SKU: {ex.Message}", "Processing Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Processing failed";
            }
            finally
            {
                buttonProcess.Enabled = true;
            }
        }

        private async Task ProcessBatchJobs(List<JobItem> jobs)
        {
            try
            {
                buttonBatchProcess.Enabled = false;
                progressBar1.Maximum = jobs.Count;
                progressBar1.Value = 0;

                for (int i = 0; i < jobs.Count; i++)
                {
                    var job = jobs[i];
                    job.Status = "Processing";
                    UpdateJobsList();

                    toolStripStatusLabel1.Text = $"Processing {job.SKU} ({i + 1}/{jobs.Count})";

                    bool success = false;
                    switch (job.ProductType)
                    {
                        case ProductType.Playaway:
                            success = await ProcessPlayawayJob(job);
                            break;
                        case ProductType.Launchpad:
                        case ProductType.Whazoodle:
                            success = await ProcessLaunchpadJob(job);
                            break;
                    }

                    job.Status = success ? "Completed" : "Failed";
                    progressBar1.Value = i + 1;
                    UpdateJobsList();

                    // Small delay to show progress
                    await Task.Delay(100);
                }

                toolStripStatusLabel1.Text = $"Batch processing completed ({jobs.Count} items)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in batch processing: {ex.Message}", "Batch Processing Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonBatchProcess.Enabled = true;
            }
        }

        private async Task<bool> ProcessPlayawayJob(JobItem job)
        {
            try
            {
                // Get data from database
                var skuData = await GetSKUDataFromDatabase(job.SKU);
                if (skuData == null)
                {
                    job.Notes = "SKU not found in database";
                    return false;
                }

                // Update job with retrieved data
                job.Title = skuData.Title;
                job.Author = skuData.Author;
                job.ISBN = skuData.ISBN;
                job.UPC = skuData.UPC;

                // Process PDF for Playaway
                var pdfPath = await _pdfProcessor.ProcessPlayawayPDF(job, skuData);
                if (!string.IsNullOrEmpty(pdfPath))
                {
                    job.FilePath = pdfPath;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                job.Notes = $"Error: {ex.Message}";
                return false;
            }
        }

        private async Task<bool> ProcessLaunchpadJob(JobItem job)
        {
            try
            {
                // Get data from database
                var skuData = await GetSKUDataFromDatabase(job.SKU);
                if (skuData == null)
                {
                    job.Notes = "SKU not found in database";
                    return false;
                }

                // Update job with retrieved data
                job.Title = skuData.Title;
                job.Author = skuData.Author;
                job.ISBN = skuData.ISBN;
                job.UPC = skuData.UPC;

                // Process PDF for Launchpad/Whazoodle
                var pdfPath = await _pdfProcessor.ProcessLaunchpadPDF(job, skuData);
                if (!string.IsNullOrEmpty(pdfPath))
                {
                    job.FilePath = pdfPath;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                job.Notes = $"Error: {ex.Message}";
                return false;
            }
        }

        private async Task<SKUData> GetSKUDataFromDatabase(string sku)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    string query = @"
                        SELECT TOP 1 
                            SKU, Title, Author, ISBN, UPC, 
                            Classification, Subtitle, Series,
                            Publisher, PubDate, Duration, 
                            AgeRange, Grade, Interest, Language
                        FROM ProductCatalog 
                        WHERE SKU = @sku";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sku", sku);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new SKUData
                                {
                                    SKU = reader["SKU"]?.ToString(),
                                    Title = reader["Title"]?.ToString(),
                                    Author = reader["Author"]?.ToString(),
                                    ISBN = reader["ISBN"]?.ToString(),
                                    UPC = reader["UPC"]?.ToString(),
                                    Classification = reader["Classification"]?.ToString(),
                                    Subtitle = reader["Subtitle"]?.ToString(),
                                    Series = reader["Series"]?.ToString(),
                                    Publisher = reader["Publisher"]?.ToString(),
                                    PubDate = reader["PubDate"]?.ToString(),
                                    Duration = reader["Duration"]?.ToString(),
                                    AgeRange = reader["AgeRange"]?.ToString(),
                                    Grade = reader["Grade"]?.ToString(),
                                    Interest = reader["Interest"]?.ToString(),
                                    Language = reader["Language"]?.ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private void UpdateJobsList()
        {
            listViewJobs.Items.Clear();
            
            foreach (var job in _jobs)
            {
                var item = new ListViewItem(job.SKU);
                item.SubItems.Add(job.Title ?? "");
                item.SubItems.Add(job.Status);
                item.SubItems.Add(job.ProductType.ToString());
                item.Tag = job;
                
                // Color code based on status
                switch (job.Status)
                {
                    case "Completed":
                        item.BackColor = Color.LightGreen;
                        break;
                    case "Failed":
                        item.BackColor = Color.LightCoral;
                        break;
                    case "Processing":
                        item.BackColor = Color.LightYellow;
                        break;
                }
                
                listViewJobs.Items.Add(item);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement settings form
            MessageBox.Show("Settings functionality will be implemented here.", "Settings", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Playaway Custom Print Buddy\n\n" +
                "A unified PDF editor for Playaway and Launchpad products.\n" +
                "Combines functionality from:\n" +
                "- Playaway PDF Editor\n" +
                "- Launchpad PDF Editor\n\n" +
                "Version 1.0\n" +
                "© Findaway", 
                "About", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}