using PDF_Template_Generator.Controls;
using PDF_Template_Generator.Models;

namespace PDF_Template_Generator.Controls
{
    public partial class ElementPropertiesPanel : UserControl
    {
        private VisualElement? _selectedElement;
        private bool _updating = false;

        public event EventHandler<ElementPropertyChangedEventArgs>? PropertyChanged;

        public ElementPropertiesPanel()
        {
            InitializeComponent();
            ShowNoSelectionMessage();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ElementPropertiesPanel
            // 
            this.AutoScroll = true;
            this.BackColor = Color.WhiteSmoke;
            this.Dock = DockStyle.Fill;
            this.Name = "ElementPropertiesPanel";
            this.Padding = new Padding(10);
            this.Size = new Size(300, 600);
            this.ResumeLayout(false);
        }

        public void SetSelectedElement(VisualElement? element)
        {
            _selectedElement = element;
            BuildPropertiesUI();
        }

        private void BuildPropertiesUI()
        {
            _updating = true;
            
            // Clear existing controls
            this.Controls.Clear();
            
            if (_selectedElement == null)
            {
                ShowNoSelectionMessage();
                _updating = false;
                return;
            }

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true,
                Padding = new Padding(5)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            var row = 0;

            // Element type header
            var typeLabel = new Label
            {
                Text = $"{_selectedElement.Type} Properties",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(typeLabel, 0, row);
            panel.SetColumnSpan(typeLabel, 2);
            row++;

            // Position properties
            AddLabel(panel, "Position:", row++);
            AddNumericInput(panel, "X:", _selectedElement.X, (value) => UpdateElementProperty("X", value), row++);
            AddNumericInput(panel, "Y:", _selectedElement.Y, (value) => UpdateElementProperty("Y", value), row++);
            
            // Size properties
            AddNumericInput(panel, "Width:", _selectedElement.Width, (value) => UpdateElementProperty("Width", value), row++);
            AddNumericInput(panel, "Height:", _selectedElement.Height, (value) => UpdateElementProperty("Height", value), row++);

            // Rotation for barcodes
            if (_selectedElement.Type == ElementType.Barcode)
            {
                AddRotationComboBox(panel, "Rotation:", _selectedElement.Rotation, (value) => UpdateElementProperty("Rotation", value), row++);
            }

            // Type-specific properties
            switch (_selectedElement.Type)
            {
                case ElementType.Barcode:
                    AddBarcodeProperties(panel, ref row);
                    break;
                case ElementType.Logo:
                    AddLogoProperties(panel, ref row);
                    break;
                case ElementType.Address:
                    AddAddressProperties(panel, ref row);
                    break;
                case ElementType.Text:
                    AddTextProperties(panel, ref row);
                    break;
            }

            // Actions
            AddSeparator(panel, row++);
            AddActionButtons(panel, row++);

            this.Controls.Add(panel);
            _updating = false;
        }

        private void ShowNoSelectionMessage()
        {
            var label = new Label
            {
                Text = "Select an element to edit its properties",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10, FontStyle.Italic)
            };
            this.Controls.Add(label);
        }

        private void AddLabel(TableLayoutPanel panel, string text, int row)
        {
            var label = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(label, 0, row);
            panel.SetColumnSpan(label, 2);
        }

        private void AddNumericInput(TableLayoutPanel panel, string label, int value, Action<int> onChanged, int row)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(lbl, 0, row);

            var numeric = new NumericUpDown
            {
                Minimum = -1000,
                Maximum = 2000,
                Dock = DockStyle.Fill
            };
            numeric.Value = value; // Set value AFTER setting range
            numeric.ValueChanged += (s, e) => { if (!_updating) onChanged((int)numeric.Value); };
            panel.Controls.Add(numeric, 1, row);
        }

        private void AddTextInput(TableLayoutPanel panel, string label, string value, Action<string> onChanged, int row)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(lbl, 0, row);

