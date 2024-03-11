using System.IO;
using System.Windows;
using WinForms = System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Win32 = Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;


namespace Exercise2_ReadFile
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<FileItem> Items { get; set; }

        private static readonly string DEFAULT_PATH = "D:\\";
        public MainWindow()
        {
            InitializeComponent();
            txtPath.Text = DEFAULT_PATH;
            txtPath.IsReadOnly = true;
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
                    ImagePath = @"D:\FPT\Sem7\PRN2\Excercise\src\Exercise2 - Scan Folder\Exercise2-ReadFile\Icons\fileICO.png",
                    Name = System.IO.Path.GetFileName(file),
                    Path = file,
                });
            }

            foreach (string folder in folders)
            {
                fileItems.Add(new FileItem
                {
                    ImagePath = @"D:\FPT\Sem7\PRN2\Excercise\src\Exercise2 - Scan Folder\Exercise2-ReadFile\Icons\folderICO.png",
                    Name = System.IO.Path.GetFileName(folder),
                    Path = folder,
                });
            }
            txtPath.Text = folderPath;
            lvShowFiles.ItemsSource = fileItems;
        }

        private void lvShowFiles_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lvShowFiles.SelectedItem != null)
            {
                FileItem selectedItem = (FileItem)lvShowFiles.SelectedItem;
                string path = selectedItem.Path;

                if (File.Exists(path))
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo()
                    {
                        FileName = path,
                        UseShellExecute = true
                    };
                    Process.Start(processStartInfo);
                }
                if (Directory.Exists(path))
                {
                    ShowFiles(path);
                }
            }
        }

        private void lvShowFiles_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            lvShowFiles.ContextMenu.IsOpen = true;
            e.Handled = true;
        }

        private void RenameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lvShowFiles.SelectedItem is FileItem selectedItem)
            {
                // Prompt the user for a new name using MessageBox
                string newName = InputDialog("Enter new name:", "Rename");

                if (!string.IsNullOrEmpty(newName))
                {
                    RenameItem(selectedItem, newName);
                }
            }
        }

        private string InputDialog(string prompt, string title)
        {
            return System.Windows.MessageBox.Show(prompt, title, MessageBoxButton.OKCancel)
                   == MessageBoxResult.OK ? Microsoft.VisualBasic.Interaction.InputBox("", title, "") : string.Empty;
        }

        private void RenameItem(FileItem item, string newName)
        {
            string currentPath = item.Path;
            string newPath = Path.Combine(Path.GetDirectoryName(currentPath), newName);

            try
            {
                if (File.Exists(currentPath))
                {
                    File.Move(currentPath, newPath);
                }
                else if (Directory.Exists(currentPath))
                {
                    Directory.Move(currentPath, newPath);
                }

                RefreshFiles();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error renaming: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lvShowFiles.SelectedItem is FileItem selectedItem)
            {
                string pathToDelete = selectedItem.Path;

                MessageBoxResult result = System.Windows.MessageBox.Show($"Are you sure you want to delete '{selectedItem.Name}'?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (File.Exists(pathToDelete))
                        {
                            File.Delete(pathToDelete);
                        }
                        else if (Directory.Exists(pathToDelete))
                        {
                            Directory.Delete(pathToDelete, true);
                        }

                        RefreshFiles();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show($"Error deleting: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void RefreshFiles()
        {
            ShowFiles(txtPath.Text);
        }
    }
}

//double click folder ra folder con, click file mở file lên
//xóa
//rename nhấn chuột phải hiện ra context menu