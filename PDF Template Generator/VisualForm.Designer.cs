using PDF_Template_Generator.Controls;

namespace PDF_Template_Generator
{
    partial class VisualForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Menu Strip
            this.menuStrip1 = new MenuStrip();
            this.fileToolStripMenuItem = new ToolStripMenuItem();
            this.newTemplateToolStripMenuItem = new ToolStripMenuItem();
            this.openTemplateToolStripMenuItem = new ToolStripMenuItem();
            this.saveTemplateToolStripMenuItem = new ToolStripMenuItem();
            this.saveTemplateAsToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.importLegacyTemplateToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.settingsToolStripMenuItem = new ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new ToolStripMenuItem();
            this.helpToolStripMenuItem = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            
            // Header Panel
            this.headerPanel = new Panel();
            this.lblProductLine = new Label();
            this.cmbProductLine = new ComboBox();
            this.lblTemplateName = new Label();
            this.txtTemplateName = new TextBox();
            this.lblSourcePdf = new Label();
            this.txtSourcePdf = new TextBox();
            this.btnBrowseSourcePdf = new Button();
            this.chkShowSpine = new CheckBox();
            
            // Element Toolbar
            this.elementToolbar = new ElementToolbar();
            
            // Main Split Container
            this.mainSplitContainer = new SplitContainer();
            
            // PDF Preview Control
            this.pdfPreviewControl = new PDFPreviewControl();
            
            // Properties Panel
            this.propertiesPanel = new ElementPropertiesPanel();
            
            // File Dialogs
            this.openFileDialog1 = new OpenFileDialog();
            this.saveFileDialog1 = new SaveFileDialog();
            
