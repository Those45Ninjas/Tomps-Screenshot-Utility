using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Xml.Linq;

using System.Diagnostics;

namespace tomps_screenshot_utility
{
    public partial class MainForm : Form
    {
        Dictionary<string, Rectangle> screens;
        private string imageUrl;

        public MainForm()
        {
            InitializeComponent();

            saveCaptureDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            screens = new Dictionary<string, Rectangle>();
            int i = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                string name;
                if (screen == Screen.PrimaryScreen)
                    name = String.Format("Display {0} (Primary)", i);
                else
                    name = String.Format("Display {0}", i);

                comboBox1.Items.Add(name);
                screens.Add(name, screen.Bounds);
                i++;
            }

            comboBox1.SelectedIndex = Properties.Settings.Default.lastMode;
        }

        private void TakeScreenshot(bool upload)
        {
            Properties.Settings.Default.lastMode = comboBox1.SelectedIndex;
            Rectangle region;
            string selectedValue = comboBox1.SelectedItem.ToString();

            // Capture Single Display
            if (screens.ContainsKey(selectedValue))
                region = screens[selectedValue];
            // Capture EVERYTHING
            else
                region = SystemInformation.VirtualScreen;

            // Find out where to save the file
            #region Find Save Location
            string fileName;
            if(upload)
            {
                fileName = Path.GetTempFileName();
                FileInfo fileInfo = new FileInfo(fileName);
                fileInfo.Attributes = FileAttributes.Temporary;
            }
            else
            {
                if (saveCaptureDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveCaptureDialog.FileName;
                }
                else
                    return;
            }
            #endregion
            // Do the screen capture process
            Program.CaptureReigonToFile(region, fileName, (upload)?0 : 500) ;

            // Upload the image
            if(upload && !Program.isUploading)
            {
                captureUploadButton.Enabled = false;
                Program.UploadImage(fileName);
                File.Delete(fileName);
            }
        }

        private void captureToFileButton_Click(object sender, EventArgs e)
        {
            TakeScreenshot(false);
        }
        private void captureUploadButton_Click(object sender, EventArgs e)
        {
            TakeScreenshot(true);
        }

        public void UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 100 && e.ProgressPercentage >= 0)
            {
                progressBar.Value = e.ProgressPercentage;
                textBox1.Text = String.Format("Uploading Image {0}%",e.ProgressPercentage);
            }
        }
        public void UploadComplete(object sender, UploadValuesCompletedEventArgs e)
        {
            captureUploadButton.Enabled = true;
            if (e.Error == null)
            {
                progressBar.Value = 100;
                imageUrl = XDocument.Load(new MemoryStream(e.Result)).Element("data").Element("link").Value;
                textBox1.Text = imageUrl;
                notifyIcon.Icon = SystemIcons.Information;
                notifyIcon.ShowBalloonTip(15000, "Screenshot Upload Complete", String.Format("Your screenshot has finished uploading to {0}. Click to view it", imageUrl), ToolTipIcon.Info);
            }
            else
            {
                imageUrl = null;
                notifyIcon.Icon = SystemIcons.Error;
                textBox1.Text = "Upload Failed";
                notifyIcon.ShowBalloonTip(15000, "Screenshot Upload Failed", String.Format("Your screenshot has failed to upload to imgur. Are you connected to the internet? ({0})",e.Error.Message), ToolTipIcon.Info);
            }
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
                Process.Start(imageUrl);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Program.isUploading)
            {
                if(MessageBox.Show("Are you sure you want to stop uploading your image?","Stop Upload?",MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                    e.Cancel = true;
            }
            Properties.Settings.Default.Save();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl) && !Program.isUploading)
                Process.Start(imageUrl);
        }
    }
}
