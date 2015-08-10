using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using System.Net;
using System.IO;

namespace tomps_screenshot_utility
{
    static class Program
    {
        static Form1 mainForm;
        public static bool isUploading = false;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new Form1();
            Application.Run(mainForm);
        }
        public static void CaptureReigonToFile(Rectangle rect,string fileName, int delay)
        {
            mainForm.Visible = false;
            System.Threading.Thread.Sleep(delay);
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, new Size(rect.Width, rect.Height));
            }
            bitmap.Save(fileName,System.Drawing.Imaging.ImageFormat.Png);
            mainForm.Visible = true;
        }
        public static void UploadImage(string fileName)
        {
            isUploading = true;
            using (WebClient web = new WebClient())
            {
                web.Headers.Add("Authorization", "Client-ID de4ed08b9722623");
                var values = new System.Collections.Specialized.NameValueCollection()
                    {
                        {"image",Convert.ToBase64String(File.ReadAllBytes(fileName)) }
                    };

                web.UploadProgressChanged += new UploadProgressChangedEventHandler(mainForm.UploadProgressChanged);

                web.UploadValuesCompleted += new UploadValuesCompletedEventHandler(mainForm.UploadComplete);
                web.UploadValuesCompleted += new UploadValuesCompletedEventHandler(UploadComplete);
                web.UploadValuesAsync(new Uri("https://api.imgur.com/3/upload.xml"), values);
            }
        }
        static void UploadComplete(object sender, UploadValuesCompletedEventArgs e)
        {
            isUploading = false;
        }
    }
}
