using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCompresser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                panel1.Visible = true;
                label2.Text = openFileDialog1.FileName;
                Bitmap img = new Bitmap(openFileDialog1.FileName);
                lblOriginalHeight.Text = img.Height.ToString() + " px";
                lblOriginalWidth.Text = img.Width.ToString() + " px";
                pictureBox1.Image = img;
                txtNewSizeHeightPixels.Text = img.Height.ToString();
                txtNewSizeWidthPixels.Text = img.Width.ToString();               
                var fileLength = new FileInfo(label2.Text).Length;
                lblCurrentFileSize.Text = (Math.Round((Convert.ToDouble(fileLength) / 1024),2)).ToString() + " kb";
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var image = pictureBox1.Image;
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            int newWidth;
            int.TryParse(txtNewSizeWidthPixels.Text, out newWidth);
            int newHeight;
            int.TryParse(txtNewSizeHeightPixels.Text, out newHeight);

            int quality;
            int.TryParse(txtQualityPercentage.Text, out quality);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            //do something
            newImage.Save("G:\\Github", imageCodecInfo, encoderParameters);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            calculateSize();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            calculateSize();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            calculateSize();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            calculateSize();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            calculateSize();
        }

        public void calculateSize()
        {
            
        }
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

    }
}
