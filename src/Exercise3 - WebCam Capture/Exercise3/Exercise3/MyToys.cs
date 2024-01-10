using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;

namespace Exercise3
{
    public static class MyToys
    {
        private static readonly string DEFAULT_PATH = "C:\\Users\\BiBo\\Desktop";
        public static string FolderBrowser()
        {
            try
            {
                string folder = string.Empty;
                WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
                dialog.InitialDirectory = DEFAULT_PATH;
                WinForms.DialogResult result = dialog.ShowDialog();
                if (result == WinForms.DialogResult.OK)
                {
                    folder = dialog.SelectedPath;
                }
                return folder;
            }
            catch (Exception e)
            {
                WinForms.MessageBox.Show("Random Error");
                return string.Empty;
            }
        }

        public static BitmapSource ConvertToBitmapSource(Bitmap bitmap)
        {
            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Bmp);

            var bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public static string SaveImage(string directoryPath, ComboBoxItem selectedItem, BitmapSource bitmapSource)
        {
            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                System.Windows.MessageBox.Show("Empty path. Please enter a valid path.");
                return "";
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHH") + "_" + Guid.NewGuid().ToString("N").Substring(0, 5);
            string fileExtension = "";

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

            fileExtension = selectedItem.Tag.ToString();
            fileName += fileExtension;

            string filePath = Path.Combine(directoryPath, fileName);

            BitmapEncoder encoder = new PngBitmapEncoder();
            Encode(encoder, fileExtension);

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            return filePath;
        }

        public static void Encode(BitmapEncoder encoder, string fileExtension)
        {
            if (fileExtension == ".bmp")
            {
                encoder = new BmpBitmapEncoder();
            }
            else if (fileExtension == ".jpg")
            {
                encoder = new JpegBitmapEncoder();
            }
            else if (fileExtension == ".png")
            {
                encoder = new PngBitmapEncoder();
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid file extension.");
                return;
            }
        }
    }
}
