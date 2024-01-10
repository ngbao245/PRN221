using AForge.Video.DirectShow;
using System.Windows;
using System.Windows.Controls;
using PictureBox = System.Windows.Forms.PictureBox;
using WinForms = System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms.Integration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Exercise3
{
    public partial class MainWindow : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice? videoSource = default;
        private PictureBox formsPictureBox;
        private bool isWindowClosed = false;

        private static readonly string DEFAULT_PATH = "C:\\Users\\BiBo\\Desktop";
        List<ImageClass> imageList = new List<ImageClass>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeCamera();

            txtPath.Text = DEFAULT_PATH;
            txtPath.IsReadOnly = true;
        }

        private void InitializeCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                formsPictureBox = new PictureBox();
                windowsFormsHost.Child = formsPictureBox;

                formsPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                videoSource.NewFrame += (s, e) =>
                {
                    if (isWindowClosed)
                        return;

                    formsPictureBox.Image = (Bitmap)e.Frame.Clone();
                };

                videoSource.Start();
            }
            else
            {
                System.Windows.MessageBox.Show("No video devices found.");
            }
        }
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string directoryPath = txtPath.Text;
            ComboBoxItem selectedItem = (ComboBoxItem)cbExtension.SelectedItem;

            try
            {
                if (capturedImage.Source is BitmapSource bitmapSource)
                {
                    string filePath = MyToys.SaveImage(directoryPath, selectedItem, bitmapSource);

                    imageList.Add(new ImageClass
                    {
                        Image = filePath,
                        Path = filePath,
                    });
                    RefreshFiles();
                }
                else
                {
                    System.Windows.MessageBox.Show("No image to save. Capture an image first.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (videoSource != null)
            {
                Bitmap capturedBitmap = (Bitmap)formsPictureBox.Image.Clone();

                capturedImage.Source = MyToys.ConvertToBitmapSource(capturedBitmap);
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            txtPath.Text = MyToys.FolderBrowser();
        }

        private void RefreshFiles()
        {
            lvImageList.ItemsSource = imageList.ToList();
        }

        private void UpToDrive_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}