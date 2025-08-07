using System.Drawing.Imaging;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PDF_Template_Generator.Models;

namespace PDF_Template_Generator.Controls
{
    public partial class PDFPreviewControl : UserControl
    {
        private string _pdfPath = string.Empty;
        private Bitmap? _pdfImage;
        private List<VisualElement> _elements = new List<VisualElement>();
        private VisualElement? _selectedElement;
        private bool _isDragging = false;
        private Point _dragStartPoint;
        private float _zoomFactor = 1.0f;
        private Point _panOffset = Point.Empty;
        
        // Track actual PDF dimensions and scaling
        private float _actualPdfWidth = 0;
        private float _actualPdfHeight = 0;
        private float _previewScale = 1.0f;
        private bool _showSpineOverlay = false;
        private ProductLine _currentProductLine = ProductLine.Playaway;

        public event EventHandler<ElementClickedEventArgs>? ElementClicked;
        public event EventHandler<ElementPositionChangedEventArgs>? ElementPositionChanged;
        public event EventHandler<ElementSelectedEventArgs>? ElementSelected;

        public string PdfPath
        {
            get => _pdfPath;
            set
            {
                _pdfPath = value;
                LoadPDF();
            }
        }

        public float ZoomFactor
        {
            get => _zoomFactor;
            set
            {
                _zoomFactor = Math.Max(0.1f, Math.Min(5.0f, value));
                Invalidate();
            }
        }

        public PDFPreviewControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            BackColor = Color.LightGray;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PDFPreviewControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "PDFPreviewControl";
            this.Size = new Size(800, 600);
            this.Paint += PDFPreviewControl_Paint;
            this.MouseDown += PDFPreviewControl_MouseDown;
            this.MouseMove += PDFPreviewControl_MouseMove;
            this.MouseUp += PDFPreviewControl_MouseUp;
            this.MouseWheel += PDFPreviewControl_MouseWheel;
            this.ResumeLayout(false);
        }

