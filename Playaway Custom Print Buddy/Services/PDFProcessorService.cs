using System;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using Playaway_Custom_Print_Buddy.Models;
using Playaway_Custom_Print_Buddy.Utilities;

namespace Playaway_Custom_Print_Buddy.Services
{
    public class PDFProcessorService
    {
        private readonly string _outputDirectory;
        private readonly string _templateDirectory;

        public PDFProcessorService()
        {
            _outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PDF_Output");
            _templateDirectory = Path.Combine(Application.StartupPath, "Templates");
            
            // Ensure directories exist
            if (!Directory.Exists(_outputDirectory))
                Directory.CreateDirectory(_outputDirectory);
            if (!Directory.Exists(_templateDirectory))
                Directory.CreateDirectory(_templateDirectory);
        }

        public async Task<string> ProcessPlayawayPDF(JobItem job, SKUData skuData)
        {
            try
            {
                // Simulate async PDF processing for Playaway
                await Task.Delay(500);

                string outputPath = Path.Combine(_outputDirectory, $"Playaway_{job.SKU}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                // Create PDF document
                using (var document = new Document(PageSize.LETTER))
                {
                    using (var writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create)))
                    {
                        document.Open();

                        // Add title
                        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        var title = new Paragraph($"Playaway Audio Book - {skuData.Title}", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);

                        document.Add(new Paragraph("\n"));

                        // Add product information
                        var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                        document.Add(new Paragraph($"SKU: {job.SKU}", normalFont));
                        document.Add(new Paragraph($"Title: {skuData.Title}", normalFont));
                        document.Add(new Paragraph($"Author: {skuData.Author}", normalFont));
                        document.Add(new Paragraph($"ISBN: {skuData.ISBN}", normalFont));
                        document.Add(new Paragraph($"UPC: {skuData.UPC}", normalFont));
                        document.Add(new Paragraph($"Duration: {skuData.Duration}", normalFont));
                        document.Add(new Paragraph($"Classification: {skuData.Classification}", normalFont));

                        // Add barcode placeholder
                        document.Add(new Paragraph("\n"));
                        var barcodeText = new Paragraph("BARCODE PLACEHOLDER", normalFont);
                        barcodeText.Alignment = Element.ALIGN_CENTER;
                        document.Add(barcodeText);

                        // Process content using FastReplacer
                        var content = GeneratePlayawayContent(skuData);
                        var processedContent = ProcessTokens(content, skuData);
                        document.Add(new Paragraph("\n"));
                        document.Add(new Paragraph(processedContent, normalFont));

                        document.Close();
                    }
                }

                return outputPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process Playaway PDF: {ex.Message}", ex);
            }
        }

