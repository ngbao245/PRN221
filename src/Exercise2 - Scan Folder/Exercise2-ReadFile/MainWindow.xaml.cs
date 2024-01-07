using System.CodeDom;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;
using System.Reflection.PortableExecutable;


namespace Exercise2_ReadFile
{
    public partial class MainWindow : Window
    {
        private static readonly string DEFAULT_PATH = "D:\\";
        public MainWindow()
        {
            InitializeComponent();
            txtPath.Text = DEFAULT_PATH;
            ShowFiles(DEFAULT_PATH);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = FolderBrowser();
            ShowFiles(folderPath);
        }

        private string FolderBrowser()
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

        private void ShowFiles(string folderPath)
        {
            List<FileItem> fileItems = new List<FileItem>();

            string[] files = Directory.GetFiles(folderPath);
            string[] folders = Directory.GetDirectories(folderPath);

            foreach (string file in files)
            {
                fileItems.Add(new FileItem
                {
                    Type = "File",
                    Name = Path.GetFileName(file),
                    Path = file
                });
            }

            foreach (string folder in folders)
            {
                fileItems.Add(new FileItem
                {
                    Type = "Folder",
                    Name = Path.GetFileName(folder),
                    Path = folder
                });
            }
            txtPath.Text = folderPath;
            lvShowFiles.ItemsSource = fileItems;
        }
    }
}

//double click folder ra folder con
//xóa
//rename nhấn chuột phải hiện ra context menu