            this.menuStrip1.SuspendLayout();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.settingsToolStripMenuItem,
                this.helpToolStripMenuItem});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.newTemplateToolStripMenuItem,
                this.openTemplateToolStripMenuItem,
                this.saveTemplateToolStripMenuItem,
                this.saveTemplateAsToolStripMenuItem,
                this.toolStripSeparator1,
                this.importLegacyTemplateToolStripMenuItem,
                this.toolStripSeparator2,
                this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            
            // Menu items implementation
            this.newTemplateToolStripMenuItem.Name = "newTemplateToolStripMenuItem";
            this.newTemplateToolStripMenuItem.ShortcutKeys = (Keys.Control | Keys.N);
            this.newTemplateToolStripMenuItem.Size = new Size(205, 22);
            this.newTemplateToolStripMenuItem.Text = "&New Template";
            this.newTemplateToolStripMenuItem.Click += new EventHandler(this.newTemplateToolStripMenuItem_Click);
            
            this.openTemplateToolStripMenuItem.Name = "openTemplateToolStripMenuItem";
            this.openTemplateToolStripMenuItem.ShortcutKeys = (Keys.Control | Keys.O);
            this.openTemplateToolStripMenuItem.Size = new Size(205, 22);
            this.openTemplateToolStripMenuItem.Text = "&Open Template";
            this.openTemplateToolStripMenuItem.Click += new EventHandler(this.openTemplateToolStripMenuItem_Click);
            
            this.saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem";
            this.saveTemplateToolStripMenuItem.ShortcutKeys = (Keys.Control | Keys.S);
            this.saveTemplateToolStripMenuItem.Size = new Size(205, 22);
            this.saveTemplateToolStripMenuItem.Text = "&Save Template";
            this.saveTemplateToolStripMenuItem.Click += new EventHandler(this.saveTemplateToolStripMenuItem_Click);
            
            this.saveTemplateAsToolStripMenuItem.Name = "saveTemplateAsToolStripMenuItem";
            this.saveTemplateAsToolStripMenuItem.Size = new Size(205, 22);
            this.saveTemplateAsToolStripMenuItem.Text = "Save Template &As...";
            this.saveTemplateAsToolStripMenuItem.Click += new EventHandler(this.saveTemplateAsToolStripMenuItem_Click);
            
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(202, 6);
            
            this.importLegacyTemplateToolStripMenuItem.Name = "importLegacyTemplateToolStripMenuItem";
            this.importLegacyTemplateToolStripMenuItem.Size = new Size(205, 22);
            this.importLegacyTemplateToolStripMenuItem.Text = "&Import Legacy Template";
            this.importLegacyTemplateToolStripMenuItem.Click += new EventHandler(this.importLegacyTemplateToolStripMenuItem_Click);
            
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(202, 6);
            
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new Size(205, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
            
            // Settings menu
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.preferencesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new EventHandler(this.preferencesToolStripMenuItem_Click);
            
            // Help menu
            this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
            
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.lblProductLine);
            this.headerPanel.Controls.Add(this.cmbProductLine);
            this.headerPanel.Controls.Add(this.lblTemplateName);
            this.headerPanel.Controls.Add(this.txtTemplateName);
            this.headerPanel.Controls.Add(this.lblSourcePdf);
            this.headerPanel.Controls.Add(this.txtSourcePdf);
            this.headerPanel.Controls.Add(this.btnBrowseSourcePdf);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Location = new Point(0, 84);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new Padding(10);
            this.headerPanel.Size = new Size(1200, 70);
            this.headerPanel.TabIndex = 2;
            this.headerPanel.BackColor = Color.FromArgb(250, 250, 250);
            
            // Header controls
            this.lblProductLine.AutoSize = true;
            this.lblProductLine.Location = new Point(13, 16);
            this.lblProductLine.Name = "lblProductLine";
            this.lblProductLine.Size = new Size(70, 15);
            this.lblProductLine.TabIndex = 0;
            this.lblProductLine.Text = "Product Line:";
            
            this.cmbProductLine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProductLine.FormattingEnabled = true;
            this.cmbProductLine.Items.AddRange(new object[] {
                "Playaway",
                "Launchpad", 
                "WhaZoodle"});
            this.cmbProductLine.Location = new Point(89, 13);
            this.cmbProductLine.Name = "cmbProductLine";
            this.cmbProductLine.Size = new Size(120, 23);
            this.cmbProductLine.TabIndex = 1;
            this.cmbProductLine.SelectedIndexChanged += new EventHandler(this.cmbProductLine_SelectedIndexChanged);
            
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.Location = new Point(230, 16);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new Size(88, 15);
            this.lblTemplateName.TabIndex = 2;
            this.lblTemplateName.Text = "Template Name:";
            
            this.txtTemplateName.Location = new Point(324, 13);
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Size = new Size(200, 23);
            this.txtTemplateName.TabIndex = 3;
            
            this.lblSourcePdf.AutoSize = true;
            this.lblSourcePdf.Location = new Point(13, 46);
            this.lblSourcePdf.Name = "lblSourcePdf";
            this.lblSourcePdf.Size = new Size(70, 15);
            this.lblSourcePdf.TabIndex = 4;
            this.lblSourcePdf.Text = "Source PDF:";
            
            this.txtSourcePdf.Location = new Point(89, 43);
            this.txtSourcePdf.Name = "txtSourcePdf";
            this.txtSourcePdf.ReadOnly = true;
            this.txtSourcePdf.Size = new Size(435, 23);
            this.txtSourcePdf.TabIndex = 5;
            
            this.btnBrowseSourcePdf.Location = new Point(530, 42);
            this.btnBrowseSourcePdf.Name = "btnBrowseSourcePdf";
            this.btnBrowseSourcePdf.Size = new Size(75, 25);
            this.btnBrowseSourcePdf.TabIndex = 6;
            this.btnBrowseSourcePdf.Text = "Browse...";
            this.btnBrowseSourcePdf.UseVisualStyleBackColor = true;
            this.btnBrowseSourcePdf.Click += new EventHandler(this.btnBrowseSourcePdf_Click);
            
            this.chkShowSpine.AutoSize = true;
            this.chkShowSpine.Location = new Point(540, 16);
            this.chkShowSpine.Name = "chkShowSpine";
            this.chkShowSpine.Size = new Size(95, 19);
            this.chkShowSpine.TabIndex = 7;
            this.chkShowSpine.Text = "📏 Show Spine";
            this.chkShowSpine.UseVisualStyleBackColor = true;
            this.chkShowSpine.CheckedChanged += new EventHandler(this.chkShowSpine_CheckedChanged);
            
            // 
            // elementToolbar
            // 
            this.elementToolbar.Dock = DockStyle.Top;
            this.elementToolbar.Location = new Point(0, 24);
            this.elementToolbar.Name = "elementToolbar";
            this.elementToolbar.Size = new Size(1200, 60);
            this.elementToolbar.TabIndex = 1;
            
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = DockStyle.Fill;
            this.mainSplitContainer.Location = new Point(0, 154);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = Orientation.Vertical;
            this.mainSplitContainer.Size = new Size(1200, 646);
            this.mainSplitContainer.SplitterDistance = 850;
            this.mainSplitContainer.TabIndex = 3;
            
            // 
            // pdfPreviewControl
            // 
            this.pdfPreviewControl.Dock = DockStyle.Fill;
            this.pdfPreviewControl.Location = new Point(0, 0);
            this.pdfPreviewControl.Name = "pdfPreviewControl";
            this.pdfPreviewControl.Size = new Size(850, 646);
            this.pdfPreviewControl.TabIndex = 0;
            
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Dock = DockStyle.Fill;
            this.propertiesPanel.Location = new Point(0, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new Size(346, 646);
            this.propertiesPanel.TabIndex = 0;
            
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf|Template Files (*.json)|*.json|All Files (*.*)|*.*";
            
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Template Files (*.json)|*.json";
            
            // Add controls to containers
            this.headerPanel.Controls.Add(this.chkShowSpine);
            this.headerPanel.Controls.Add(this.btnBrowseSourcePdf);
            this.headerPanel.Controls.Add(this.txtSourcePdf);
            this.headerPanel.Controls.Add(this.lblSourcePdf);
            this.headerPanel.Controls.Add(this.txtTemplateName);
            this.headerPanel.Controls.Add(this.lblTemplateName);
            this.headerPanel.Controls.Add(this.cmbProductLine);
            this.headerPanel.Controls.Add(this.lblProductLine);
            
            this.mainSplitContainer.Panel1.Controls.Add(this.pdfPreviewControl);
            this.mainSplitContainer.Panel2.Controls.Add(this.propertiesPanel);
            
            // 
            // VisualForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.elementToolbar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new Size(1000, 600);
            this.Name = "VisualForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "PDF Template Generator - Visual Designer";
            this.WindowState = FormWindowState.Maximized;
            
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newTemplateToolStripMenuItem;
        private ToolStripMenuItem openTemplateToolStripMenuItem;
        private ToolStripMenuItem saveTemplateToolStripMenuItem;
        private ToolStripMenuItem saveTemplateAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem importLegacyTemplateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        
        private Panel headerPanel;
        private Label lblProductLine;
        private ComboBox cmbProductLine;
        private Label lblTemplateName;
        private TextBox txtTemplateName;
        private Label lblSourcePdf;
        private TextBox txtSourcePdf;
        private Button btnBrowseSourcePdf;
        private CheckBox chkShowSpine;
        
        private ElementToolbar elementToolbar;
        private SplitContainer mainSplitContainer;
        private PDFPreviewControl pdfPreviewControl;
        private ElementPropertiesPanel propertiesPanel;
        
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
    }
} 