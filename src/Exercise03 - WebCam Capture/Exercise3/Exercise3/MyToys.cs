using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
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
        private static readonly string TEMP_PATH = "D:\\FPT\\Sem7\\PRN2\\Excercise\\src\\Exercise3 - WebCam Capture\\Exercise3\\Exercise3\\temp\\";

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

        public static void Temp(ComboBoxItem selectedItem, BitmapSource bitmapSource)
        {
            SaveImage(TEMP_PATH, selectedItem, bitmapSource);    
        }

        public static void UploadToDrive(string credentialsPath, string folderId, string imageUploadPath)
        {
            try
            {
                GoogleCredential credential;
                using (var credentialsStream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(credentialsStream).CreateScoped(new[]
                    {
                    DriveService.ScopeConstants.DriveFile
                });

                    var service = new DriveService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "Google Drive Upload Console App"
                    });

                    var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = Path.GetFileName(imageUploadPath),
                        Parents = new List<string> { folderId },
                    };

                    FilesResource.CreateMediaUpload request;
                    using (var imageStream = new FileStream(imageUploadPath, FileMode.Open))
                    {
                        request = service.Files.Create(fileMetaData, imageStream, "");
                        request.Fields = "id";
                        request.Upload();
                    }

                    var uploadedImage = request.ResponseBody;
                    if (uploadedImage != null && fileMetaData != null)
                    {
                        System.Windows.MessageBox.Show($"Image {fileMetaData.Name} uploaded with ID {uploadedImage.Id}.");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Error uploading the image to Google Drive.");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
