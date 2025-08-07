using PDF_Template_Generator.Controls;

namespace PDF_Template_Generator.Controls
{
    public partial class ElementToolbar : UserControl
    {
        private ElementType _selectedElementType = ElementType.Barcode;
        private readonly Dictionary<ElementType, Button> _toolButtons = new Dictionary<ElementType, Button>();

        public event EventHandler<ElementTypeSelectedEventArgs>? ElementTypeSelected;
        public event EventHandler<AddElementEventArgs>? AddElementRequested;

        public ElementType SelectedElementType 
        { 
            get => _selectedElementType;
            private set
            {
                _selectedElementType = value;
                UpdateToolSelection();
                ElementTypeSelected?.Invoke(this, new ElementTypeSelectedEventArgs { ElementType = value });
            }
        }

        public ElementToolbar()
        {
            InitializeComponent();
            CreateToolButtons();
            SelectedElementType = ElementType.Barcode; // Set default
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ElementToolbar
            // 
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Dock = DockStyle.Top;
            this.Name = "ElementToolbar";
            this.Padding = new Padding(5);
            this.Size = new Size(800, 60);
            this.ResumeLayout(false);
        }

        private void CreateToolButtons()
        {
            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Padding = new Padding(5)
            };

            // Title label
            var titleLabel = new Label
            {
                Text = "Elements:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(5, 15, 15, 5)
            };
            flowPanel.Controls.Add(titleLabel);

            // Add tool buttons for each element type
            AddToolButton(flowPanel, ElementType.Barcode, "📊\nBarcode", "Add a barcode to the template");
            AddToolButton(flowPanel, ElementType.Logo, "🖼️\nLogo", "Add a logo image to the template");
            AddToolButton(flowPanel, ElementType.Address, "📍\nAddress", "Add address text to the template");
            AddToolButton(flowPanel, ElementType.Text, "📝\nText", "Add custom text to the template");

            // Separator
            var separator = new Panel
            {
                Width = 2,
                Height = 40,
                BackColor = Color.LightGray,
                Margin = new Padding(10, 5, 10, 5)
            };
            flowPanel.Controls.Add(separator);

            // Action buttons
            var previewButton = new Button
            {
                Text = "🔍\nPreview",
                Size = new Size(70, 45),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightBlue,
                Margin = new Padding(5)
            };
            previewButton.Click += (s, e) => PreviewTemplate();
            flowPanel.Controls.Add(previewButton);

            var generateButton = new Button
            {
                Text = "⚡\nGenerate",
                Size = new Size(70, 45),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGreen,
                Margin = new Padding(5)
            };
            generateButton.Click += (s, e) => GenerateTemplate();
            flowPanel.Controls.Add(generateButton);

            // Zoom controls
            var zoomLabel = new Label
            {
                Text = "Zoom:",
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(15, 15, 5, 5)
            };
            flowPanel.Controls.Add(zoomLabel);

            var zoomOutButton = new Button
            {
                Text = "🔍−",
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2)
            };
            zoomOutButton.Click += (s, e) => ChangeZoom(-0.1f);
            flowPanel.Controls.Add(zoomOutButton);

            var zoomInButton = new Button
            {
                Text = "🔍+",
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2)
            };
            zoomInButton.Click += (s, e) => ChangeZoom(0.1f);
            flowPanel.Controls.Add(zoomInButton);

            var fitButton = new Button
            {
                Text = "Fit",
                Size = new Size(40, 30),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(2)
            };
            fitButton.Click += (s, e) => FitToWindow();
            flowPanel.Controls.Add(fitButton);

            this.Controls.Add(flowPanel);
        }

        private void AddToolButton(FlowLayoutPanel parent, ElementType elementType, string text, string tooltip)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(70, 45),
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Margin = new Padding(2),
                Tag = elementType
            };

            button.FlatAppearance.BorderColor = Color.DarkGray;
            button.FlatAppearance.BorderSize = 1;

            // Add tooltip
            var toolTip = new ToolTip();
            toolTip.SetToolTip(button, tooltip);

            button.Click += ToolButton_Click;
            
            _toolButtons[elementType] = button;
            parent.Controls.Add(button);
        }

        private void ToolButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is ElementType elementType)
            {
                SelectedElementType = elementType;
                
                // Provide feedback to user
                var instructionText = GetInstructionText(elementType);
                ShowInstruction(instructionText);
            }
        }

        private void UpdateToolSelection()
        {
            foreach (var kvp in _toolButtons)
            {
                var button = kvp.Value;
                var isSelected = kvp.Key == _selectedElementType;
                
                button.BackColor = isSelected ? Color.LightBlue : Color.White;
                button.ForeColor = isSelected ? Color.DarkBlue : Color.Black;
                button.FlatAppearance.BorderColor = isSelected ? Color.Blue : Color.DarkGray;
                button.FlatAppearance.BorderSize = isSelected ? 2 : 1;
            }
        }

        private string GetInstructionText(ElementType elementType)
        {
            return elementType switch
            {
                ElementType.Barcode => "Click on the PDF preview to place a barcode",
                ElementType.Logo => "Click on the PDF preview to place a logo",
                ElementType.Address => "Click on the PDF preview to place address text",
                ElementType.Text => "Click on the PDF preview to place custom text",
                _ => "Click on the PDF preview to place element"
            };
        }

        private void ShowInstruction(string instruction)
        {
            // Find or create instruction label
            var instructionLabel = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "InstructionLabel");
            
            if (instructionLabel == null)
            {
                instructionLabel = new Label
                {
                    Name = "InstructionLabel",
                    Text = instruction,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.DarkBlue,
                    AutoSize = true,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Location = new Point(this.Width - 300, 20)
                };
                this.Controls.Add(instructionLabel);
                instructionLabel.BringToFront();
            }
            else
            {
                instructionLabel.Text = instruction;
            }

            // Auto-hide after a few seconds
            var timer = new System.Windows.Forms.Timer { Interval = 3000 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                if (instructionLabel.Text == instruction) // Only hide if text hasn't changed
                {
                    instructionLabel.Text = "";
                }
            };
            timer.Start();
        }

        public event EventHandler? PreviewRequested;
        public event EventHandler? GenerateRequested;
        public event EventHandler<ZoomChangeEventArgs>? ZoomChangeRequested;
        public event EventHandler? FitToWindowRequested;

        private void PreviewTemplate()
        {
            PreviewRequested?.Invoke(this, EventArgs.Empty);
        }

        private void GenerateTemplate()
        {
            GenerateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ChangeZoom(float delta)
        {
            ZoomChangeRequested?.Invoke(this, new ZoomChangeEventArgs { ZoomDelta = delta });
        }

        private void FitToWindow()
        {
            FitToWindowRequested?.Invoke(this, EventArgs.Empty);
        }

        public void SetInstruction(string instruction)
        {
            ShowInstruction(instruction);
        }

        public void ClearInstruction()
        {
            var instructionLabel = this.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "InstructionLabel");
            if (instructionLabel != null)
            {
                instructionLabel.Text = "";
            }
        }
    }

    public class ElementTypeSelectedEventArgs : EventArgs
    {
        public ElementType ElementType { get; set; }
    }

    public class AddElementEventArgs : EventArgs
    {
        public ElementType ElementType { get; set; }
        public Point Position { get; set; }
    }

    public class ZoomChangeEventArgs : EventArgs
    {
        public float ZoomDelta { get; set; }
    }
} 