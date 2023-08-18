using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using DocumentFormat.OpenXml.Drawing;

namespace PhotoFilterApp
{
    public partial class frmPhotoFilterApp : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        int filtroPadrao = 0;

        public frmPhotoFilterApp()
        {
            InitializeComponent();
        }

        private void frmPhotoFilterApp_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Nenhuma webcam encontrada.");
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            videoSource.Start();
        }

        private void frmPhotoFilterApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap originalImage = (Bitmap)eventArgs.Frame.Clone();
            Bitmap filteredImage = ApplyFilter(originalImage, filtroPadrao); // Apply your desired filter function here

            photo.Image = filteredImage;
        }

        private Bitmap ApplyFilter(Bitmap inputImage, int filterIndex)
        {
            // Apply your color filter here
            // Example: Apply one of 10 different filters randomly

            Random random = new Random();
            //int filterIndex = random.Next(10);

            IFilter filter;

            switch (filterIndex)
            {
                case 0:
                    filter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721);
                    break;
                case 1:
                    filter = new Invert();
                    break;
                case 2:
                    filter = new Sepia();
                    break;
                case 3:
                    filter = new ContrastCorrection(30);
                    break;
                case 4:
                    filter = new SaturationCorrection((float)0.5);
                    break;
                case 5:
                    filter = new BrightnessCorrection(20);
                    break;
                case 6:
                    filter = new GaussianBlur(2, 11);
                    break;
                case 7:
                    filter = new OilPainting();
                    break;
                case 8:
                    filter = new Mirror(false, true);
                    break;
                case 9:
                    filter = new RotateChannels();
                    break;
                default:
                    return inputImage; // Return original image if no filter is applied
            }

            return filter.Apply(inputImage);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtroPadrao = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filtroPadrao = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            filtroPadrao = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            filtroPadrao = 3;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filtroPadrao = 4;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            filtroPadrao = 5;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            filtroPadrao = 6;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            filtroPadrao = 7;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            filtroPadrao = 8;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            filtroPadrao = 9;
        }
    }
}
