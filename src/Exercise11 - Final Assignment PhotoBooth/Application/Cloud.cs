using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace Exercise3
{
    public static class Cloud
    {
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
