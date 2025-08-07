namespace PDF_Template_Generator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newTemplateToolStripMenuItem = new ToolStripMenuItem();
            openTemplateToolStripMenuItem = new ToolStripMenuItem();
            saveTemplateToolStripMenuItem = new ToolStripMenuItem();
            saveTemplateAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            importLegacyTemplateToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            headerPanel = new Panel();
            lblProductLine = new Label();
            cmbProductLine = new ComboBox();
            lblTemplateName = new Label();
            txtTemplateName = new TextBox();
            sourcePdfPanel = new GroupBox();
            lblSourcePdf = new Label();
            txtSourcePdf = new TextBox();
            btnBrowseSourcePdf = new Button();
            tabControl1 = new TabControl();
            tabLogo = new TabPage();
            chkAddLogo = new CheckBox();
            lblLogoPath = new Label();
            txtLogoPath = new TextBox();
            btnBrowseLogo = new Button();
            lblLogoPosition = new Label();
            cmbLogoPosition = new ComboBox();
            lblLogoX = new Label();
            txtLogoX = new TextBox();
            lblLogoY = new Label();
            txtLogoY = new TextBox();
            lblLogoScale = new Label();
            txtLogoScale = new TextBox();
            tabAddress = new TabPage();
            tabBarcode = new TabPage();
            tabText = new TabPage();
            tabSpecial = new TabPage();
            lblAddress1 = new Label();
            txtAddress1 = new TextBox();
            lblAddress2 = new Label();
            txtAddress2 = new TextBox();
            lblAddress3 = new Label();
            txtAddress3 = new TextBox();
            lblAddressPosition = new Label();
            cmbAddressPosition = new ComboBox();
            lblAddressX = new Label();
            txtAddressX = new TextBox();
            lblAddressY = new Label();
            txtAddressY = new TextBox();
            lblAddressSize = new Label();
            txtAddressSize = new TextBox();
            lblAddressColor = new Label();
            cmbAddressColor = new ComboBox();
            chkAddBarcode = new CheckBox();
            lblBarcodeNumber = new Label();
            txtBarcodeNumber = new TextBox();
            lblBarcodeType = new Label();
            cmbBarcodeType = new ComboBox();
            lblBarcodePosition = new Label();
            cmbBarcodePosition = new ComboBox();
            lblBarcodeX = new Label();
            txtBarcodeX = new TextBox();
            lblBarcodeY = new Label();
            txtBarcodeY = new TextBox();
            lblBarcodeScale = new Label();
            txtBarcodeScale = new TextBox();
            lblBarcodeRotation = new Label();
            cmbBarcodeRotation = new ComboBox();
            chkWhiteBackground = new CheckBox();
            lblText1 = new Label();
            txtText1 = new TextBox();
            lblText1X = new Label();
            txtText1X = new TextBox();
            lblText1Y = new Label();
            txtText1Y = new TextBox();
            lblText1Size = new Label();
            txtText1Size = new TextBox();
            lblText1Color = new Label();
            cmbText1Color = new ComboBox();
            chkAddSpine = new CheckBox();
            actionPanel = new Panel();
            btnGenerate = new Button();
            btnPreview = new Button();
            btnSettings = new Button();
            openFileDialog1 = new OpenFileDialog();
            openFileDialog2 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog2 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            headerPanel.SuspendLayout();
            sourcePdfPanel.SuspendLayout();
            tabControl1.SuspendLayout();
            tabLogo.SuspendLayout();
            actionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newTemplateToolStripMenuItem, openTemplateToolStripMenuItem, saveTemplateToolStripMenuItem, saveTemplateAsToolStripMenuItem, toolStripSeparator1, importLegacyTemplateToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newTemplateToolStripMenuItem
            // 
            newTemplateToolStripMenuItem.Name = "newTemplateToolStripMenuItem";
            newTemplateToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newTemplateToolStripMenuItem.Size = new Size(202, 22);
            newTemplateToolStripMenuItem.Text = "&New Template";
            newTemplateToolStripMenuItem.Click += newTemplateToolStripMenuItem_Click;
            // 
            // openTemplateToolStripMenuItem
            // 
            openTemplateToolStripMenuItem.Name = "openTemplateToolStripMenuItem";
            openTemplateToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openTemplateToolStripMenuItem.Size = new Size(202, 22);
            openTemplateToolStripMenuItem.Text = "&Open Template";
            openTemplateToolStripMenuItem.Click += openTemplateToolStripMenuItem_Click;
            // 
            // saveTemplateToolStripMenuItem
            // 
            saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem";
            saveTemplateToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveTemplateToolStripMenuItem.Size = new Size(202, 22);
            saveTemplateToolStripMenuItem.Text = "&Save Template";
            saveTemplateToolStripMenuItem.Click += saveTemplateToolStripMenuItem_Click;
            // 
            // saveTemplateAsToolStripMenuItem
            // 
            saveTemplateAsToolStripMenuItem.Name = "saveTemplateAsToolStripMenuItem";
            saveTemplateAsToolStripMenuItem.Size = new Size(202, 22);
            saveTemplateAsToolStripMenuItem.Text = "Save Template &As...";
            saveTemplateAsToolStripMenuItem.Click += saveTemplateAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(199, 6);
            // 
            // importLegacyTemplateToolStripMenuItem
            // 
            importLegacyTemplateToolStripMenuItem.Name = "importLegacyTemplateToolStripMenuItem";
            importLegacyTemplateToolStripMenuItem.Size = new Size(202, 22);
            importLegacyTemplateToolStripMenuItem.Text = "&Import Legacy Template";
            importLegacyTemplateToolStripMenuItem.Click += importLegacyTemplateToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(199, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(202, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { preferencesToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "&Settings";
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(135, 22);
            preferencesToolStripMenuItem.Text = "&Preferences";
            preferencesToolStripMenuItem.Click += preferencesToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "&About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(lblProductLine);
            headerPanel.Controls.Add(cmbProductLine);
            headerPanel.Controls.Add(lblTemplateName);
            headerPanel.Controls.Add(txtTemplateName);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 24);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(10);
            headerPanel.Size = new Size(984, 50);
            headerPanel.TabIndex = 1;
            // 
            // lblProductLine
            // 
            lblProductLine.AutoSize = true;
            lblProductLine.Location = new Point(13, 16);
            lblProductLine.Name = "lblProductLine";
            lblProductLine.Size = new Size(77, 15);
            lblProductLine.TabIndex = 0;
            lblProductLine.Text = "Product Line:";
            // 
            // cmbProductLine
            // 
            cmbProductLine.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductLine.FormattingEnabled = true;
            cmbProductLine.Items.AddRange(new object[] { "Playaway", "Launchpad", "WhaZoodle" });
            cmbProductLine.Location = new Point(89, 13);
            cmbProductLine.Name = "cmbProductLine";
            cmbProductLine.Size = new Size(120, 23);
            cmbProductLine.TabIndex = 1;
            cmbProductLine.SelectedIndexChanged += cmbProductLine_SelectedIndexChanged;
            // 
            // lblTemplateName
            // 
            lblTemplateName.AutoSize = true;
            lblTemplateName.Location = new Point(250, 16);
            lblTemplateName.Name = "lblTemplateName";
            lblTemplateName.Size = new Size(94, 15);
            lblTemplateName.TabIndex = 2;
            lblTemplateName.Text = "Template Name:";
            // 
            // txtTemplateName
            // 
            txtTemplateName.Location = new Point(344, 13);
            txtTemplateName.Name = "txtTemplateName";
            txtTemplateName.Size = new Size(200, 23);
            txtTemplateName.TabIndex = 3;
            // 
            // sourcePdfPanel
            // 
            sourcePdfPanel.Controls.Add(lblSourcePdf);
            sourcePdfPanel.Controls.Add(txtSourcePdf);
            sourcePdfPanel.Controls.Add(btnBrowseSourcePdf);
            sourcePdfPanel.Dock = DockStyle.Top;
            sourcePdfPanel.Location = new Point(0, 74);
            sourcePdfPanel.Name = "sourcePdfPanel";
            sourcePdfPanel.Padding = new Padding(10);
            sourcePdfPanel.Size = new Size(984, 60);
            sourcePdfPanel.TabIndex = 2;
            sourcePdfPanel.TabStop = false;
            sourcePdfPanel.Text = "Source PDF";
            // 
            // lblSourcePdf
            // 
            lblSourcePdf.AutoSize = true;
            lblSourcePdf.Location = new Point(13, 26);
            lblSourcePdf.Name = "lblSourcePdf";
            lblSourcePdf.Size = new Size(52, 15);
            lblSourcePdf.TabIndex = 0;
            lblSourcePdf.Text = "PDF File:";
            // 
            // txtSourcePdf
            // 
            txtSourcePdf.Location = new Point(71, 23);
            txtSourcePdf.Name = "txtSourcePdf";
            txtSourcePdf.ReadOnly = true;
            txtSourcePdf.Size = new Size(400, 23);
            txtSourcePdf.TabIndex = 1;
            // 
            // btnBrowseSourcePdf
            // 
            btnBrowseSourcePdf.Location = new Point(477, 22);
            btnBrowseSourcePdf.Name = "btnBrowseSourcePdf";
            btnBrowseSourcePdf.Size = new Size(75, 25);
            btnBrowseSourcePdf.TabIndex = 2;
            btnBrowseSourcePdf.Text = "Browse...";
            btnBrowseSourcePdf.UseVisualStyleBackColor = true;
            btnBrowseSourcePdf.Click += btnBrowseSourcePdf_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabLogo);
            tabControl1.Controls.Add(tabAddress);
            tabControl1.Controls.Add(tabBarcode);
            tabControl1.Controls.Add(tabText);
            tabControl1.Controls.Add(tabSpecial);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 134);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(984, 377);
            tabControl1.TabIndex = 3;
            // 
            // tabLogo
            // 
            tabLogo.Controls.Add(chkAddLogo);
            tabLogo.Controls.Add(lblLogoPath);
            tabLogo.Controls.Add(txtLogoPath);
            tabLogo.Controls.Add(btnBrowseLogo);
            tabLogo.Controls.Add(lblLogoPosition);
            tabLogo.Controls.Add(cmbLogoPosition);
            tabLogo.Controls.Add(lblLogoX);
            tabLogo.Controls.Add(txtLogoX);
            tabLogo.Controls.Add(lblLogoY);
            tabLogo.Controls.Add(txtLogoY);
            tabLogo.Controls.Add(lblLogoScale);
            tabLogo.Controls.Add(txtLogoScale);
            tabLogo.Location = new Point(4, 24);
            tabLogo.Name = "tabLogo";
            tabLogo.Padding = new Padding(20);
            tabLogo.Size = new Size(976, 349);
            tabLogo.TabIndex = 0;
            tabLogo.Text = "Logo";
            tabLogo.UseVisualStyleBackColor = true;
            // 
            // chkAddLogo
            // 
            chkAddLogo.Location = new Point(0, 0);
            chkAddLogo.Name = "chkAddLogo";
            chkAddLogo.Size = new Size(104, 24);
            chkAddLogo.TabIndex = 0;
            // 
            // lblLogoPath
            // 
            lblLogoPath.Location = new Point(0, 0);
            lblLogoPath.Name = "lblLogoPath";
            lblLogoPath.Size = new Size(100, 23);
            lblLogoPath.TabIndex = 1;
            // 
            // txtLogoPath
            // 
            txtLogoPath.Location = new Point(0, 0);
            txtLogoPath.Name = "txtLogoPath";
            txtLogoPath.Size = new Size(100, 23);
            txtLogoPath.TabIndex = 2;
            // 
            // btnBrowseLogo
            // 
            btnBrowseLogo.Location = new Point(0, 0);
            btnBrowseLogo.Name = "btnBrowseLogo";
            btnBrowseLogo.Size = new Size(75, 23);
            btnBrowseLogo.TabIndex = 3;
            // 
            // lblLogoPosition
            // 
            lblLogoPosition.Location = new Point(0, 0);
            lblLogoPosition.Name = "lblLogoPosition";
            lblLogoPosition.Size = new Size(100, 23);
            lblLogoPosition.TabIndex = 4;
            // 
            // cmbLogoPosition
            // 
            cmbLogoPosition.Location = new Point(0, 0);
            cmbLogoPosition.Name = "cmbLogoPosition";
            cmbLogoPosition.Size = new Size(121, 23);
            cmbLogoPosition.TabIndex = 5;
            // 
            // lblLogoX
            // 
            lblLogoX.Location = new Point(0, 0);
            lblLogoX.Name = "lblLogoX";
            lblLogoX.Size = new Size(100, 23);
            lblLogoX.TabIndex = 6;
            // 
            // txtLogoX
            // 
            txtLogoX.Location = new Point(0, 0);
            txtLogoX.Name = "txtLogoX";
            txtLogoX.Size = new Size(100, 23);
            txtLogoX.TabIndex = 7;
            // 
            // lblLogoY
            // 
            lblLogoY.Location = new Point(0, 0);
            lblLogoY.Name = "lblLogoY";
            lblLogoY.Size = new Size(100, 23);
            lblLogoY.TabIndex = 8;
            // 
            // txtLogoY
            // 
            txtLogoY.Location = new Point(0, 0);
            txtLogoY.Name = "txtLogoY";
            txtLogoY.Size = new Size(100, 23);
            txtLogoY.TabIndex = 9;
            // 
            // lblLogoScale
            // 
            lblLogoScale.Location = new Point(0, 0);
            lblLogoScale.Name = "lblLogoScale";
            lblLogoScale.Size = new Size(100, 23);
            lblLogoScale.TabIndex = 10;
            // 
            // txtLogoScale
            // 
            txtLogoScale.Location = new Point(0, 0);
            txtLogoScale.Name = "txtLogoScale";
            txtLogoScale.Size = new Size(100, 23);
            txtLogoScale.TabIndex = 11;
            // 
            // tabAddress
            // 
            tabAddress.Location = new Point(4, 24);
            tabAddress.Name = "tabAddress";
            tabAddress.Size = new Size(976, 332);
            tabAddress.TabIndex = 1;
            // 
            // tabBarcode
            // 
            tabBarcode.Location = new Point(4, 24);
            tabBarcode.Name = "tabBarcode";
            tabBarcode.Size = new Size(976, 332);
            tabBarcode.TabIndex = 2;
            // 
            // tabText
            // 
            tabText.Location = new Point(4, 24);
            tabText.Name = "tabText";
            tabText.Size = new Size(976, 332);
            tabText.TabIndex = 3;
            // 
            // tabSpecial
            // 
            tabSpecial.Location = new Point(4, 24);
            tabSpecial.Name = "tabSpecial";
            tabSpecial.Size = new Size(976, 332);
            tabSpecial.TabIndex = 4;
            // 
            // lblAddress1
            // 
            lblAddress1.Location = new Point(0, 0);
            lblAddress1.Name = "lblAddress1";
            lblAddress1.Size = new Size(100, 23);
            lblAddress1.TabIndex = 0;
            // 
            // txtAddress1
            // 
            txtAddress1.Location = new Point(0, 0);
            txtAddress1.Name = "txtAddress1";
            txtAddress1.Size = new Size(100, 23);
            txtAddress1.TabIndex = 0;
            // 
            // lblAddress2
            // 
            lblAddress2.Location = new Point(0, 0);
            lblAddress2.Name = "lblAddress2";
            lblAddress2.Size = new Size(100, 23);
            lblAddress2.TabIndex = 0;
            // 
            // txtAddress2
            // 
            txtAddress2.Location = new Point(0, 0);
            txtAddress2.Name = "txtAddress2";
            txtAddress2.Size = new Size(100, 23);
            txtAddress2.TabIndex = 0;
            // 
            // lblAddress3
            // 
            lblAddress3.Location = new Point(0, 0);
            lblAddress3.Name = "lblAddress3";
            lblAddress3.Size = new Size(100, 23);
            lblAddress3.TabIndex = 0;
            // 
            // txtAddress3
            // 
            txtAddress3.Location = new Point(0, 0);
            txtAddress3.Name = "txtAddress3";
            txtAddress3.Size = new Size(100, 23);
            txtAddress3.TabIndex = 0;
            // 
            // lblAddressPosition
            // 
            lblAddressPosition.Location = new Point(0, 0);
            lblAddressPosition.Name = "lblAddressPosition";
            lblAddressPosition.Size = new Size(100, 23);
            lblAddressPosition.TabIndex = 0;
            // 
            // cmbAddressPosition
            // 
            cmbAddressPosition.Location = new Point(0, 0);
            cmbAddressPosition.Name = "cmbAddressPosition";
            cmbAddressPosition.Size = new Size(121, 23);
            cmbAddressPosition.TabIndex = 0;
            // 
            // lblAddressX
            // 
            lblAddressX.Location = new Point(0, 0);
            lblAddressX.Name = "lblAddressX";
            lblAddressX.Size = new Size(100, 23);
            lblAddressX.TabIndex = 0;
            // 
            // txtAddressX
            // 
            txtAddressX.Location = new Point(0, 0);
            txtAddressX.Name = "txtAddressX";
            txtAddressX.Size = new Size(100, 23);
            txtAddressX.TabIndex = 0;
            // 
            // lblAddressY
            // 
            lblAddressY.Location = new Point(0, 0);
            lblAddressY.Name = "lblAddressY";
            lblAddressY.Size = new Size(100, 23);
            lblAddressY.TabIndex = 0;
            // 
            // txtAddressY
            // 
            txtAddressY.Location = new Point(0, 0);
            txtAddressY.Name = "txtAddressY";
            txtAddressY.Size = new Size(100, 23);
            txtAddressY.TabIndex = 0;
            // 
            // lblAddressSize
            // 
            lblAddressSize.Location = new Point(0, 0);
            lblAddressSize.Name = "lblAddressSize";
            lblAddressSize.Size = new Size(100, 23);
            lblAddressSize.TabIndex = 0;
            // 
            // txtAddressSize
            // 
            txtAddressSize.Location = new Point(0, 0);
            txtAddressSize.Name = "txtAddressSize";
            txtAddressSize.Size = new Size(100, 23);
            txtAddressSize.TabIndex = 0;
            // 
            // lblAddressColor
            // 
            lblAddressColor.Location = new Point(0, 0);
            lblAddressColor.Name = "lblAddressColor";
            lblAddressColor.Size = new Size(100, 23);
            lblAddressColor.TabIndex = 0;
            // 
            // cmbAddressColor
            // 
            cmbAddressColor.Location = new Point(0, 0);
            cmbAddressColor.Name = "cmbAddressColor";
            cmbAddressColor.Size = new Size(121, 23);
            cmbAddressColor.TabIndex = 0;
            // 
            // chkAddBarcode
            // 
            chkAddBarcode.Location = new Point(0, 0);
            chkAddBarcode.Name = "chkAddBarcode";
            chkAddBarcode.Size = new Size(104, 24);
            chkAddBarcode.TabIndex = 0;
            // 
            // lblBarcodeNumber
            // 
            lblBarcodeNumber.Location = new Point(0, 0);
            lblBarcodeNumber.Name = "lblBarcodeNumber";
            lblBarcodeNumber.Size = new Size(100, 23);
            lblBarcodeNumber.TabIndex = 0;
            // 
            // txtBarcodeNumber
            // 
            txtBarcodeNumber.Location = new Point(0, 0);
            txtBarcodeNumber.Name = "txtBarcodeNumber";
            txtBarcodeNumber.Size = new Size(100, 23);
            txtBarcodeNumber.TabIndex = 0;
            // 
            // lblBarcodeType
            // 
            lblBarcodeType.Location = new Point(0, 0);
            lblBarcodeType.Name = "lblBarcodeType";
            lblBarcodeType.Size = new Size(100, 23);
            lblBarcodeType.TabIndex = 0;
            // 
            // cmbBarcodeType
            // 
            cmbBarcodeType.Location = new Point(0, 0);
            cmbBarcodeType.Name = "cmbBarcodeType";
            cmbBarcodeType.Size = new Size(121, 23);
            cmbBarcodeType.TabIndex = 0;
            // 
            // lblBarcodePosition
            // 
            lblBarcodePosition.Location = new Point(0, 0);
            lblBarcodePosition.Name = "lblBarcodePosition";
            lblBarcodePosition.Size = new Size(100, 23);
            lblBarcodePosition.TabIndex = 0;
            // 
            // cmbBarcodePosition
            // 
            cmbBarcodePosition.Location = new Point(0, 0);
            cmbBarcodePosition.Name = "cmbBarcodePosition";
            cmbBarcodePosition.Size = new Size(121, 23);
            cmbBarcodePosition.TabIndex = 0;
            // 
            // lblBarcodeX
            // 
            lblBarcodeX.Location = new Point(0, 0);
            lblBarcodeX.Name = "lblBarcodeX";
            lblBarcodeX.Size = new Size(100, 23);
            lblBarcodeX.TabIndex = 0;
            // 
            // txtBarcodeX
            // 
            txtBarcodeX.Location = new Point(0, 0);
            txtBarcodeX.Name = "txtBarcodeX";
            txtBarcodeX.Size = new Size(100, 23);
            txtBarcodeX.TabIndex = 0;
            // 
            // lblBarcodeY
            // 
            lblBarcodeY.Location = new Point(0, 0);
            lblBarcodeY.Name = "lblBarcodeY";
            lblBarcodeY.Size = new Size(100, 23);
            lblBarcodeY.TabIndex = 0;
            // 
            // txtBarcodeY
            // 
            txtBarcodeY.Location = new Point(0, 0);
            txtBarcodeY.Name = "txtBarcodeY";
            txtBarcodeY.Size = new Size(100, 23);
            txtBarcodeY.TabIndex = 0;
            // 
            // lblBarcodeScale
            // 
            lblBarcodeScale.Location = new Point(0, 0);
            lblBarcodeScale.Name = "lblBarcodeScale";
            lblBarcodeScale.Size = new Size(100, 23);
            lblBarcodeScale.TabIndex = 0;
            // 
            // txtBarcodeScale
            // 
            txtBarcodeScale.Location = new Point(0, 0);
            txtBarcodeScale.Name = "txtBarcodeScale";
            txtBarcodeScale.Size = new Size(100, 23);
            txtBarcodeScale.TabIndex = 0;
            // 
            // lblBarcodeRotation
            // 
            lblBarcodeRotation.Location = new Point(0, 0);
            lblBarcodeRotation.Name = "lblBarcodeRotation";
            lblBarcodeRotation.Size = new Size(100, 23);
            lblBarcodeRotation.TabIndex = 0;
            // 
            // cmbBarcodeRotation
            // 
            cmbBarcodeRotation.Location = new Point(0, 0);
            cmbBarcodeRotation.Name = "cmbBarcodeRotation";
            cmbBarcodeRotation.Size = new Size(121, 23);
            cmbBarcodeRotation.TabIndex = 0;
            // 
            // chkWhiteBackground
            // 
            chkWhiteBackground.Location = new Point(0, 0);
            chkWhiteBackground.Name = "chkWhiteBackground";
            chkWhiteBackground.Size = new Size(104, 24);
            chkWhiteBackground.TabIndex = 0;
            // 
            // lblText1
            // 
            lblText1.Location = new Point(0, 0);
            lblText1.Name = "lblText1";
            lblText1.Size = new Size(100, 23);
            lblText1.TabIndex = 0;
            // 
            // txtText1
            // 
            txtText1.Location = new Point(0, 0);
            txtText1.Name = "txtText1";
            txtText1.Size = new Size(100, 23);
            txtText1.TabIndex = 0;
            // 
            // lblText1X
            // 
            lblText1X.Location = new Point(0, 0);
            lblText1X.Name = "lblText1X";
            lblText1X.Size = new Size(100, 23);
            lblText1X.TabIndex = 0;
            // 
            // txtText1X
            // 
            txtText1X.Location = new Point(0, 0);
            txtText1X.Name = "txtText1X";
            txtText1X.Size = new Size(100, 23);
            txtText1X.TabIndex = 0;
            // 
            // lblText1Y
            // 
            lblText1Y.Location = new Point(0, 0);
            lblText1Y.Name = "lblText1Y";
            lblText1Y.Size = new Size(100, 23);
            lblText1Y.TabIndex = 0;
            // 
            // txtText1Y
            // 
            txtText1Y.Location = new Point(0, 0);
            txtText1Y.Name = "txtText1Y";
            txtText1Y.Size = new Size(100, 23);
            txtText1Y.TabIndex = 0;
            // 
            // lblText1Size
            // 
            lblText1Size.Location = new Point(0, 0);
            lblText1Size.Name = "lblText1Size";
            lblText1Size.Size = new Size(100, 23);
            lblText1Size.TabIndex = 0;
            // 
            // txtText1Size
            // 
            txtText1Size.Location = new Point(0, 0);
            txtText1Size.Name = "txtText1Size";
            txtText1Size.Size = new Size(100, 23);
            txtText1Size.TabIndex = 0;
            // 
            // lblText1Color
            // 
            lblText1Color.Location = new Point(0, 0);
            lblText1Color.Name = "lblText1Color";
            lblText1Color.Size = new Size(100, 23);
            lblText1Color.TabIndex = 0;
            // 
            // cmbText1Color
            // 
            cmbText1Color.Location = new Point(0, 0);
            cmbText1Color.Name = "cmbText1Color";
            cmbText1Color.Size = new Size(121, 23);
            cmbText1Color.TabIndex = 0;
            // 
            // chkAddSpine
            // 
            chkAddSpine.Location = new Point(0, 0);
            chkAddSpine.Name = "chkAddSpine";
            chkAddSpine.Size = new Size(104, 24);
            chkAddSpine.TabIndex = 0;
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnGenerate);
            actionPanel.Controls.Add(btnPreview);
            actionPanel.Controls.Add(btnSettings);
            actionPanel.Dock = DockStyle.Bottom;
            actionPanel.Location = new Point(0, 511);
            actionPanel.Name = "actionPanel";
            actionPanel.Padding = new Padding(10);
            actionPanel.Size = new Size(984, 50);
            actionPanel.TabIndex = 4;
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGenerate.Location = new Point(886, 13);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(85, 30);
            btnGenerate.TabIndex = 2;
            btnGenerate.Text = "Generate PDF";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnPreview
            // 
            btnPreview.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPreview.Location = new Point(795, 13);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(85, 30);
            btnPreview.TabIndex = 1;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(13, 13);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(85, 30);
            btnSettings.TabIndex = 0;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tabControl1);
            Controls.Add(actionPanel);
            Controls.Add(sourcePdfPanel);
            Controls.Add(headerPanel);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1000, 600);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PDF Template Generator";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            sourcePdfPanel.ResumeLayout(false);
            sourcePdfPanel.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabLogo.ResumeLayout(false);
            tabLogo.PerformLayout();
            actionPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTemplateAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importLegacyTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblProductLine;
        private System.Windows.Forms.ComboBox cmbProductLine;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.TextBox txtTemplateName;
        
        private System.Windows.Forms.GroupBox sourcePdfPanel;
        private System.Windows.Forms.Label lblSourcePdf;
        private System.Windows.Forms.TextBox txtSourcePdf;
        private System.Windows.Forms.Button btnBrowseSourcePdf;
        
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLogo;
        private System.Windows.Forms.TabPage tabAddress;
        private System.Windows.Forms.TabPage tabBarcode;
        private System.Windows.Forms.TabPage tabText;
        private System.Windows.Forms.TabPage tabSpecial;
        
        // Logo Tab Controls
        private System.Windows.Forms.CheckBox chkAddLogo;
        private System.Windows.Forms.Label lblLogoPath;
        private System.Windows.Forms.TextBox txtLogoPath;
        private System.Windows.Forms.Button btnBrowseLogo;
        private System.Windows.Forms.Label lblLogoPosition;
        private System.Windows.Forms.ComboBox cmbLogoPosition;
        private System.Windows.Forms.Label lblLogoX;
        private System.Windows.Forms.TextBox txtLogoX;
        private System.Windows.Forms.Label lblLogoY;
        private System.Windows.Forms.TextBox txtLogoY;
        private System.Windows.Forms.Label lblLogoScale;
        private System.Windows.Forms.TextBox txtLogoScale;
        
        // Address Tab Controls
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lblAddress2;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label lblAddress3;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.Label lblAddressPosition;
        private System.Windows.Forms.ComboBox cmbAddressPosition;
        private System.Windows.Forms.Label lblAddressX;
        private System.Windows.Forms.TextBox txtAddressX;
        private System.Windows.Forms.Label lblAddressY;
        private System.Windows.Forms.TextBox txtAddressY;
        private System.Windows.Forms.Label lblAddressSize;
        private System.Windows.Forms.TextBox txtAddressSize;
        private System.Windows.Forms.Label lblAddressColor;
        private System.Windows.Forms.ComboBox cmbAddressColor;
        
        // Barcode Tab Controls
        private System.Windows.Forms.CheckBox chkAddBarcode;
        private System.Windows.Forms.Label lblBarcodeNumber;
        private System.Windows.Forms.TextBox txtBarcodeNumber;
        private System.Windows.Forms.Label lblBarcodeType;
        private System.Windows.Forms.ComboBox cmbBarcodeType;
        private System.Windows.Forms.Label lblBarcodePosition;
        private System.Windows.Forms.ComboBox cmbBarcodePosition;
        private System.Windows.Forms.Label lblBarcodeX;
        private System.Windows.Forms.TextBox txtBarcodeX;
        private System.Windows.Forms.Label lblBarcodeY;
        private System.Windows.Forms.TextBox txtBarcodeY;
        private System.Windows.Forms.Label lblBarcodeScale;
        private System.Windows.Forms.TextBox txtBarcodeScale;
        private System.Windows.Forms.Label lblBarcodeRotation;
        private System.Windows.Forms.ComboBox cmbBarcodeRotation;
        private System.Windows.Forms.CheckBox chkWhiteBackground;
        
        // Text Tab Controls (Text1 shown as example)
        private System.Windows.Forms.Label lblText1;
        private System.Windows.Forms.TextBox txtText1;
        private System.Windows.Forms.Label lblText1X;
        private System.Windows.Forms.TextBox txtText1X;
        private System.Windows.Forms.Label lblText1Y;
        private System.Windows.Forms.TextBox txtText1Y;
        private System.Windows.Forms.Label lblText1Size;
        private System.Windows.Forms.TextBox txtText1Size;
        private System.Windows.Forms.Label lblText1Color;
        private System.Windows.Forms.ComboBox cmbText1Color;
        
        // Special Tab Controls
        private System.Windows.Forms.CheckBox chkAddSpine;
        
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSettings;
        
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
    }
}
