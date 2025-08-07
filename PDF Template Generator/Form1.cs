using PDF_Template_Generator.Models;
using PDF_Template_Generator.Services;

namespace PDF_Template_Generator
{
    public partial class Form1 : Form
    {
        private TemplateConfig _currentTemplate;
        private PositionPresetManager _presetManager;
        private PDFProcessor _pdfProcessor;
        private string _currentTemplateFilePath = string.Empty;
        private string _outputDirectory = string.Empty;

        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            _currentTemplate = new TemplateConfig();
            _presetManager = new PositionPresetManager();
            
            // Load settings
            LoadApplicationSettings();
            
            _pdfProcessor = new PDFProcessor(_outputDirectory);
            
            // Initialize UI
            InitializeDropdowns();
            cmbProductLine.SelectedIndex = 0; // Default to Playaway
            LoadTemplateToUI(_currentTemplate);
        }

        private void LoadApplicationSettings()
        {
            // Load output directory from settings
            _outputDirectory = Properties.Settings.Default.pdfEditedLocation;
            if (string.IsNullOrEmpty(_outputDirectory))
            {
                _outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PDF Templates");
                if (!Directory.Exists(_outputDirectory))
                    Directory.CreateDirectory(_outputDirectory);
            }
        }

        private void InitializeDropdowns()
        {
            // Barcode types
            cmbBarcodeType.Items.AddRange(new string[] {
                "CODABAR", "CODE11", "CODE128", "CODE39", "CODE93"
            });
            cmbBarcodeType.SelectedIndex = 0;

            // Rotation options
            cmbBarcodeRotation.Items.AddRange(new string[] {
                "0°", "90°", "180°", "270°"
            });
            cmbBarcodeRotation.SelectedIndex = 0;

            // Colors
            string[] colors = { "Black", "Red", "Blue", "Green" };
            cmbAddressColor.Items.AddRange(colors);
            cmbText1Color.Items.AddRange(colors);
            cmbAddressColor.SelectedIndex = 0;
            cmbText1Color.SelectedIndex = 0;

            // Set default values
            txtLogoScale.Text = "100";
            txtBarcodeScale.Text = "100";
            txtAddressSize.Text = "12";
            txtText1Size.Text = "12";
        }

        #region Event Handlers

