using PDF_Template_Generator.Controls;
using PDF_Template_Generator.Models;
using PDF_Template_Generator.Services;

namespace PDF_Template_Generator
{
    public partial class VisualForm : Form
    {
        private TemplateConfig _currentTemplate;
        private PositionPresetManager _presetManager;
        private PDFProcessor _pdfProcessor;
        private string _currentTemplateFilePath = string.Empty;
        private string _outputDirectory = string.Empty;

        public VisualForm()
        {
            InitializeComponent();
            InitializeApplication();
            WireUpEvents();
        }

        private void InitializeApplication()
        {
            _currentTemplate = new TemplateConfig();
            _presetManager = new PositionPresetManager();
            
            // Load settings
            LoadApplicationSettings();
            _pdfProcessor = new PDFProcessor(_outputDirectory);
            
            // Set defaults
            cmbProductLine.SelectedIndex = 0; // Default to Playaway
            UpdateTemplateFromUI();
        }

        private void LoadApplicationSettings()
        {
            _outputDirectory = Properties.Settings.Default.pdfEditedLocation;
            if (string.IsNullOrEmpty(_outputDirectory))
            {
                _outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PDF Templates");
                if (!Directory.Exists(_outputDirectory))
                    Directory.CreateDirectory(_outputDirectory);
            }
        }

        private void WireUpEvents()
        {
            // PDF Preview events
            pdfPreviewControl.ElementClicked += PdfPreviewControl_ElementClicked;
            pdfPreviewControl.ElementPositionChanged += PdfPreviewControl_ElementPositionChanged;
            pdfPreviewControl.ElementSelected += PdfPreviewControl_ElementSelected;

            // Toolbar events
            elementToolbar.ElementTypeSelected += ElementToolbar_ElementTypeSelected;
            elementToolbar.PreviewRequested += ElementToolbar_PreviewRequested;
            elementToolbar.GenerateRequested += ElementToolbar_GenerateRequested;
            elementToolbar.ZoomChangeRequested += ElementToolbar_ZoomChangeRequested;
            elementToolbar.FitToWindowRequested += ElementToolbar_FitToWindowRequested;

            // Properties panel events
            propertiesPanel.PropertyChanged += PropertiesPanel_PropertyChanged;
        }

        #region Event Handlers