            var textBox = new TextBox
            {
                Text = value,
                Dock = DockStyle.Fill
            };
            textBox.TextChanged += (s, e) => { if (!_updating) onChanged(textBox.Text); };
            panel.Controls.Add(textBox, 1, row);
        }

        private void AddComboBox(TableLayoutPanel panel, string label, string[] items, string selectedValue, Action<string> onChanged, int row)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(lbl, 0, row);

            var combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            combo.Items.AddRange(items);
            combo.SelectedItem = selectedValue;
            combo.SelectedIndexChanged += (s, e) => { if (!_updating && combo.SelectedItem != null) onChanged(combo.SelectedItem.ToString()!); };
            panel.Controls.Add(combo, 1, row);
        }

        private void AddRotationComboBox(TableLayoutPanel panel, string label, int rotation, Action<int> onChanged, int row)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(lbl, 0, row);

            var combo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            combo.Items.AddRange(new string[] { "0°", "90°", "180°", "270°" });
            combo.SelectedIndex = rotation / 90;
            combo.SelectedIndexChanged += (s, e) => { if (!_updating) onChanged(combo.SelectedIndex * 90); };
            panel.Controls.Add(combo, 1, row);
        }

        private void AddFileSelector(TableLayoutPanel panel, string label, string filter, string currentPath, Action<string> onChanged, int row)
        {
            var lbl = new Label
            {
                Text = label,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(lbl, 0, row);

            var containerPanel = new Panel { Dock = DockStyle.Fill };
            
            var textBox = new TextBox
            {
                Text = currentPath,
                Dock = DockStyle.Fill,
                ReadOnly = true
            };
            
            var button = new Button
            {
                Text = "...",
                Width = 30,
                Dock = DockStyle.Right
            };
            
            button.Click += (s, e) =>
            {
                var dialog = new OpenFileDialog { Filter = filter };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = dialog.FileName;
                    onChanged(dialog.FileName);
                }
            };

            containerPanel.Controls.Add(textBox);
            containerPanel.Controls.Add(button);
            panel.Controls.Add(containerPanel, 1, row);
        }

        private void AddBarcodeProperties(TableLayoutPanel panel, ref int row)
        {
            AddLabel(panel, "Barcode Settings:", row++);
            
            var barcodeNumber = GetElementProperty("BarcodeNumber", "");
            var barcodeType = GetElementProperty("BarcodeType", "CODABAR");
            var scale = GetElementProperty("Scale", 100);

            AddTextInput(panel, "Number:", barcodeNumber, (value) => UpdateElementProperty("BarcodeNumber", value), row++);
            AddComboBox(panel, "Type:", new[] { "CODABAR", "CODE11", "CODE128", "CODE39", "CODE93" }, barcodeType, (value) => UpdateElementProperty("BarcodeType", value), row++);
            AddNumericInput(panel, "Scale (%):", scale, (value) => UpdateElementProperty("Scale", value), row++);
            
            // Show warning for rotated barcodes
            if (_selectedElement != null && _selectedElement.Rotation != 0)
            {
                AddAdvancedFeatureWarning(panel, row++);
            }
        }

        private void AddAdvancedFeatureWarning(TableLayoutPanel panel, int row)
        {
            var warningLabel = new Label
            {
                Text = "⚠️ Rotation requires new JSON format",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.DarkOrange,
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(warningLabel, 0, row);
            panel.SetColumnSpan(warningLabel, 2);
        }

        private void AddLogoProperties(TableLayoutPanel panel, ref int row)
        {
            AddLabel(panel, "Logo Settings:", row++);
            
            var logoPath = GetElementProperty("LogoPath", "");
            var scale = GetElementProperty("Scale", 100);

            AddFileSelector(panel, "Image:", "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp", logoPath, (value) => UpdateElementProperty("LogoPath", value), row++);
            AddNumericInput(panel, "Scale (%):", scale, (value) => UpdateElementProperty("Scale", value), row++);
        }

        private void AddAddressProperties(TableLayoutPanel panel, ref int row)
        {
            AddLabel(panel, "Address Settings:", row++);
            
            var address1 = GetElementProperty("Address1", "");
            var address2 = GetElementProperty("Address2", "");
            var address3 = GetElementProperty("Address3", "");
            var font = GetElementProperty("Font", "Helvetica");
            var fontSize = GetElementProperty("FontSize", 12);
            var alignment = GetElementProperty("Alignment", "Left");
            var color = GetElementProperty("Color", "Black");

            AddTextInput(panel, "Line 1:", address1, (value) => UpdateElementProperty("Address1", value), row++);
            AddTextInput(panel, "Line 2:", address2, (value) => UpdateElementProperty("Address2", value), row++);
            AddTextInput(panel, "Line 3:", address3, (value) => UpdateElementProperty("Address3", value), row++);
            AddComboBox(panel, "Font:", ["Helvetica", "Times New Roman", "Courier"], font, value => UpdateElementProperty("Font", value), row++);
            AddNumericInput(panel, "Font Size:", fontSize, (value) => UpdateElementProperty("FontSize", value), row++);
            AddComboBox(panel, "Text Alignment", ["Left", "Center", "Right"],alignment, (value) => UpdateElementProperty("TextAlignment", value), row++);
            AddComboBox(panel, "Color:", ["Black", "Red", "Blue", "Green"], color, (value) => UpdateElementProperty("Color", value), row++);
        }

        private void AddTextProperties(TableLayoutPanel panel, ref int row)
        {
            AddLabel(panel, "Text Settings:", row++);
            
            var text = GetElementProperty("Text", "");
            var font = GetElementProperty("Font", "");
            var fontSize = GetElementProperty("FontSize", 12);
            var color = GetElementProperty("Color", "Black");

            AddTextInput(panel, "Text:", text, (value) => UpdateElementProperty("Text", value), row++);
            AddComboBox(panel, "Font:", ["Helvetica", "Times New Roman", "Courier"], font, value => UpdateElementProperty("Font", value), row++);
            AddNumericInput(panel, "Font Size:", fontSize, (value) => UpdateElementProperty("FontSize", value), row++);
            AddComboBox(panel, "Color:", ["Black", "Red", "Blue", "Green"], color, (value) => UpdateElementProperty("Color", value), row++);
        }



        private void AddSeparator(TableLayoutPanel panel, int row)
        {
            var separator = new Panel
            {
                Height = 1,
                BackColor = Color.LightGray,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 10)
            };
            panel.Controls.Add(separator, 0, row);
            panel.SetColumnSpan(separator, 2);
        }

        private void AddActionButtons(TableLayoutPanel panel, int row)
        {
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            var deleteButton = new Button
            {
                Text = "Delete",
                BackColor = Color.LightCoral,
                ForeColor = Color.White,
                Size = new Size(80, 30)
            };
            deleteButton.Click += (s, e) => DeleteElement();

            var duplicateButton = new Button
            {
                Text = "Duplicate",
                Size = new Size(80, 30)
            };
            duplicateButton.Click += (s, e) => DuplicateElement();

            buttonPanel.Controls.Add(deleteButton);
            buttonPanel.Controls.Add(duplicateButton);

            panel.Controls.Add(buttonPanel, 0, row);
            panel.SetColumnSpan(buttonPanel, 2);
        }

        private T GetElementProperty<T>(string key, T defaultValue)
        {
            if (_selectedElement?.Properties.TryGetValue(key, out var value) == true && value is T typedValue)
            {
                return typedValue;
            }
            return defaultValue;
        }

        private void UpdateElementProperty(string key, object value)
        {
            if (_selectedElement == null) return;

            // Update element properties
            _selectedElement.Properties[key] = value;

            // Update visual properties
            switch (key)
            {
                case "X":
                    _selectedElement.X = (int)value;
                    break;
                case "Y":
                    _selectedElement.Y = (int)value;
                    break;
                case "Width":
                    _selectedElement.Width = (int)value;
                    break;
                case "Height":
                    _selectedElement.Height = (int)value;
                    break;
                case "Rotation":
                    _selectedElement.Rotation = (int)value;
                    break;
            }

            PropertyChanged?.Invoke(this, new ElementPropertyChangedEventArgs 
            { 
                Element = _selectedElement,
                PropertyName = key,
                NewValue = value
            });
        }

        private void DeleteElement()
        {
            if (_selectedElement != null)
            {
                PropertyChanged?.Invoke(this, new ElementPropertyChangedEventArgs 
                { 
                    Element = _selectedElement,
                    PropertyName = "DELETE",
                    NewValue = true
                });
            }
        }

        private void DuplicateElement()
        {
            if (_selectedElement != null)
            {
                PropertyChanged?.Invoke(this, new ElementPropertyChangedEventArgs 
                { 
                    Element = _selectedElement,
                    PropertyName = "DUPLICATE",
                    NewValue = true
                });
            }
        }
    }

    public class ElementPropertyChangedEventArgs : EventArgs
    {
        public VisualElement Element { get; set; } = null!;
        public string PropertyName { get; set; } = string.Empty;
        public object NewValue { get; set; } = null!;
    }
} 