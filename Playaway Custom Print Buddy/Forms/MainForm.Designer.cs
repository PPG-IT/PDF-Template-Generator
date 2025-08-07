namespace Playaway_Custom_Print_Buddy.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxProductType = new System.Windows.Forms.GroupBox();
            this.radioButtonPlayaway = new System.Windows.Forms.RadioButton();
            this.radioButtonLaunchpad = new System.Windows.Forms.RadioButton();
            this.radioButtonWhazoodle = new System.Windows.Forms.RadioButton();
            this.groupBoxProcessing = new System.Windows.Forms.GroupBox();
            this.textBoxSKU = new System.Windows.Forms.TextBox();
            this.labelSKU = new System.Windows.Forms.Label();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonBatchProcess = new System.Windows.Forms.Button();
            this.listViewJobs = new System.Windows.Forms.ListView();
            this.columnHeaderSKU = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTitle = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.labelPreview = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxProductType.SuspendLayout();
            this.groupBoxProcessing.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewJobs);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxProcessing);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxProductType);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelPreview);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 626);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBoxProductType
            // 
            this.groupBoxProductType.Controls.Add(this.radioButtonWhazoodle);
            this.groupBoxProductType.Controls.Add(this.radioButtonLaunchpad);
            this.groupBoxProductType.Controls.Add(this.radioButtonPlayaway);
            this.groupBoxProductType.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxProductType.Location = new System.Drawing.Point(0, 0);
            this.groupBoxProductType.Name = "groupBoxProductType";
            this.groupBoxProductType.Size = new System.Drawing.Size(400, 80);
            this.groupBoxProductType.TabIndex = 0;
            this.groupBoxProductType.TabStop = false;
            this.groupBoxProductType.Text = "Product Type";
            // 
            // radioButtonPlayaway
            // 
            this.radioButtonPlayaway.AutoSize = true;
            this.radioButtonPlayaway.Checked = true;
            this.radioButtonPlayaway.Location = new System.Drawing.Point(20, 25);
            this.radioButtonPlayaway.Name = "radioButtonPlayaway";
            this.radioButtonPlayaway.Size = new System.Drawing.Size(73, 17);
            this.radioButtonPlayaway.TabIndex = 0;
            this.radioButtonPlayaway.TabStop = true;
            this.radioButtonPlayaway.Text = "Playaway";
            this.radioButtonPlayaway.UseVisualStyleBackColor = true;
            this.radioButtonPlayaway.CheckedChanged += new System.EventHandler(this.ProductTypeRadioButton_CheckedChanged);
            // 
            // radioButtonLaunchpad
            // 
            this.radioButtonLaunchpad.AutoSize = true;
            this.radioButtonLaunchpad.Location = new System.Drawing.Point(120, 25);
            this.radioButtonLaunchpad.Name = "radioButtonLaunchpad";
            this.radioButtonLaunchpad.Size = new System.Drawing.Size(79, 17);
            this.radioButtonLaunchpad.TabIndex = 1;
            this.radioButtonLaunchpad.Text = "Launchpad";
            this.radioButtonLaunchpad.UseVisualStyleBackColor = true;
            this.radioButtonLaunchpad.CheckedChanged += new System.EventHandler(this.ProductTypeRadioButton_CheckedChanged);
            // 
            // radioButtonWhazoodle
            // 
            this.radioButtonWhazoodle.AutoSize = true;
            this.radioButtonWhazoodle.Location = new System.Drawing.Point(220, 25);
            this.radioButtonWhazoodle.Name = "radioButtonWhazoodle";
            this.radioButtonWhazoodle.Size = new System.Drawing.Size(79, 17);
            this.radioButtonWhazoodle.TabIndex = 2;
            this.radioButtonWhazoodle.Text = "Whazoodle";
            this.radioButtonWhazoodle.UseVisualStyleBackColor = true;
            this.radioButtonWhazoodle.CheckedChanged += new System.EventHandler(this.ProductTypeRadioButton_CheckedChanged);
            // 
            // groupBoxProcessing
            // 
            this.groupBoxProcessing.Controls.Add(this.progressBar1);
            this.groupBoxProcessing.Controls.Add(this.buttonBatchProcess);
            this.groupBoxProcessing.Controls.Add(this.buttonProcess);
            this.groupBoxProcessing.Controls.Add(this.labelSKU);
            this.groupBoxProcessing.Controls.Add(this.textBoxSKU);
            this.groupBoxProcessing.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxProcessing.Location = new System.Drawing.Point(0, 80);
            this.groupBoxProcessing.Name = "groupBoxProcessing";
            this.groupBoxProcessing.Size = new System.Drawing.Size(400, 120);
            this.groupBoxProcessing.TabIndex = 1;
            this.groupBoxProcessing.TabStop = false;
            this.groupBoxProcessing.Text = "Processing";
            // 
            // textBoxSKU
            // 
            this.textBoxSKU.Location = new System.Drawing.Point(60, 25);
            this.textBoxSKU.Name = "textBoxSKU";
            this.textBoxSKU.Size = new System.Drawing.Size(200, 20);
            this.textBoxSKU.TabIndex = 0;
            // 
            // labelSKU
            // 
            this.labelSKU.AutoSize = true;
            this.labelSKU.Location = new System.Drawing.Point(20, 28);
            this.labelSKU.Name = "labelSKU";
            this.labelSKU.Size = new System.Drawing.Size(32, 13);
            this.labelSKU.TabIndex = 1;
            this.labelSKU.Text = "SKU:";
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(20, 55);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(80, 25);
            this.buttonProcess.TabIndex = 2;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonBatchProcess
            // 
            this.buttonBatchProcess.Location = new System.Drawing.Point(110, 55);
            this.buttonBatchProcess.Name = "buttonBatchProcess";
            this.buttonBatchProcess.Size = new System.Drawing.Size(100, 25);
            this.buttonBatchProcess.TabIndex = 3;
            this.buttonBatchProcess.Text = "Batch Process";
            this.buttonBatchProcess.UseVisualStyleBackColor = true;
            this.buttonBatchProcess.Click += new System.EventHandler(this.buttonBatchProcess_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(20, 90);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(350, 20);
            this.progressBar1.TabIndex = 4;
            // 
            // listViewJobs
            // 
            this.listViewJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSKU,
            this.columnHeaderTitle,
            this.columnHeaderStatus,
            this.columnHeaderType});
            this.listViewJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewJobs.FullRowSelect = true;
            this.listViewJobs.GridLines = true;
            this.listViewJobs.Location = new System.Drawing.Point(0, 200);
            this.listViewJobs.Name = "listViewJobs";
            this.listViewJobs.Size = new System.Drawing.Size(400, 426);
            this.listViewJobs.TabIndex = 2;
            this.listViewJobs.UseCompatibleStateImageBehavior = false;
            this.listViewJobs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderSKU
            // 
            this.columnHeaderSKU.Text = "SKU";
            this.columnHeaderSKU.Width = 80;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "Title";
            this.columnHeaderTitle.Width = 150;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 80;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 80;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Controls.Add(this.labelPreview);
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(0, 0);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(796, 626);
            this.panelPreview.TabIndex = 0;
            // 
            // labelPreview
            // 
            this.labelPreview.AutoSize = true;
            this.labelPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPreview.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelPreview.Location = new System.Drawing.Point(350, 300);
            this.labelPreview.Name = "labelPreview";
            this.labelPreview.Size = new System.Drawing.Size(96, 20);
            this.labelPreview.TabIndex = 0;
            this.labelPreview.Text = "PDF Preview";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 672);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Playaway Custom Print Buddy";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxProductType.ResumeLayout(false);
            this.groupBoxProductType.PerformLayout();
            this.groupBoxProcessing.ResumeLayout(false);
            this.groupBoxProcessing.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxProductType;
        private System.Windows.Forms.RadioButton radioButtonWhazoodle;
        private System.Windows.Forms.RadioButton radioButtonLaunchpad;
        private System.Windows.Forms.RadioButton radioButtonPlayaway;
        private System.Windows.Forms.GroupBox groupBoxProcessing;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonBatchProcess;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Label labelSKU;
        private System.Windows.Forms.TextBox textBoxSKU;
        private System.Windows.Forms.ListView listViewJobs;
        private System.Windows.Forms.ColumnHeader columnHeaderSKU;
        private System.Windows.Forms.ColumnHeader columnHeaderTitle;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Label labelPreview;
    }
}