        private void PdfPreviewControl_ElementClicked(object sender, ElementClickedEventArgs e)
        {
            // Create new element at clicked position
            var elementType = elementToolbar.SelectedElementType;
            
            // Cannot add elements without source PDF loaded in viewer
            if (string.IsNullOrEmpty(_currentTemplate.SourcePdfPath))
            {
                MessageBox.Show($"Please load a source PDF before adding elements", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Prevent users from adding more than 5 text elements or more than 1 of all other elements
            switch (elementType)
            {
                case ElementType.Text:
                    if (pdfPreviewControl.GetElements().Count(x => x.Type == elementType) == 5)
                    {
                        MessageBox.Show($"Cannot add more than 5 {elementType} elements. Please delete one to add another.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                default:
                    if (pdfPreviewControl.GetElements().Count(x => x.Type == elementType) == 1)
                    {
                        MessageBox.Show($"Cannot add more than 1 {elementType} element. Please delete one to add another.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
            }
            
            // Create and add element
            var element = CreateNewElement(elementType, e.Position);
            pdfPreviewControl.AddElement(element);
            propertiesPanel.SetSelectedElement(element);
            
            elementToolbar.SetInstruction($"{elementType} added! Configure properties on the right or drag to reposition.");
        }

        private void PdfPreviewControl_ElementPositionChanged(object sender, ElementPositionChangedEventArgs e)
        {
            // Update properties panel with new position
            propertiesPanel.SetSelectedElement(e.Element);
        }

        private void PdfPreviewControl_ElementSelected(object sender, ElementSelectedEventArgs e)
        {
            if (e.Element != null)
            {
                // Update properties panel
                propertiesPanel.SetSelectedElement(e.Element);
                
                // Provide detailed feedback about the selected element
                var elementInfo = $"{e.Element.Type} selected at ({e.Element.X}, {e.Element.Y})";
                if (e.Element.Rotation != 0)
                {
                    elementInfo += $" rotated {e.Element.Rotation}°";
                }
                elementInfo += " - Edit properties on right, drag to move";
                
                elementToolbar.SetInstruction(elementInfo);
            }
            else
            {
                // Clear selection
                propertiesPanel.SetSelectedElement(null);
                elementToolbar.SetInstruction("Click to place elements or select existing ones to edit");
            }
        }

        private void ElementToolbar_ElementTypeSelected(object sender, ElementTypeSelectedEventArgs e)
        {
            // User selected a different element type to place
            elementToolbar.SetInstruction($"Click on the PDF preview to place a {e.ElementType.ToString().ToLower()}");
        }

        private void ElementToolbar_PreviewRequested(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentTemplate.SourcePdfPath))
                {
                    MessageBox.Show("Please select a source PDF file first.", "Preview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UpdateTemplateFromUI();
                UpdateTemplateFromVisualDesigner();
                
                // Generate a preview PDF with timestamp
                string previewFileName = $"PREVIEW_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                
                _pdfProcessor.ProcessTemplate(_currentTemplate, previewFileName);
                
                var previewPath = Path.Combine(_outputDirectory, previewFileName);
                
                // Show success dialog with option to open
                var result = MessageBox.Show($"Preview generated successfully!\n\nFile: {previewFileName}\n\nWould you like to open it now?", 
                    "Preview Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = previewPath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not open preview file: {ex.Message}\n\nFile saved to: {previewPath}", 
                            "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating preview: {ex.Message}", "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ElementToolbar_GenerateRequested(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private void ElementToolbar_ZoomChangeRequested(object sender, ZoomChangeEventArgs e)
        {
            pdfPreviewControl.ZoomFactor += e.ZoomDelta;
        }

        private void ElementToolbar_FitToWindowRequested(object sender, EventArgs e)
        {
            // Get the actual PDF dimensions and control size
            var (pdfWidth, pdfHeight) = pdfPreviewControl.GetActualPdfDimensions();
            var previewScale = pdfPreviewControl.GetPreviewScale();
            
            if (pdfWidth > 0 && pdfHeight > 0)
            {
                // Calculate zoom to fit the control
                var controlWidth = pdfPreviewControl.Width - 40; // Leave some margin
                var controlHeight = pdfPreviewControl.Height - 40;
                
                var scaledPdfWidth = pdfWidth * previewScale;
                var scaledPdfHeight = pdfHeight * previewScale;
                
                var zoomX = controlWidth / scaledPdfWidth;
                var zoomY = controlHeight / scaledPdfHeight;
                
                // Use the smaller zoom to ensure it fits
                var fitZoom = Math.Min(zoomX, zoomY);
                
                pdfPreviewControl.ZoomFactor = fitZoom;
                
                // Center the PDF in the control
                var finalPdfWidth = scaledPdfWidth * fitZoom;
                var finalPdfHeight = scaledPdfHeight * fitZoom;
                
                var centerX = (pdfPreviewControl.Width - finalPdfWidth) / 2;
                var centerY = (pdfPreviewControl.Height - finalPdfHeight) / 2;
                
                pdfPreviewControl.SetPanOffset(new Point((int)centerX, (int)centerY));
                
                elementToolbar.SetInstruction($"Zoomed to {fitZoom:P0} and centered");
            }
            else
            {
                // Fallback to 100%
                pdfPreviewControl.ZoomFactor = 1.0f;
                pdfPreviewControl.SetPanOffset(new Point(20, 20)); // Small margin
                elementToolbar.SetInstruction("Reset zoom to 100%");
            }
        }

        private void PropertiesPanel_PropertyChanged(object sender, ElementPropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DELETE")
            {
                pdfPreviewControl.RemoveElement(e.Element);
                propertiesPanel.SetSelectedElement(null);
                elementToolbar.SetInstruction("Element deleted.");
            }
            else if (e.PropertyName == "DUPLICATE")
            {
                var duplicate = DuplicateElement(e.Element);
                pdfPreviewControl.AddElement(duplicate);
                propertiesPanel.SetSelectedElement(duplicate);
                elementToolbar.SetInstruction("Element duplicated.");
            }
            else
            {
                // Property changed - refresh preview
                pdfPreviewControl.Invalidate();
            }
        }

        private void cmbProductLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductLine.SelectedItem != null)
            {
                var productLine = (ProductLine)Enum.Parse(typeof(ProductLine), cmbProductLine.SelectedItem.ToString() ?? "Playaway");
                _currentTemplate.ProductLine = productLine;
                
                // Could load product-specific template here
                elementToolbar.SetInstruction($"Product line changed to {productLine}. Position presets updated.");
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
                
                // Load PDF into preview
                pdfPreviewControl.PdfPath = openFileDialog1.FileName;
                elementToolbar.SetInstruction("PDF loaded! Select an element type and click on the preview to place it.");
            }
        }

        #endregion

        #region Menu Events

        private void newTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmLoseChanges())
            {
                _currentTemplate = new TemplateConfig();
                _currentTemplateFilePath = string.Empty;
                LoadTemplateToUI(_currentTemplate);
                pdfPreviewControl.ClearElements();
                propertiesPanel.SetSelectedElement(null);
                this.Text = "PDF Template Generator - Visual Designer - New Template";
            }
        }

        private void openTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Template Files (*.json)|*.json";
            openFileDialog1.Title = "Open Template";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var template = TemplateConfig.LoadFromFile(openFileDialog1.FileName);
                if (template != null)
                {
                    _currentTemplate = template;
                    _currentTemplateFilePath = openFileDialog1.FileName;
                    LoadTemplateToUI(_currentTemplate);
                    LoadTemplateToVisualDesigner(_currentTemplate);
                    this.Text = $"PDF Template Generator - Visual Designer - {Path.GetFileNameWithoutExtension(openFileDialog1.FileName)}";
                }
                else
                {
                    MessageBox.Show("Failed to load template file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            saveFileDialog1.Filter = "Template Files (*.json)|*.json";
            saveFileDialog1.Title = "Save Template As";
            
            if (!string.IsNullOrEmpty(_currentTemplate.Name))
            {
                saveFileDialog1.FileName = _currentTemplate.Name + ".json";
            }
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _currentTemplateFilePath = saveFileDialog1.FileName;
                SaveCurrentTemplate();
                this.Text = $"PDF Template Generator - Visual Designer - {Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)}";
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
                    LoadTemplateToVisualDesigner(_currentTemplate);
                    MessageBox.Show("Legacy template imported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to import legacy template.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                LoadApplicationSettings();
                _pdfProcessor = new PDFProcessor(_outputDirectory);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PDF Template Generator v2.0\n\nVisual designer for Playaway, Launchpad, and WhaZoodle templates.\n\nClick-to-place interface with drag-and-drop positioning.", 
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Helper Methods

        private VisualElement CreateNewElement(ElementType elementType, Point position)
        {
            var element = new VisualElement
            {
                Type = elementType,
                Name = GetDefaultElementName(elementType),
                X = position.X,
                Y = position.Y,
                Width = GetDefaultElementWidth(elementType),
                Height = GetDefaultElementHeight(elementType)
            };

            // Set default properties based on element type
            switch (elementType)
            {
                case ElementType.Barcode:
                    element.Properties["BarcodeNumber"] = "123456789";
                    element.Properties["BarcodeType"] = "CODABAR";
                    element.Properties["Scale"] = 100;
                    break;
                case ElementType.Logo:
                    element.Properties["LogoPath"] = "";
                    element.Properties["Scale"] = 100;
                    break;
                case ElementType.Address:
                    element.Properties["Address1"] = "Address Line 1";
                    element.Properties["Address2"] = "Address Line 2";
                    element.Properties["Address3"] = "Address Line 3";
                    element.Properties["Font"] = "Helvetica";
                    element.Properties["FontSize"] = 12;
                    element.Properties["Color"] = "Black";
                    element.Width = 200;
                    element.Height = 60;
                    break;
                case ElementType.Text:
                    element.Properties["Text"] = "Custom Text";
                    element.Properties["Font"] = "Helvetica";
                    element.Properties["FontSize"] = 12;
                    element.Properties["Color"] = "Black";
                    break;
            }

            return element;
        }

        private VisualElement DuplicateElement(VisualElement original)
        {
            var duplicate = new VisualElement
            {
                Type = original.Type,
                Name = original.Name + " Copy",
                X = original.X + 20,
                Y = original.Y + 20,
                Width = original.Width,
                Height = original.Height,
                Rotation = original.Rotation,
                Properties = new Dictionary<string, object>(original.Properties)
            };

            return duplicate;
        }

        private string GetDefaultElementName(ElementType elementType)
        {
            return elementType switch
            {
                ElementType.Barcode => "Barcode",
                ElementType.Logo => "Logo",
                ElementType.Address => "Address",
                ElementType.Text => "Text",
                _ => "Element"
            };
        }

        private int GetDefaultElementWidth(ElementType elementType)
        {
            return elementType switch
            {
                ElementType.Barcode => 120,
                ElementType.Logo => 100,
                ElementType.Address => 200,
                ElementType.Text => 150,
                _ => 100
            };
        }

        private int GetDefaultElementHeight(ElementType elementType)
        {
            return elementType switch
            {
                ElementType.Barcode => 50,
                ElementType.Logo => 80,
                ElementType.Address => 60,
                ElementType.Text => 30,
                _ => 50
            };
        }

        private void LoadTemplateToUI(TemplateConfig template)
        {
            txtTemplateName.Text = template.Name;
            cmbProductLine.SelectedItem = template.ProductLine.ToString();
            txtSourcePdf.Text = template.SourcePdfPath;
            
            if (!string.IsNullOrEmpty(template.SourcePdfPath))
            {
                pdfPreviewControl.PdfPath = template.SourcePdfPath;
            }
        }

        private void LoadTemplateToVisualDesigner(TemplateConfig template)
        {
            pdfPreviewControl.ClearElements();

            // Convert template config to visual elements
            if (template.AddLogo)
            {
                var logoElement = new VisualElement
                {
                    Type = ElementType.Logo,
                    Name = "Logo",
                    X = template.LogoX,
                    Y = template.LogoY,
                    Width = 100,
                    Height = 80
                };
                logoElement.Properties["LogoPath"] = template.LogoPath;
                logoElement.Properties["Scale"] = template.LogoScale;
                pdfPreviewControl.AddElement(logoElement);
            }

            if (template.AddBarcode)
            {
                var barcodeElement = new VisualElement
                {
                    Type = ElementType.Barcode,
                    Name = "Barcode",
                    X = template.BarcodeX,
                    Y = template.BarcodeY,
                    Width = 120,
                    Height = 50,
                    Rotation = template.BarcodeRotation
                };
                barcodeElement.Properties["BarcodeNumber"] = template.BarcodeNumber;
                barcodeElement.Properties["BarcodeType"] = template.BarcodeType;
                barcodeElement.Properties["Scale"] = (int)template.BarcodeScale;
                pdfPreviewControl.AddElement(barcodeElement);
            }

            if (!string.IsNullOrEmpty(template.Address1) || !string.IsNullOrEmpty(template.Address2) || !string.IsNullOrEmpty(template.Address3))
            {
                var addressElement = new VisualElement
                {
                    Type = ElementType.Address,
                    Name = "Address",
                    X = template.AddressX,
                    Y = template.AddressY,
                    Width = 200,
                    Height = 60
                };
                addressElement.Properties["Address1"] = template.Address1;
                addressElement.Properties["Address2"] = template.Address2;
                addressElement.Properties["Address3"] = template.Address3;
                addressElement.Properties["Font"] = template.AddressFont;
                addressElement.Properties["FontSize"] = (int)template.AddressSize;
                addressElement.Properties["TextAlignment"] = template.AddressAlignment;
                addressElement.Properties["Color"] = template.AddressColor;
                pdfPreviewControl.AddElement(addressElement);
            }

            // Add text elements
            AddTextElementIfNotEmpty(template.Text1, template.Text1X, template.Text1Y, template.Text1Font, template.Text1Size, template.Text1Color, "Text 1");
            AddTextElementIfNotEmpty(template.Text2, template.Text2X, template.Text2Y, template.Text2Font, template.Text2Size, template.Text2Color, "Text 2");
            AddTextElementIfNotEmpty(template.Text3, template.Text3X, template.Text3Y, template.Text3Font, template.Text3Size, template.Text3Color, "Text 3");
            AddTextElementIfNotEmpty(template.Text4, template.Text4X, template.Text4Y, template.Text4Font, template.Text4Size, template.Text4Color, "Text 4");
            AddTextElementIfNotEmpty(template.Text5, template.Text5X, template.Text5Y, template.Text5Font, template.Text5Size, template.Text5Color, "Text 5");
        }

        private void AddTextElementIfNotEmpty(string text, int x, int y, string font, float size, string color, string name)
        {
            if (string.IsNullOrEmpty(text)) return;
            
            var textElement = new VisualElement
            {
                Type = ElementType.Text,
                Name = name,
                X = x,
                Y = y,
                Width = 150,
                Height = 30
            };
            textElement.Properties["Text"] = text;
            textElement.Properties["Font"] = font;
            textElement.Properties["FontSize"] = (int)size;
            textElement.Properties["Color"] = color;
            pdfPreviewControl.AddElement(textElement);
        }

        private void UpdateTemplateFromUI()
        {
            _currentTemplate.Name = txtTemplateName.Text;
            _currentTemplate.SourcePdfPath = txtSourcePdf.Text;
            
            if (cmbProductLine.SelectedItem != null)
            {
                _currentTemplate.ProductLine = (ProductLine)Enum.Parse(typeof(ProductLine), cmbProductLine.SelectedItem.ToString() ?? "Playaway");
            }
        }

        private void SaveCurrentTemplate()
        {
            try
            {
                UpdateTemplateFromUI();
                UpdateTemplateFromVisualDesigner();
                _currentTemplate.SaveToFile(_currentTemplateFilePath);
                MessageBox.Show("Template saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTemplateFromVisualDesigner()
        {
            var elements = pdfPreviewControl.GetElements();

            // Reset template elements
            _currentTemplate.AddLogo = false;
            _currentTemplate.AddBarcode = false;
            _currentTemplate.Address1 = _currentTemplate.Address2 = _currentTemplate.Address3 = "";
            _currentTemplate.Text1 = _currentTemplate.Text2 = _currentTemplate.Text3 = _currentTemplate.Text4 = _currentTemplate.Text5 = "";

            int textIndex = 1;
            foreach (var element in elements)
            {
                switch (element.Type)
                {
                    case ElementType.Logo:
                        _currentTemplate.AddLogo = true;
                        _currentTemplate.LogoPath = element.Properties.GetValueOrDefault("LogoPath", "").ToString() ?? "";
                        _currentTemplate.LogoX = element.X;
                        _currentTemplate.LogoY = element.Y;
                        _currentTemplate.LogoScale = (int)element.Properties.GetValueOrDefault("Scale", 100);
                        break;

                    case ElementType.Barcode:
                        _currentTemplate.AddBarcode = true;
                        _currentTemplate.BarcodeNumber = element.Properties.GetValueOrDefault("BarcodeNumber", "").ToString() ?? "";
                        _currentTemplate.BarcodeType = element.Properties.GetValueOrDefault("BarcodeType", "CODABAR").ToString() ?? "CODABAR";
                        _currentTemplate.BarcodeX = element.X;
                        _currentTemplate.BarcodeY = element.Y;
                        _currentTemplate.BarcodeScale = (int)element.Properties.GetValueOrDefault("Scale", 100);
                        _currentTemplate.BarcodeRotation = element.Rotation;
                        break;

                    case ElementType.Address:
                        _currentTemplate.Address1 = element.Properties.GetValueOrDefault("Address1", "").ToString() ?? "";
                        _currentTemplate.Address2 = element.Properties.GetValueOrDefault("Address2", "").ToString() ?? "";
                        _currentTemplate.Address3 = element.Properties.GetValueOrDefault("Address3", "").ToString() ?? "";
                        _currentTemplate.AddressFont = element.Properties.GetValueOrDefault("Font", "Helvetica").ToString() ?? "Helvetica";
                        _currentTemplate.AddressX = element.X;
                        _currentTemplate.AddressY = element.Y;
                        _currentTemplate.AddressSize = (int)element.Properties.GetValueOrDefault("FontSize", 12);
                        _currentTemplate.AddressAlignment = element.Properties.GetValueOrDefault("TextAlignment", "Left").ToString() ?? "Left";
                        _currentTemplate.AddressColor = element.Properties.GetValueOrDefault("Color", "Black").ToString() ?? "Black";
                        break;

                    case ElementType.Text:
                        var text = element.Properties.GetValueOrDefault("Text", "").ToString() ?? "";
                        var fontSize = (int)element.Properties.GetValueOrDefault("FontSize", 12);
                        var font = element.Properties.GetValueOrDefault("Font", "Helvetica").ToString() ?? "Helvetica";
                        var color = element.Properties.GetValueOrDefault("Color", "Black").ToString() ?? "Black";

                        switch (textIndex)
                        {
                            case 1:
                                _currentTemplate.Text1 = text;
                                _currentTemplate.Text1X = element.X;
                                _currentTemplate.Text1Y = element.Y;
                                _currentTemplate.Text1Font = font;
                                _currentTemplate.Text1Size = fontSize;
                                _currentTemplate.Text1Color = color;
                                break;
                            case 2:
                                _currentTemplate.Text2 = text;
                                _currentTemplate.Text2X = element.X;
                                _currentTemplate.Text2Y = element.Y;
                                _currentTemplate.Text2Font = font;
                                _currentTemplate.Text2Size = fontSize;
                                _currentTemplate.Text2Color = color;
                                break;
                            case 3:
                                _currentTemplate.Text3 = text;
                                _currentTemplate.Text3X = element.X;
                                _currentTemplate.Text3Y = element.Y;
                                _currentTemplate.Text3Font = font;
                                _currentTemplate.Text3Size = fontSize;
                                _currentTemplate.Text3Color = color;
                                break;
                            case 4:
                                _currentTemplate.Text4 = text;
                                _currentTemplate.Text4X = element.X;
                                _currentTemplate.Text4Y = element.Y;
                                _currentTemplate.Text4Font = font;
                                _currentTemplate.Text4Size = fontSize;
                                _currentTemplate.Text4Color = color;
                                break;
                            case 5:
                                _currentTemplate.Text5 = text;
                                _currentTemplate.Text5X = element.X;
                                _currentTemplate.Text5Y = element.Y;
                                _currentTemplate.Text5Font = font;
                                _currentTemplate.Text5Size = fontSize;
                                _currentTemplate.Text5Color = color;
                                break;
                        }
                        textIndex++;
                        break;
                }
            }
        }

        private void GeneratePDF()
        {
            try
            {
                UpdateTemplateFromUI();
                UpdateTemplateFromVisualDesigner();
                
                if (string.IsNullOrEmpty(_currentTemplate.SourcePdfPath))
                {
                    MessageBox.Show("Please select a source PDF file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string outputFileName = !string.IsNullOrEmpty(_currentTemplate.Name) 
                    ? _currentTemplate.Name + ".pdf" 
                    : Path.GetFileName(_currentTemplate.SourcePdfPath);

                _pdfProcessor.ProcessTemplate(_currentTemplate, outputFileName);
                
                MessageBox.Show($"PDF generated successfully!\nSaved to: {Path.Combine(_outputDirectory, outputFileName)}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ConfirmLoseChanges()
        {
            // For now, always return true. Could add "unsaved changes" tracking later
            return true;
        }

        private void UpdateSpineOverlay()
        {
            // Update the PDF preview control with spine overlay settings
            if (_currentTemplate != null)
            {
                pdfPreviewControl.SetSpineOverlay(_currentTemplate.AddSpine, _currentTemplate.ProductLine);
            }
        }

        private void chkShowSpine_CheckedChanged(object sender, EventArgs e)
        {
            // Update template with spine setting
            if (_currentTemplate != null)
            {
                _currentTemplate.AddSpine = chkShowSpine.Checked;
                UpdateSpineOverlay();
            }
        }

        #endregion
    }
} 