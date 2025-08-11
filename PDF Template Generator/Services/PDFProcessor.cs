using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PDF_Template_Generator.Models;
using BusinessRefinery.Barcode;

namespace PDF_Template_Generator.Services
{
    public class PDFProcessor
    {
        private readonly string _outputDirectory;

        public PDFProcessor(string outputDirectory)
        {
            _outputDirectory = outputDirectory;
        }

        public bool ProcessTemplate(TemplateConfig config, string outputFileName = "")
        {
            if (string.IsNullOrEmpty(config.SourcePdfPath) || !File.Exists(config.SourcePdfPath))
            {
                throw new FileNotFoundException("Source PDF file not found: " + config.SourcePdfPath);
            }

            try
            {
                // Determine output filename
                if (string.IsNullOrEmpty(outputFileName))
                {
                    FileInfo sourceInfo = new FileInfo(config.SourcePdfPath);
                    outputFileName = sourceInfo.Name;
                }

                string outputPath = Path.Combine(_outputDirectory, outputFileName);

                // Open source PDF
                using (var reader = new PdfReader(config.SourcePdfPath))
                {
                    var size = reader.GetPageSizeWithRotation(1);
                    var document = new Document(size);

                    using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        document.Open();

                        using (var stamper = new PdfStamper(reader, fs))
                        {
                            var cb = stamper.GetOverContent(1);

                            // Add background elements first
                            AddBackgroundElements(cb, config);

                            // Add spine if enabled
                            if (config.AddSpine)
                            {
                                AddSpineElement(cb, config);
                            }

                            // Add logo if enabled
                            if (config.AddLogo && !string.IsNullOrEmpty(config.LogoPath))
                            {
                                AddLogoElement(cb, config);
                            }

                            // Add address text
                            if (!string.IsNullOrEmpty(config.Address1) || !string.IsNullOrEmpty(config.Address2) || !string.IsNullOrEmpty(config.Address3))
                            {
                                AddAddressText(cb, config);
                            }

                            // Add barcode if enabled
                            if (config.AddBarcode && !string.IsNullOrEmpty(config.BarcodeNumber))
                            {
                                AddBarcodeElement(cb, config);
                            }

                            // Add text elements
                            AddTextElements(cb, config);

                            stamper.Close();
                        }

                        document.Close();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing PDF template: {ex.Message}", ex);
            }
        }

        private void AddBackgroundElements(PdfContentByte cb, TemplateConfig config)
        {
            try
            {
                // Add product-specific background elements
                switch (config.ProductLine)
                {
                    case ProductLine.Playaway:
                        // Add bottom area covering image for Playaway
                        if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Assets", "WhiteBoxBottom.gif")))
                        {
                            var bottomArea = iTextSharp.text.Image.GetInstance(Path.Combine(Environment.CurrentDirectory, "Assets", "WhiteBoxBottom.gif"));
                            bottomArea.ScalePercent(40);
                            bottomArea.Alignment = 0;
                            bottomArea.SetAbsolutePosition(480, 40);
                            cb.AddImage(bottomArea);
                        }
                        break;
                    // Add cases for other product lines as needed
                }
            }
            catch
            {
                // Continue if background elements fail
            }
        }

        private void AddSpineElement(PdfContentByte cb, TemplateConfig config)
        {
            try
            {
                string spineAsset = GetSpineAssetPath(config.ProductLine);
                if (!File.Exists(spineAsset)) return;

                var spineBox = iTextSharp.text.Image.GetInstance(spineAsset);
                
                // Set scale and positioning based on product line
                switch (config.ProductLine)
                {
                    case ProductLine.Playaway:
                        spineBox.ScalePercent(24F);
                        spineBox.SetAbsolutePosition(308, 49);
                        break;
                    case ProductLine.WhaZoodle:
                        spineBox.ScalePercent(28);
                        spineBox.SetAbsolutePosition(455, 52);
                        break;
                    case ProductLine.Launchpad:
                        spineBox.ScalePercent(28);
                        spineBox.SetAbsolutePosition(600, 52);
                        break;
                }

                spineBox.Alignment = 0;
                cb.AddImage(spineBox);
            }
            catch
            {
                // Continue if spine element fails
            }
        }

        private void AddLogoElement(PdfContentByte cb, TemplateConfig config)
        {
            try
            {
                if (!File.Exists(config.LogoPath)) return;

                var logo = iTextSharp.text.Image.GetInstance(config.LogoPath);
                logo.ScalePercent(config.LogoScale);
                logo.Alignment = 0;
                logo.SetAbsolutePosition(config.LogoX, config.LogoY);
                cb.AddImage(logo);
            }
            catch
            {
                // Continue if logo fails
            }
        }

        private void AddAddressText(PdfContentByte cb, TemplateConfig config)
        {
            try
            {
                var color = GetColor(config.AddressColor);
                var bf = GetFont(config.AddressFont);
                cb.SetColorFill(color);
                cb.SetFontAndSize(bf, config.AddressSize);

                if (!string.IsNullOrEmpty(config.Address1))
                {
                    cb.BeginText();
                    cb.ShowTextAligned(0, config.Address1, config.AddressX, config.AddressY + 10, 0);
                    cb.EndText();
                }

                if (!string.IsNullOrEmpty(config.Address2))
                {
                    cb.BeginText();
                    cb.ShowTextAligned(0, config.Address2, config.AddressX, config.AddressY, 0);
                    cb.EndText();
                }

                if (!string.IsNullOrEmpty(config.Address3))
                {
                    cb.BeginText();
                    cb.ShowTextAligned(0, config.Address3, config.AddressX, config.AddressY - 10, 0);
                    cb.EndText();
                }
            }
            catch
            {
                // Continue if address text fails
            }
        }

        private void AddBarcodeElement(PdfContentByte cb, TemplateConfig config)
        {
            try
            {
                string tempBarcodePath = Path.GetTempFileName() + ".png";
                
                // Generate barcode with rotation support
                if (GenerateBarcode(config.BarcodeNumber, config.BarcodeType, tempBarcodePath, config.BarcodeRotation))
                {
                    // Add white background if specified
                    if (config.WhiteBackground)
                    {
                        string whiteBoxPath = Path.Combine(Environment.CurrentDirectory, "Assets", "WhiteBoxBack.gif");
                        if (File.Exists(whiteBoxPath))
                        {
                            var backBox = iTextSharp.text.Image.GetInstance(whiteBoxPath);
                            backBox.ScalePercent(config.BarcodeScale + 12);
                            backBox.Alignment = 0;
                            backBox.SetAbsolutePosition(config.BarcodeX - 10, config.BarcodeY - 5);
                            cb.AddImage(backBox);
                        }
                    }

                    // Add barcode image (rotation already applied during generation)
                    var barcode = iTextSharp.text.Image.GetInstance(tempBarcodePath);
                    barcode.ScalePercent(config.BarcodeScale);
                    barcode.Alignment = 0;
                    barcode.SetAbsolutePosition(config.BarcodeX, config.BarcodeY);
                    cb.AddImage(barcode);

                    // Clean up temp file
                    if (File.Exists(tempBarcodePath))
                        File.Delete(tempBarcodePath);
                }
            }
            catch
            {
                // Continue if barcode fails
            }
        }

        private void AddTextElements(PdfContentByte cb, TemplateConfig config)
        {
            AddTextElement(cb, config.Text1Font, config.Text1, config.Text1X, config.Text1Y, config.Text1Size, config.Text1Color);
            AddTextElement(cb, config.Text2Font, config.Text2, config.Text2X, config.Text2Y, config.Text2Size, config.Text2Color);
            AddTextElement(cb, config.Text3Font, config.Text3, config.Text3X, config.Text3Y, config.Text3Size, config.Text3Color);
            AddTextElement(cb, config.Text4Font, config.Text4, config.Text4X, config.Text4Y, config.Text4Size, config.Text4Color);
            AddTextElement(cb, config.Text5Font, config.Text5, config.Text5X, config.Text5Y, config.Text5Size, config.Text5Color);
        }

        private void AddTextElement(PdfContentByte cb, string font, string text, int x, int y, float size, string color)
        {
            if (string.IsNullOrEmpty(text)) return;

            try
            {
                var textColor = GetColor(color);
                var bf = GetFont(font);
                cb.SetColorFill(textColor);
                cb.SetFontAndSize(bf, size);

                cb.BeginText();
                cb.ShowTextAligned(0, text, x, y, 0);
                cb.EndText();
            }
            catch
            {
                // Continue if text element fails
            }
        }

        private BaseColor GetColor(string colorName) => colorName?.ToLower() switch
        {
            "red" => BaseColor.RED,
            "blue" => BaseColor.BLUE,
            "green" => BaseColor.GREEN,
            "black" or _ => BaseColor.BLACK
        };

        private BaseFont GetFont(string font) => font switch
        {
            "Times New Roman" => BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.EMBEDDED),
            "Courier" => BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.EMBEDDED),
            "Helvetica" or _ => BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED),
        };
        

        private string GetSpineAssetPath(ProductLine productLine)
        {
            string assetName = productLine switch
            {
                ProductLine.Playaway => "play-spine.gif",
                ProductLine.Launchpad or ProductLine.WhaZoodle => "view-spine.gif",
                _ => "play-spine.gif"
            };

            return Path.Combine(Environment.CurrentDirectory, "Assets", assetName);
        }

        private bool GenerateBarcode(string data, string type, string outputPath)
        {
            return GenerateBarcode(data, type, outputPath, 0); // Default no rotation
        }

        private bool GenerateBarcode(string data, string type, string outputPath, int rotation)
        {
            try
            {
                // Debug output to verify rotation value
                System.Diagnostics.Debug.WriteLine($"Generating barcode: data='{data}', type='{type}', rotation={rotation}°");
                
                // Use BusinessRefinery.Barcode exactly like legacy code
                Linear barcode = new Linear();
                
                // Set symbology based on type
                switch (type.ToUpper())
                {
                    case "CODABAR":
                        barcode.Symbology = Symbology.CODABAR;
                        break;
                    case "CODE11":
                        barcode.Symbology = Symbology.CODE11;
                        break;
                    case "CODE128":
                        barcode.Symbology = Symbology.CODE128;
                        break;
                    case "CODE39":
                        barcode.Symbology = Symbology.CODE39;
                        break;
                    case "CODE93":
                        barcode.Symbology = Symbology.CODE93;
                        break;
                    default:
                        barcode.Symbology = Symbology.CODABAR;
                        break;
                }
                
                // Set font and data exactly like legacy
                barcode.TextFont = new System.Drawing.Font("Times New Roman", 18F);
                barcode.Code = data;
                
                // Generate barcode to file exactly like legacy
                if (rotation == 0)
                {
                    // No rotation needed
                    barcode.drawBarcode2ImageFile(outputPath);
                }
                else
                {
                    // Generate to temp file first, then rotate
                    string tempPath = Path.GetTempFileName() + ".png";
                    barcode.drawBarcode2ImageFile(tempPath);
                    
                    // Rotate the image
                    RotateImage(tempPath, outputPath, rotation);
                    
                    // Clean up temp file
                    File.Delete(tempPath);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Barcode generation failed: {ex.Message}");
                
                // Fallback to text-based barcode if BusinessRefinery fails
                try
                {
                    using (var bitmap = new System.Drawing.Bitmap(200, 50))
                    using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(System.Drawing.Color.White);
                        graphics.DrawString($"{type}: {data}", new System.Drawing.Font("Arial", 8), System.Drawing.Brushes.Black, 5, 5);
                        bitmap.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private void RotateImage(string inputPath, string outputPath, int rotation)
        {
            using (var originalImage = System.Drawing.Image.FromFile(inputPath))
            {
                // Determine rotation type
                RotateFlipType rotateType = rotation switch
                {
                    90 => RotateFlipType.Rotate90FlipNone,
                    180 => RotateFlipType.Rotate180FlipNone,
                    270 => RotateFlipType.Rotate270FlipNone,
                    _ => RotateFlipType.RotateNoneFlipNone
                };
                
                if (rotateType != RotateFlipType.RotateNoneFlipNone)
                {
                    using (var rotatedImage = new System.Drawing.Bitmap(originalImage))
                    {
                        rotatedImage.RotateFlip(rotateType);
                        rotatedImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                else
                {
                    originalImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
} 