        private void LoadPDF()
        {
            if (string.IsNullOrEmpty(_pdfPath) || !File.Exists(_pdfPath))
            {
                _pdfImage?.Dispose();
                _pdfImage = null;
                Invalidate();
                return;
            }

            try
            {
                // Convert first page of PDF to bitmap for preview
                _pdfImage = ConvertPDFToBitmap(_pdfPath);
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF: {ex.Message}", "PDF Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Bitmap? ConvertPDFToBitmap(string pdfPath)
        {
            try
            {
                using (var reader = new PdfReader(pdfPath))
                {
                    var pageSize = reader.GetPageSizeWithRotation(1);
                    
                    // Store actual PDF dimensions
                    _actualPdfWidth = pageSize.Width;
                    _actualPdfHeight = pageSize.Height;
                    
                    int width = (int)pageSize.Width;
                    int height = (int)pageSize.Height;

                    // Calculate scale factor for preview (limit to reasonable size)
                    _previewScale = 1.0f;
                    if (width > 1200 || height > 1600)
                    {
                        _previewScale = Math.Min(1200f / width, 1600f / height);
                        width = (int)(width * _previewScale);
                        height = (int)(height * _previewScale);
                    }

                    var bitmap = new Bitmap(width, height);
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        // Create a clean PDF preview background
                        graphics.Clear(Color.White);
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        // Draw page border
                        var borderPen = new Pen(Color.DarkGray, 2);
                        graphics.DrawRectangle(borderPen, 1, 1, width - 3, height - 3);

                        // Draw subtle grid to help with positioning
                        var gridPen = new Pen(Color.FromArgb(230, 230, 230), 1);
                        int gridSize = 50;
                        for (int x = gridSize; x < width; x += gridSize)
                        {
                            graphics.DrawLine(gridPen, x, 0, x, height);
                        }
                        for (int y = gridSize; y < height; y += gridSize)
                        {
                            graphics.DrawLine(gridPen, 0, y, width, y);
                        }

                        // Draw corner guides
                        var guideBrush = new SolidBrush(Color.FromArgb(200, 200, 200));
                        int guideSize = 20;
                        graphics.FillRectangle(guideBrush, 0, 0, guideSize, guideSize);
                        graphics.FillRectangle(guideBrush, width - guideSize, 0, guideSize, guideSize);
                        graphics.FillRectangle(guideBrush, 0, height - guideSize, guideSize, guideSize);
                        graphics.FillRectangle(guideBrush, width - guideSize, height - guideSize, guideSize, guideSize);

                        // Draw PDF representation with more visual cues
                        var font = new Font("Segoe UI", 12, FontStyle.Bold);
                        var textBrush = new SolidBrush(Color.FromArgb(100, 100, 100));
                        string pdfInfo = $"{System.IO.Path.GetFileName(pdfPath)}";
                        string dimensions = $"{(int)(pageSize.Width)} x {(int)(pageSize.Height)} pts";
                        
                        var textSize = graphics.MeasureString(pdfInfo, font);
                        graphics.DrawString(pdfInfo, font, textBrush, (width - textSize.Width) / 2, 30);
                        
                        var dimFont = new Font("Segoe UI", 10);
                        var dimSize = graphics.MeasureString(dimensions, dimFont);
                        graphics.DrawString(dimensions, dimFont, textBrush, (width - dimSize.Width) / 2, 55);

                        // Draw common positioning areas for reference
                        var areaPen = new Pen(Color.FromArgb(180, 180, 180), 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                        
                        // Top area (typical for logos/text)
                        var topArea = new Rectangle(20, 80, width - 40, 60);
                        graphics.DrawRectangle(areaPen, topArea);
                        graphics.DrawString("Header Area", new Font("Segoe UI", 8), Brushes.Gray, topArea.X + 5, topArea.Y + 5);
                        
                        // Right area (typical for barcodes)
                        var rightArea = new Rectangle(width - 150, 150, 130, height - 300);
                        graphics.DrawRectangle(areaPen, rightArea);
                        graphics.DrawString("Barcode\nArea", new Font("Segoe UI", 8), Brushes.Gray, rightArea.X + 5, rightArea.Y + 5);
                        
                        // Bottom area (typical for address)
                        var bottomArea = new Rectangle(20, height - 120, width - 40, 100);
                        graphics.DrawRectangle(areaPen, bottomArea);
                        graphics.DrawString("Footer/Address Area", new Font("Segoe UI", 8), Brushes.Gray, bottomArea.X + 5, bottomArea.Y + 5);
                        
                        // Left spine area (for spine barcodes)
                        var spineArea = new Rectangle(5, 150, 30, height - 300);
                        graphics.DrawRectangle(areaPen, spineArea);
                        
                        // Add text rotation indicator for spine
                        var transform = graphics.Transform;
                        graphics.TranslateTransform(spineArea.X + 15, spineArea.Y + 50);
                        graphics.RotateTransform(90);
                        graphics.DrawString("Spine", new Font("Segoe UI", 8), Brushes.Gray, 0, 0);
                        graphics.Transform = transform;
                        
                        areaPen.Dispose();

                        // Add positioning hints and coordinate info
                        var hintFont = new Font("Segoe UI", 9, FontStyle.Italic);
                        var hintBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
                        graphics.DrawString("Click to place elements", hintFont, hintBrush, 10, height - 25);
                        
                        // Show scale information for debugging
                        var scaleText = $"Scale: {_previewScale:F2} | PDF: {_actualPdfWidth:F0}x{_actualPdfHeight:F0}pt";
                        graphics.DrawString(scaleText, hintFont, hintBrush, width - 250, height - 25);

                        borderPen.Dispose();
                        gridPen.Dispose();
                        guideBrush.Dispose();
                        font.Dispose();
                        textBrush.Dispose();
                        dimFont.Dispose();
                        hintFont.Dispose();
                        hintBrush.Dispose();
                    }
                    return bitmap;
                }
            }
            catch (Exception ex)
            {
                // Create a simple error representation
                var bitmap = new Bitmap(800, 600);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.LightGray);
                    graphics.DrawRectangle(Pens.Red, 10, 10, 780, 580);
                    graphics.DrawString("PDF Load Error", new Font("Arial", 16, FontStyle.Bold), Brushes.Red, 50, 50);
                    graphics.DrawString($"File: {System.IO.Path.GetFileName(pdfPath)}", new Font("Arial", 12), Brushes.Black, 50, 80);
                    graphics.DrawString($"Error: {ex.Message}", new Font("Arial", 10), Brushes.Gray, 50, 110);
                    graphics.DrawString("You can still place elements by clicking", new Font("Arial", 10, FontStyle.Italic), Brushes.Blue, 50, 140);
                }
                return bitmap;
            }
        }

