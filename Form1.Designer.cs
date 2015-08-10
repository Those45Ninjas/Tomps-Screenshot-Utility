namespace tomps_screenshot_utility
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.saveCaptureDialog = new System.Windows.Forms.SaveFileDialog();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.captureToFileButton = new System.Windows.Forms.Button();
            this.captureUploadButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveCaptureDialog
            // 
            this.saveCaptureDialog.DefaultExt = "png";
            this.saveCaptureDialog.FileName = "screenshot";
            this.saveCaptureDialog.Title = "Save Screenshot";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "250 x 250",
            "All Displays",
            "Window",
            "Region"});
            this.comboBox1.Location = new System.Drawing.Point(4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(312, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Capture Mode";
            // 
            // captureToFileButton
            // 
            this.captureToFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captureToFileButton.Location = new System.Drawing.Point(0, 0);
            this.captureToFileButton.Name = "captureToFileButton";
            this.captureToFileButton.Size = new System.Drawing.Size(154, 81);
            this.captureToFileButton.TabIndex = 1;
            this.captureToFileButton.Text = "Capture and save to a file";
            this.captureToFileButton.UseVisualStyleBackColor = true;
            this.captureToFileButton.Click += new System.EventHandler(this.captureToFileButton_Click);
            // 
            // captureUploadButton
            // 
            this.captureUploadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captureUploadButton.Location = new System.Drawing.Point(0, 0);
            this.captureUploadButton.Name = "captureUploadButton";
            this.captureUploadButton.Size = new System.Drawing.Size(154, 81);
            this.captureUploadButton.TabIndex = 2;
            this.captureUploadButton.Text = "Capture and upload to imgur.com";
            this.captureUploadButton.UseVisualStyleBackColor = true;
            this.captureUploadButton.Click += new System.EventHandler(this.captureUploadButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(4, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.captureToFileButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.captureUploadButton);
            this.splitContainer1.Size = new System.Drawing.Size(312, 81);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(4, 106);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(312, 17);
            this.progressBar.TabIndex = 4;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "done";
            this.notifyIcon.BalloonTipTitle = "done the upload";
            this.notifyIcon.Text = "Upload Comlete";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 127);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.progressBar);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "Tomps Screenshot Utility 0.3.0b";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveCaptureDialog;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button captureToFileButton;
        private System.Windows.Forms.Button captureUploadButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