        public async Task<string> ProcessLaunchpadPDF(JobItem job, SKUData skuData)
        {
            try
            {
                // Simulate async PDF processing for Launchpad/Whazoodle
                await Task.Delay(500);

                string outputPath = Path.Combine(_outputDirectory, $"Launchpad_{job.SKU}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                // Create PDF document
                using (var document = new Document(PageSize.LETTER))
                {
                    using (var writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create)))
                    {
                        document.Open();

                        // Add title based on processing mode
                        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        string modeText = job.ProcessingMode == ProcessingMode.WZ ? "Whazoodle" : "Launchpad";
                        var title = new Paragraph($"{modeText} Print-On-Demand Insert - {skuData.Title}", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);

                        document.Add(new Paragraph("\n"));

                        // Add product information
                        var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                        document.Add(new Paragraph($"SKU: {job.SKU}", normalFont));
                        document.Add(new Paragraph($"Mode: {job.ProcessingMode}", normalFont));
                        document.Add(new Paragraph($"Title: {skuData.Title}", normalFont));
                        document.Add(new Paragraph($"Author: {skuData.Author}", normalFont));
                        document.Add(new Paragraph($"ISBN: {skuData.ISBN}", normalFont));
                        document.Add(new Paragraph($"UPC: {skuData.UPC}", normalFont));
                        document.Add(new Paragraph($"Publisher: {skuData.Publisher}", normalFont));
                        document.Add(new Paragraph($"Publication Date: {skuData.PubDate}", normalFont));
                        document.Add(new Paragraph($"Series: {skuData.Series}", normalFont));

                        // Add age/grade information
                        if (!string.IsNullOrEmpty(skuData.AgeRange))
                            document.Add(new Paragraph($"Age Range: {skuData.AgeRange}", normalFont));
                        if (!string.IsNullOrEmpty(skuData.Grade))
                            document.Add(new Paragraph($"Grade Level: {skuData.Grade}", normalFont));
                        if (!string.IsNullOrEmpty(skuData.Interest))
                            document.Add(new Paragraph($"Interest Level: {skuData.Interest}", normalFont));

                        // Add barcode placeholder
                        document.Add(new Paragraph("\n"));
                        var barcodeText = new Paragraph("BARCODE PLACEHOLDER", normalFont);
                        barcodeText.Alignment = Element.ALIGN_CENTER;
                        document.Add(barcodeText);

                        // Process content using FastReplacer
                        var content = GenerateLaunchpadContent(skuData, job.ProcessingMode);
                        var processedContent = ProcessTokens(content, skuData);
                        document.Add(new Paragraph("\n"));
                        document.Add(new Paragraph(processedContent, normalFont));

                        document.Close();
                    }
                }

                return outputPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process Launchpad PDF: {ex.Message}", ex);
            }
        }

        private string GeneratePlayawayContent(SKUData skuData)
        {
            return @"
PLAYAWAY AUDIO BOOK INFORMATION

This Playaway audiobook device contains a complete, unabridged recording
of the book listed above. The device is pre-loaded with the content and
requires no additional downloads or setup.

Features:
- Self-contained audio player
- Built-in rechargeable battery
- Easy-to-use controls
- High-quality digital audio

To use this device:
1. Insert the included earbuds or headphones
2. Press the power button to turn on
3. Use the play/pause button to control playback
4. Adjust volume using the volume buttons

Battery life: Up to 8 hours of continuous playback
Charging: USB cable included for recharging

For technical support or replacement, please contact your library or
visit www.playaway.com for more information.

MARC RECORD INFORMATION:
Classification: [%CLASSIFICATION%]
Language: [%LANGUAGE%]
Duration: [%DURATION%]

This item is intended for library circulation and should be returned
according to your library's lending policies.
";
        }

        private string GenerateLaunchpadContent(SKUData skuData, ProcessingMode mode)
        {
            string modeText = mode == ProcessingMode.WZ ? "Whazoodle" : "Launchpad";
            
            return $@"
{modeText.ToUpper()} PRINT-ON-DEMAND INSERT

This is a custom insert created for the {modeText} Print-On-Demand service.
The information contained herein is specific to the title listed above.

Publication Information:
Publisher: [%PUBLISHER%]
Publication Date: [%PUBDATE%]
Series: [%SERIES%]
Classification: [%CLASSIFICATION%]

Target Audience:
Age Range: [%AGERANGE%]
Grade Level: [%GRADE%]
Interest Level: [%INTEREST%]
Language: [%LANGUAGE%]

Additional Information:
Subtitle: [%SUBTITLE%]

This insert is designed to accompany the main publication and provides
supplementary information for cataloging and circulation purposes.

For questions about this title or the {modeText} service, please contact
your library's technical services department.

Processing Mode: {mode}
Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
";
        }

        private string ProcessTokens(string content, SKUData skuData)
        {
            var replacer = new FastReplacerSnippet();
            
            // Add all the token replacements
            replacer.AddReplacement("[%CLASSIFICATION%]", skuData.Classification ?? "");
            replacer.AddReplacement("[%LANGUAGE%]", skuData.Language ?? "");
            replacer.AddReplacement("[%DURATION%]", skuData.Duration ?? "");
            replacer.AddReplacement("[%PUBLISHER%]", skuData.Publisher ?? "");
            replacer.AddReplacement("[%PUBDATE%]", skuData.PubDate ?? "");
            replacer.AddReplacement("[%SERIES%]", skuData.Series ?? "");
            replacer.AddReplacement("[%AGERANGE%]", skuData.AgeRange ?? "");
            replacer.AddReplacement("[%GRADE%]", skuData.Grade ?? "");
            replacer.AddReplacement("[%INTEREST%]", skuData.Interest ?? "");
            replacer.AddReplacement("[%SUBTITLE%]", skuData.Subtitle ?? "");
            
            return replacer.ProcessText(content);
        }

        public string GetOutputDirectory()
        {
            return _outputDirectory;
        }

        public void OpenOutputFolder()
        {
            if (Directory.Exists(_outputDirectory))
            {
                System.Diagnostics.Process.Start("explorer.exe", _outputDirectory);
            }
        }
    }
} 