        private void PDFPreviewControl_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);

            if (_pdfImage == null) return;

            // Calculate the actual display rectangle for the PDF
            var pdfDisplayWidth = (int)(_pdfImage.Width * _zoomFactor);
            var pdfDisplayHeight = (int)(_pdfImage.Height * _zoomFactor);
            var pdfDisplayRect = new Rectangle(_panOffset.X, _panOffset.Y, pdfDisplayWidth, pdfDisplayHeight);

            // Draw PDF background
            g.DrawImage(_pdfImage, pdfDisplayRect);

            // Draw spine overlay if enabled
            if (_showSpineOverlay)
            {
                DrawSpineOverlay(g);
            }

            // Draw visual elements (no transform needed, we calculate screen coordinates directly)
            foreach (var element in _elements)
            {
                DrawVisualElement(g, element);
            }

            // Draw selection handles for selected element
            if (_selectedElement != null)
            {
                DrawSelectionHandles(g, _selectedElement);
            }

            // Draw coordinate info (no transform applied to UI text)
            if (_selectedElement != null)
            {
                var coordText = $"PDF Coords: ({_selectedElement.X}, {_selectedElement.Y}) Size: {_selectedElement.Width}x{_selectedElement.Height}";
                var coordFont = new Font("Segoe UI", 10, FontStyle.Bold);
                var coordBrush = new SolidBrush(Color.Blue);
                g.DrawString(coordText, coordFont, coordBrush, 10, 10);
                coordFont.Dispose();
                coordBrush.Dispose();
            }

            // Draw scale info
            var scaleText = $"Zoom: {_zoomFactor:F1}x | Scale: {_previewScale:F2} | PDF: {_actualPdfWidth:F0}x{_actualPdfHeight:F0}pt";
            var scaleFont = new Font("Segoe UI", 9, FontStyle.Italic);
            var scaleBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
            g.DrawString(scaleText, scaleFont, scaleBrush, this.Width - 300, this.Height - 25);
            scaleFont.Dispose();
            scaleBrush.Dispose();
        }

        private void DrawVisualElement(Graphics g, VisualElement element)
        {
            // Convert PDF coordinates to screen coordinates for drawing
            var screenPos = PdfToScreenCoordinates(new Point(element.X, element.Y));
            var screenWidth = (int)(element.Width * _previewScale * _zoomFactor);
            var screenHeight = (int)(element.Height * _previewScale * _zoomFactor);
            
            var rect = new Rectangle(screenPos.X, screenPos.Y, screenWidth, screenHeight);
            
            // Use more distinct colors and thicker borders for better visibility
            Brush brush;
            Pen pen;
            
            if (element.IsSelected)
            {
                brush = new SolidBrush(Color.FromArgb(180, 173, 216, 230)); // Light blue with transparency
                pen = new Pen(Color.Blue, 3); // Thicker blue border
            }
            else
            {
                brush = new SolidBrush(Color.FromArgb(180, 255, 255, 224)); // Light yellow with transparency
                pen = new Pen(Color.DarkOrange, 2); // Orange border for visibility
            }

            // Draw element background with rounded corners effect
            g.FillRectangle(brush, rect);
            g.DrawRectangle(pen, rect);
            
            // Add a subtle inner border for depth
            if (element.IsSelected)
            {
                var innerPen = new Pen(Color.FromArgb(100, Color.White), 1);
                g.DrawRectangle(innerPen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
                innerPen.Dispose();
            }

            // Draw element icon/text (scale font size with zoom)
            var fontSize = Math.Max(8, (int)(10 * _zoomFactor));
            var font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            var textBrush = new SolidBrush(element.IsSelected ? Color.DarkBlue : Color.DarkRed);
            string displayText = GetElementDisplayText(element);
            
            // Add text background for better readability
            var textSize = g.MeasureString(displayText, font);
            var textX = rect.X + (rect.Width - textSize.Width) / 2;
            var textY = rect.Y + (rect.Height - textSize.Height) / 2;
            
            var textBgRect = new RectangleF(textX - 2, textY - 1, textSize.Width + 4, textSize.Height + 2);
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.White)), textBgRect);
            
            g.DrawString(displayText, font, textBrush, textX, textY);

            // Draw rotation indicator if rotated
            if (element.Rotation != 0)
            {
                var rotFont = new Font("Segoe UI", Math.Max(7, (int)(9 * _zoomFactor)), FontStyle.Bold);
                var rotText = $"↻{element.Rotation}°";
                var rotSize = g.MeasureString(rotText, rotFont);
                var rotBg = new RectangleF(rect.Right - rotSize.Width - 2, rect.Y, rotSize.Width + 2, rotSize.Height);
                g.FillRectangle(new SolidBrush(Color.FromArgb(220, Color.Red)), rotBg);
                g.DrawString(rotText, rotFont, Brushes.White, rect.Right - rotSize.Width - 1, rect.Y + 1);
                rotFont.Dispose();
            }

            // Draw coordinate info for selected element
            if (element.IsSelected)
            {
                var coordFont = new Font("Segoe UI", Math.Max(7, (int)(8 * _zoomFactor)));
                var coordText = $"({element.X}, {element.Y})";
                var coordSize = g.MeasureString(coordText, coordFont);
                var coordBg = new RectangleF(rect.X, rect.Bottom + 2, coordSize.Width + 4, coordSize.Height + 2);
                g.FillRectangle(new SolidBrush(Color.FromArgb(220, Color.Blue)), coordBg);
                g.DrawString(coordText, coordFont, Brushes.White, rect.X + 2, rect.Bottom + 3);
                coordFont.Dispose();
            }

            font.Dispose();
            textBrush.Dispose();
            brush.Dispose();
            pen.Dispose();
        }

        private void DrawSelectionHandles(Graphics g, VisualElement element)
        {
            // Convert PDF coordinates to screen coordinates for drawing
            var screenPos = PdfToScreenCoordinates(new Point(element.X, element.Y));
            var screenWidth = (int)(element.Width * _previewScale * _zoomFactor);
            var screenHeight = (int)(element.Height * _previewScale * _zoomFactor);
            
            var rect = new Rectangle(screenPos.X, screenPos.Y, screenWidth, screenHeight);
            var handleSize = Math.Max(4, (int)(6 * _zoomFactor));
            var handleBrush = Brushes.Blue;

            // Corner handles
            g.FillRectangle(handleBrush, rect.X - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
            g.FillRectangle(handleBrush, rect.Right - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
            g.FillRectangle(handleBrush, rect.X - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
            g.FillRectangle(handleBrush, rect.Right - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
        }

        private string GetElementDisplayText(VisualElement element)
        {
            return element.Type switch
            {
                ElementType.Barcode => "📊 Barcode",
                ElementType.Logo => "🖼️ Logo",
                ElementType.Address => "📍 Address",
                ElementType.Text => $"📝 {element.Name}",
                _ => "Element"
            };
        }

        private void PDFPreviewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // Check if clicking on an existing element first (using screen coordinates for more accurate hit testing)
            var clickedElement = GetElementAtScreenPoint(e.Location);
            
            if (clickedElement != null)
            {
                // Select and start dragging
                SelectElement(clickedElement);
                _isDragging = true;
                _dragStartPoint = e.Location;
            }
            else
            {
                // Clear current selection when clicking on empty space
                if (_selectedElement != null)
                {
                    _selectedElement.IsSelected = false;
                    _selectedElement = null;
                    Invalidate();
                    
                    // Notify that selection was cleared
                    ElementSelected?.Invoke(this, new ElementSelectedEventArgs { Element = null! });
                }
                
                // New element placement - convert to PDF coordinates
                var pdfPoint = ScreenToPdfCoordinates(e.Location);
                ElementClicked?.Invoke(this, new ElementClickedEventArgs { Position = pdfPoint });
            }
        }

        private void PDFPreviewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _selectedElement != null)
            {
                this.Cursor = Cursors.SizeAll; // Show move cursor when dragging
                
                // Calculate delta in PDF coordinates
                var currentPdfPoint = ScreenToPdfCoordinates(e.Location);
                var startPdfPoint = ScreenToPdfCoordinates(_dragStartPoint);
                
                var deltaX = currentPdfPoint.X - startPdfPoint.X;
                var deltaY = currentPdfPoint.Y - startPdfPoint.Y;
                
                _selectedElement.X += deltaX;
                _selectedElement.Y += deltaY;
                
                _dragStartPoint = e.Location;
                
                ElementPositionChanged?.Invoke(this, new ElementPositionChangedEventArgs 
                { 
                    Element = _selectedElement,
                    NewX = _selectedElement.X,
                    NewY = _selectedElement.Y
                });
                
                Invalidate();
            }
            else
            {
                // Change cursor based on what's under the mouse
                var elementUnderMouse = GetElementAtScreenPoint(e.Location);
                if (elementUnderMouse != null)
                {
                    this.Cursor = Cursors.Hand; // Show hand cursor when over an element
                }
                else
                {
                    this.Cursor = Cursors.Cross; // Show cross cursor for placement
                }
            }
        }

        private void PDFPreviewControl_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            
            // Reset cursor after dragging
            var elementUnderMouse = GetElementAtScreenPoint(e.Location);
            if (elementUnderMouse != null)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Cross;
            }
        }

        private void PDFPreviewControl_MouseWheel(object sender, MouseEventArgs e)
        {
            // Zoom in/out
            var zoomDelta = e.Delta > 0 ? 1.1f : 0.9f;
            ZoomFactor *= zoomDelta;
        }

        private Point ScreenToPdfCoordinates(Point screenPoint)
        {
            // Convert screen coordinates to legacy PDF coordinates
            // First, account for zoom and pan to get preview coordinates
            var previewX = (screenPoint.X - _panOffset.X) / _zoomFactor;
            var previewY = (screenPoint.Y - _panOffset.Y) / _zoomFactor;
            
            // Convert from preview coordinates to legacy PDF coordinates
            // Flip Y back to legacy "distance from top" format
            var pdfX = (int)(previewX / _previewScale);
            var pdfY = (int)(_actualPdfHeight - (previewY / _previewScale));
            
            return new Point(pdfX, pdfY);
        }

        private Point PdfToScreenCoordinates(Point pdfPoint)
        {
            // Convert legacy coordinates to screen coordinates for visual display
            // Legacy coordinates are "distance from top" but PDF places them "distance from bottom"
            // For visual accuracy, flip Y to show where elements will actually appear in final PDF
            var previewX = pdfPoint.X * _previewScale;
            var previewY = (_actualPdfHeight - pdfPoint.Y) * _previewScale; // Flip Y to match final PDF placement
            
            // Then apply zoom and pan to get screen coordinates
            var screenX = (int)(previewX * _zoomFactor + _panOffset.X);
            var screenY = (int)(previewY * _zoomFactor + _panOffset.Y);
            
            return new Point(screenX, screenY);
        }

        private VisualElement? GetElementAtPoint(Point point)
        {
            // Convert screen point to PDF coordinates for comparison
            var pdfPoint = ScreenToPdfCoordinates(point);
            
            // Check elements in reverse order (top to bottom)
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                var element = _elements[i];
                var rect = new Rectangle(element.X, element.Y, element.Width, element.Height);
                if (rect.Contains(pdfPoint))
                {
                    return element;
                }
            }
            return null;
        }

        private void SelectElement(VisualElement element)
        {
            // Deselect previous
            if (_selectedElement != null)
                _selectedElement.IsSelected = false;

            // Select new
            _selectedElement = element;
            element.IsSelected = true;
            
            ElementSelected?.Invoke(this, new ElementSelectedEventArgs { Element = element });
            Invalidate();
        }

        public void AddElement(VisualElement element)
        {
            _elements.Add(element);
            SelectElement(element);
            Invalidate();
        }

        public void RemoveElement(VisualElement element)
        {
            _elements.Remove(element);
            if (_selectedElement == element)
                _selectedElement = null;
            Invalidate();
        }

        public void ClearElements()
        {
            _elements.Clear();
            _selectedElement = null;
            Invalidate();
        }

        public List<VisualElement> GetElements()
        {
            return new List<VisualElement>(_elements);
        }

        public (float width, float height) GetActualPdfDimensions()
        {
            return (_actualPdfWidth, _actualPdfHeight);
        }

        public float GetPreviewScale()
        {
            return _previewScale;
        }

        public void SetPanOffset(Point offset)
        {
            _panOffset = offset;
            Invalidate();
        }

        public Point GetPanOffset()
        {
            return _panOffset;
        }

        public void SetSpineOverlay(bool showSpine, ProductLine productLine)
        {
            _showSpineOverlay = showSpine;
            _currentProductLine = productLine;
            Invalidate();
        }

        private void DrawSpineOverlay(Graphics g)
        {
            // Get spine position based on product line (using legacy code positioning)
            int spineX, spineY, spineWidth, spineHeight;
            
            switch (_currentProductLine)
            {
                case ProductLine.WhaZoodle:
                    spineX = 455;
                    spineY = 52;
                    spineWidth = 100;
                    spineHeight = 200;
                    break;
                case ProductLine.Launchpad:
                    spineX = 600;
                    spineY = 52;
                    spineWidth = 100;
                    spineHeight = 200;
                    break;
                case ProductLine.Playaway:
                default:
                    spineX = 308;
                    spineY = 49;
                    spineWidth = 100;
                    spineHeight = 200;
                    break;
            }

            // Convert spine position to screen coordinates using the same method as all other elements
            // Spine coordinates are in legacy PDF coordinate system (bottom-left anchor point)
            // Use the same conversion as regular elements to ensure consistent positioning
            var spineScreenPos = PdfToScreenCoordinates(new Point(spineX, spineY));
            var spineScreenWidth = (int)(spineWidth * _previewScale * _zoomFactor);
            var spineScreenHeight = (int)(spineHeight * _previewScale * _zoomFactor);
            
            var spineRect = new Rectangle(spineScreenPos.X, spineScreenPos.Y, spineScreenWidth, spineScreenHeight);
            
            // Draw spine overlay with distinctive styling
            var spineBrush = new SolidBrush(Color.FromArgb(120, 255, 140, 0)); // Semi-transparent orange
            var spinePen = new Pen(Color.DarkOrange, 3) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            
            g.FillRectangle(spineBrush, spineRect);
            g.DrawRectangle(spinePen, spineRect);
            
            // Add spine label
            var labelFont = new Font("Segoe UI", Math.Max(8, (int)(10 * _zoomFactor)), FontStyle.Bold);
            var labelBrush = new SolidBrush(Color.DarkOrange);
            var labelText = "📏 SPINE";
            var labelSize = g.MeasureString(labelText, labelFont);
            
            var labelX = spineRect.X + (spineRect.Width - labelSize.Width) / 2;
            var labelY = spineRect.Y + (spineRect.Height - labelSize.Height) / 2;
            
            // Add background for text
            var labelBg = new RectangleF(labelX - 2, labelY - 1, labelSize.Width + 4, labelSize.Height + 2);
            g.FillRectangle(new SolidBrush(Color.FromArgb(220, Color.White)), labelBg);
            
            g.DrawString(labelText, labelFont, labelBrush, labelX, labelY);
            
            // Add product line indicator
            var productFont = new Font("Segoe UI", Math.Max(6, (int)(8 * _zoomFactor)), FontStyle.Italic);
            var productText = _currentProductLine.ToString();
            var productSize = g.MeasureString(productText, productFont);
            
            var productBg = new RectangleF(spineRect.X, spineRect.Bottom - productSize.Height - 2, productSize.Width + 4, productSize.Height + 2);
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.DarkOrange)), productBg);
            g.DrawString(productText, productFont, Brushes.White, spineRect.X + 2, spineRect.Bottom - productSize.Height);
            
            // Cleanup
            spineBrush.Dispose();
            spinePen.Dispose();
            labelFont.Dispose();
            labelBrush.Dispose();
            productFont.Dispose();
        }

        private VisualElement? GetElementAtScreenPoint(Point screenPoint)
        {
            // Check elements in reverse order (top to bottom) using screen coordinates for precise hit testing
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                var element = _elements[i];
                
                // Calculate the screen rectangle for this element
                var screenPos = PdfToScreenCoordinates(new Point(element.X, element.Y));
                var screenWidth = (int)(element.Width * _previewScale * _zoomFactor);
                var screenHeight = (int)(element.Height * _previewScale * _zoomFactor);
                
                var screenRect = new Rectangle(screenPos.X, screenPos.Y, screenWidth, screenHeight);
                
                // Add a bit of padding to make selection easier
                screenRect.Inflate(5, 5);
                
                if (screenRect.Contains(screenPoint))
                {
                    return element;
                }
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pdfImage?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    // Supporting classes for the visual preview
    public class VisualElement
    {
        public ElementType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 100;
        public int Height { get; set; } = 50;
        public int Rotation { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }

    public enum ElementType
    {
        Barcode,
        Logo,
        Address,
        Text
    }

    public class ElementClickedEventArgs : EventArgs
    {
        public Point Position { get; set; }
    }

    public class ElementPositionChangedEventArgs : EventArgs
    {
        public VisualElement Element { get; set; } = null!;
        public int NewX { get; set; }
        public int NewY { get; set; }
    }

    public class ElementSelectedEventArgs : EventArgs
    {
        public VisualElement Element { get; set; } = null!;
    }
} 