        private void cmbProductLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductLine.SelectedItem != null)
            {
                var productLine = (ProductLine)Enum.Parse(typeof(ProductLine), cmbProductLine.SelectedItem.ToString() ?? "Playaway");
                _currentTemplate.ProductLine = productLine;
                
                // Update position presets based on product line
                UpdatePositionPresets();
            }
        }

        private void UpdatePositionPresets()
        {
            // Update barcode position presets
            cmbBarcodePosition.Items.Clear();
            cmbBarcodePosition.Items.Add("Custom");
            var barcodePresets = _presetManager.GetPresetsForProduct(_currentTemplate.ProductLine, "Barcode");
            foreach (var preset in barcodePresets)
            {
                cmbBarcodePosition.Items.Add(preset);
            }
            cmbBarcodePosition.SelectedIndex = 0;

            // Update logo position presets
            cmbLogoPosition.Items.Clear();
            cmbLogoPosition.Items.Add("Custom");
            var logoPresets = _presetManager.GetPresetsForProduct(_currentTemplate.ProductLine, "Logo");
            foreach (var preset in logoPresets)
            {
                cmbLogoPosition.Items.Add(preset);
            }
            cmbLogoPosition.SelectedIndex = 0;

            // Update address position presets
            cmbAddressPosition.Items.Clear();
            cmbAddressPosition.Items.Add("Custom");
            var addressPresets = _presetManager.GetPresetsForProduct(_currentTemplate.ProductLine, "Text");
            foreach (var preset in addressPresets)
            {
                cmbAddressPosition.Items.Add(preset);
            }
            cmbAddressPosition.SelectedIndex = 0;
        }

        private void cmbBarcodePosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBarcodePosition.SelectedItem is PositionPreset preset)
            {
                txtBarcodeX.Text = preset.X.ToString();
                txtBarcodeY.Text = preset.Y.ToString();
                txtBarcodeScale.Text = preset.Scale.ToString();
                cmbBarcodeRotation.SelectedIndex = preset.Rotation / 90;
            }
        }

        private void cmbLogoPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLogoPosition.SelectedItem is PositionPreset preset)
            {
                txtLogoX.Text = preset.X.ToString();
                txtLogoY.Text = preset.Y.ToString();
                txtLogoScale.Text = preset.Scale.ToString();
            }
        }

        private void cmbAddressPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAddressPosition.SelectedItem is PositionPreset preset)
            {
                txtAddressX.Text = preset.X.ToString();
                txtAddressY.Text = preset.Y.ToString();
                txtAddressSize.Text = preset.Scale.ToString();
            }
        }

        private void btnBrowseSourcePdf_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
            openFileDialog1.Title = "Select Source PDF";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSourcePdf.Text = openFileDialog1.FileName;
                _currentTemplate.SourcePdfPath = openFileDialog1.FileName;
            }
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
            openFileDialog2.Title = "Select Logo Image";
            
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                txtLogoPath.Text = openFileDialog2.FileName;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // Update template from UI
                UpdateTemplateFromUI();
                
                // Validate template
                if (string.IsNullOrEmpty(_currentTemplate.SourcePdfPath))
                {
                    MessageBox.Show("Please select a source PDF file.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate PDF
                string outputFileName = !string.IsNullOrEmpty(_currentTemplate.Name) 
                    ? _currentTemplate.Name + ".pdf" 
                    : Path.GetFileName(_currentTemplate.SourcePdfPath);

                _pdfProcessor.ProcessTemplate(_currentTemplate, outputFileName);
                
                MessageBox.Show($"PDF generated successfully!\nSaved to: {Path.Combine(_outputDirectory, outputFileName)}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            // Placeholder for preview functionality
            MessageBox.Show("Preview functionality will be implemented in a future version.", 
                "Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                LoadApplicationSettings();
                _pdfProcessor = new PDFProcessor(_outputDirectory);
            }
        }

        #endregion

        #region Menu Events

        private void newTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentTemplate = new TemplateConfig();
            _currentTemplateFilePath = string.Empty;
            LoadTemplateToUI(_currentTemplate);
            this.Text = "PDF Template Generator - New Template";
        }

        private void openTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Template Files (*.json)|*.json";
            saveFileDialog1.Title = "Open Template";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var template = TemplateConfig.LoadFromFile(saveFileDialog1.FileName);
                if (template != null)
                {
                    _currentTemplate = template;
                    _currentTemplateFilePath = saveFileDialog1.FileName;
                    LoadTemplateToUI(_currentTemplate);
                    this.Text = $"PDF Template Generator - {Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)}";
                }
                else
                {
                    MessageBox.Show("Failed to load template file.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentTemplateFilePath))
            {
                saveTemplateAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                SaveCurrentTemplate();
            }
        }

        private void saveTemplateAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTemplateFromUI();
            
            saveFileDialog2.Filter = "Template Files (*.json)|*.json";
            saveFileDialog2.Title = "Save Template As";
            
            // Suggest appropriate filename based on advanced features
            if (!string.IsNullOrEmpty(_currentTemplate.Name))
            {
                var baseName = _currentTemplate.Name;
                if (_currentTemplate.HasAdvancedFeatures())
                {
                    // Suggest "Advanced" suffix if not already present
                    if (!baseName.ToLower().Contains("advanced"))
                    {
                        baseName += "_Advanced";
                    }
                }
                saveFileDialog2.FileName = baseName + ".json";
            }
            
            // Show warning for advanced features before saving
            if (_currentTemplate.HasAdvancedFeatures())
            {
                var warning = _currentTemplate.GetLegacyCompatibilityWarning();
                MessageBox.Show(warning, "Advanced Features Detected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                _currentTemplateFilePath = saveFileDialog2.FileName;
                SaveCurrentTemplate();
                this.Text = $"PDF Template Generator - {Path.GetFileNameWithoutExtension(saveFileDialog2.FileName)}";
            }
        }

        private void importLegacyTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Legacy Template Files (*.*)|*.*";
            openFileDialog1.Title = "Import Legacy Template";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var template = TemplateConfig.ImportLegacyTemplate(openFileDialog1.FileName);
                if (template != null)
                {
                    _currentTemplate = template;
                    LoadTemplateToUI(_currentTemplate);
                    MessageBox.Show("Legacy template imported successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to import legacy template.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSettings_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PDF Template Generator v1.0\n\nUnified template system for Playaway, Launchpad, and WhaZoodle products.", 
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Helper Methods

        private void LoadTemplateToUI(TemplateConfig template)
        {
            // Basic info
            txtTemplateName.Text = template.Name;
            cmbProductLine.SelectedItem = template.ProductLine.ToString();
            txtSourcePdf.Text = template.SourcePdfPath;

            // Logo
            chkAddLogo.Checked = template.AddLogo;
            txtLogoPath.Text = template.LogoPath;
            txtLogoX.Text = template.LogoX.ToString();
            txtLogoY.Text = template.LogoY.ToString();
            txtLogoScale.Text = template.LogoScale.ToString();

            // Address
            txtAddress1.Text = template.Address1;
            txtAddress2.Text = template.Address2;
            txtAddress3.Text = template.Address3;
            txtAddressX.Text = template.AddressX.ToString();
            txtAddressY.Text = template.AddressY.ToString();
            txtAddressSize.Text = template.AddressSize.ToString();
            cmbAddressColor.SelectedItem = template.AddressColor;

            // Barcode
            chkAddBarcode.Checked = template.AddBarcode;
            txtBarcodeNumber.Text = template.BarcodeNumber;
            cmbBarcodeType.SelectedItem = template.BarcodeType;
            txtBarcodeX.Text = template.BarcodeX.ToString();
            txtBarcodeY.Text = template.BarcodeY.ToString();
            txtBarcodeScale.Text = template.BarcodeScale.ToString();
            cmbBarcodeRotation.SelectedIndex = template.BarcodeRotation / 90;
            chkWhiteBackground.Checked = template.WhiteBackground;

            // Text 1
            txtText1.Text = template.Text1;
            txtText1X.Text = template.Text1X.ToString();
            txtText1Y.Text = template.Text1Y.ToString();
            txtText1Size.Text = template.Text1Size.ToString();
            cmbText1Color.SelectedItem = template.Text1Color;

            // Special
            chkAddSpine.Checked = template.AddSpine;

            // Update position presets after loading product line
            UpdatePositionPresets();
        }

        private void UpdateTemplateFromUI()
        {
            // Basic info
            _currentTemplate.Name = txtTemplateName.Text;
            _currentTemplate.SourcePdfPath = txtSourcePdf.Text;

            // Logo
            _currentTemplate.AddLogo = chkAddLogo.Checked;
            _currentTemplate.LogoPath = txtLogoPath.Text;
            int.TryParse(txtLogoX.Text, out int logoX);
            _currentTemplate.LogoX = logoX;
            int.TryParse(txtLogoY.Text, out int logoY);
            _currentTemplate.LogoY = logoY;
            int.TryParse(txtLogoScale.Text, out int logoScale);
            _currentTemplate.LogoScale = logoScale;

            // Address
            _currentTemplate.Address1 = txtAddress1.Text;
            _currentTemplate.Address2 = txtAddress2.Text;
            _currentTemplate.Address3 = txtAddress3.Text;
            int.TryParse(txtAddressX.Text, out int addressX);
            _currentTemplate.AddressX = addressX;
            int.TryParse(txtAddressY.Text, out int addressY);
            _currentTemplate.AddressY = addressY;
            float.TryParse(txtAddressSize.Text, out float addressSize);
            _currentTemplate.AddressSize = addressSize;
            _currentTemplate.AddressColor = cmbAddressColor.SelectedItem?.ToString() ?? "Black";

            // Barcode
            _currentTemplate.AddBarcode = chkAddBarcode.Checked;
            _currentTemplate.BarcodeNumber = txtBarcodeNumber.Text;
            _currentTemplate.BarcodeType = cmbBarcodeType.SelectedItem?.ToString() ?? "CODABAR";
            int.TryParse(txtBarcodeX.Text, out int barcodeX);
            _currentTemplate.BarcodeX = barcodeX;
            int.TryParse(txtBarcodeY.Text, out int barcodeY);
            _currentTemplate.BarcodeY = barcodeY;
            float.TryParse(txtBarcodeScale.Text, out float barcodeScale);
            _currentTemplate.BarcodeScale = barcodeScale;
            _currentTemplate.BarcodeRotation = cmbBarcodeRotation.SelectedIndex * 90;
            _currentTemplate.WhiteBackground = chkWhiteBackground.Checked;

            // Text 1
            _currentTemplate.Text1 = txtText1.Text;
            int.TryParse(txtText1X.Text, out int text1X);
            _currentTemplate.Text1X = text1X;
            int.TryParse(txtText1Y.Text, out int text1Y);
            _currentTemplate.Text1Y = text1Y;
            float.TryParse(txtText1Size.Text, out float text1Size);
            _currentTemplate.Text1Size = text1Size;
            _currentTemplate.Text1Color = cmbText1Color.SelectedItem?.ToString() ?? "Black";

            // Special
            _currentTemplate.AddSpine = chkAddSpine.Checked;
        }

        private void SaveCurrentTemplate()
        {
            try
            {
                UpdateTemplateFromUI();
                
                // Check for advanced features that require JSON format
                if (_currentTemplate.HasAdvancedFeatures())
                {
                    var warning = _currentTemplate.GetLegacyCompatibilityWarning();
                    var result = MessageBox.Show(warning + "\n\nDo you want to continue saving as JSON format?", 
                        "Advanced Features Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    
                    if (result == DialogResult.No)
                        return;
                }
                
                _currentTemplate.SaveToFile(_currentTemplateFilePath);
                MessageBox.Show("Template saved successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
