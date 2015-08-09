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

namespace tomps_screenshot_utility
{
    public partial class Form1 : Form
    {
        public Dictionary<string, Rectangle> screens;
        public Form1()
        {
            InitializeComponent();

            screens = new Dictionary<string, Rectangle>();
            int i = 0;
            foreach(Screen screen in Screen.AllScreens)
            {
                string name; 
                if(screen == Screen.PrimaryScreen)
                    name = String.Format("Display {0} (Primary)", i);
                else
                   name = String.Format("Display {0}",i);

                comboBox1.Items.Add(name);
                screens.Add(name, screen.Bounds);
                i++;
            }
        }
        public Bitmap CaptureReigon(Rectangle rect)
        {
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, new Size(rect.Width, rect.Height));
            }
            return bitmap;
        }
        private void CaptureToFile()
        {
            this.WindowState = FormWindowState.Minimized;

            // Capture the image
            Bitmap bitmap;

            string selectedValue = comboBox1.SelectedItem.ToString();
            Console.WriteLine(selectedValue + " is " + screens.ContainsKey(selectedValue).ToString());
            if (screens.ContainsKey(selectedValue))
                bitmap = CaptureReigon(screens[selectedValue]);
            else if (selectedValue == "Window")
                bitmap = CaptureReigon(SystemInformation.VirtualScreen);
            else if (selectedValue == "Region")
                bitmap = CaptureReigon(SystemInformation.VirtualScreen);
            else
                bitmap = CaptureReigon(SystemInformation.VirtualScreen);

            // Save or upload the image
            if (saveCaptureDialog.ShowDialog() == DialogResult.OK)
                bitmap.Save(saveCaptureDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);

            this.WindowState = FormWindowState.Normal;
        }
        private void captureToFileButton_Click(object sender, EventArgs e)
        {
            CaptureToFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureToImgur();
        }

        private void CaptureToImgur()
        {
            throw new NotImplementedException();
        }
    }
}
