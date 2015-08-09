using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Xml.Linq;

using System.Diagnostics;

namespace tomps_screenshot_utility
{
    public partial class Form1 : Form
    {
        Dictionary<string, Rectangle> screens;
        private string imageUrl;

        public Form1()
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
        }

        private Bitmap CaptureReigon(Rectangle rect)
        {
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, new Size(rect.Width, rect.Height));
            }
            return bitmap;
        }
        private void Capture(bool upload)
        {
            this.WindowState = FormWindowState.Minimized;

            // Capture the image
            Bitmap bitmap;

            string selectedValue = comboBox1.SelectedItem.ToString();
            if (screens.ContainsKey(selectedValue))
                bitmap = CaptureReigon(screens[selectedValue]);
            else if (selectedValue == "Window")
                bitmap = CaptureReigon(new Rectangle(Screen.PrimaryScreen.Bounds.Location,new Size(64,64)));
            else if (selectedValue == "Region")
                bitmap = CaptureReigon(SystemInformation.VirtualScreen);
            else
                bitmap = CaptureReigon(SystemInformation.VirtualScreen);

            // Save or upload the image
            if (upload)
            {
                this.WindowState = FormWindowState.Normal;

                // Create a tempoary file
                string tempFileName = "";
                try
                {
                    tempFileName = Path.GetTempFileName();
                    FileInfo fileInfo = new FileInfo(tempFileName);
                    fileInfo.Attributes = FileAttributes.Temporary;

                    bitmap.Save(tempFileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to create TEMP file or set its attributes. Uploading Failed!" + Environment.NewLine + ex.Message);
                }

                using (WebClient w = new WebClient())
                {
                    w.Headers.Add("Authorization", "Client-ID de4ed08b9722623");
                    var values = new System.Collections.Specialized.NameValueCollection()
                    {
                        {"image",Convert.ToBase64String(File.ReadAllBytes(tempFileName)) }
                    };

                    File.Delete(tempFileName);

                    w.UploadProgressChanged += new UploadProgressChangedEventHandler(UploadProgressChanged);
                    w.UploadValuesCompleted += new UploadValuesCompletedEventHandler(UploadComplete);
                    w.UploadValuesAsync(new Uri("https://api.imgur.com/3/upload.xml"), values);
                }
            }
            else
            {
                if (saveCaptureDialog.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(saveCaptureDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void captureToFileButton_Click(object sender, EventArgs e)
        {
            Capture(false);
        }
        private void captureUploadButton_Click(object sender, EventArgs e)
        {
            Capture(true);
        }

        private void UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage <= 100 && e.ProgressPercentage >= 0)
                progressBar.Value = e.ProgressPercentage;
        }
        private void UploadComplete(object sender, UploadValuesCompletedEventArgs e)
        {
            progressBar.Value = 100;
            notifyIcon.Icon = SystemIcons.Application;
            imageUrl = XDocument.Load(new MemoryStream(e.Result)).Element("data").Element("link").Value;
            notifyIcon.ShowBalloonTip(15000, "Screenshot Upload Complete", String.Format("Your screenshot has finished uploading to {0}. Click to view it",imageUrl), ToolTipIcon.Info);
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
                Process.Start(imageUrl);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
