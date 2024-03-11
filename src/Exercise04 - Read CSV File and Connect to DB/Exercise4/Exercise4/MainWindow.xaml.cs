using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercise4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable dt;
        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            var subjects = new List<string>
            {
                "mathematics", "literature", "physics", "chemistry",
                "biology", "history", "geography", "civic_education", "english"
            };
            cbSubject.ItemsSource = subjects;
        }


        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            dt = new DataTable();
            dt.Columns.Add("student_id", typeof(string));
            dt.Columns.Add("mathematics", typeof(string));
            dt.Columns.Add("literature", typeof(string));
            dt.Columns.Add("physics", typeof(string));
            dt.Columns.Add("chemistry", typeof(string));
            dt.Columns.Add("biology", typeof(string));
            dt.Columns.Add("history", typeof(string));
            dt.Columns.Add("geography", typeof(string));
            dt.Columns.Add("civic_education", typeof(string));
            dt.Columns.Add("english", typeof(string));

            foreach (Candidate candidateInfo in LoadCandidates(ofd.FileName))
            {
                dt.Rows.Add(
                    candidateInfo.student_id,
                    candidateInfo.mathematics,
                    candidateInfo.literature,
                    candidateInfo.physics,
                    candidateInfo.chemistry,
                    candidateInfo.biology,
                    candidateInfo.history,
                    candidateInfo.geography,
                    candidateInfo.civic_education,
                    candidateInfo.english
                );
            }

            DataView dv = new DataView(dt);
            dgvCSV.ItemsSource = dv;
        }

        private IEnumerable<Candidate> LoadCandidates(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string[] candidateArray = sr.ReadLine().Split(",");
                    yield return new Candidate
                    {
                        student_id = candidateArray[0],
                        mathematics = candidateArray[1],
                        literature = candidateArray[2],
                        physics = candidateArray[3],
                        chemistry = candidateArray[4],
                        biology = candidateArray[5],
                        history = candidateArray[6],
                        geography = candidateArray[7],
                        civic_education = candidateArray[8],
                        english = candidateArray[9]
                    };
                }
            }
        }

        private void btnShowCandidateNumber_Click(object sender, RoutedEventArgs e)
        {
            if (cbSubject.SelectedItem != null && dt != null)
            {
                string selectedSubject = cbSubject.SelectedItem.ToString();
                int candidateCount = GetCandidateCountForSubject(selectedSubject);
                MessageBox.Show($"Number of candidates for {selectedSubject}: {candidateCount}");
            }
            else
            {
                MessageBox.Show("Please select a subject and load data first.");
            }
        }

        private int GetCandidateCountForSubject(string subject)
        {
            return dt.AsEnumerable().Count(row => !string.IsNullOrEmpty(row.Field<string>(subject)));
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dt.Clear();
            dgvCSV.ItemsSource = null;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    string connectionString = "Server=LAPTOP\\SQLEXPRESS;Database=THPTQG;Trusted_Connection=True;";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        int batchSize = 20000;
                        int totalRows = dt.Rows.Count;

                        for (int startRow = 0; startRow < totalRows; startRow += batchSize)
                        {
                            int endRow = Math.Min(startRow + batchSize, totalRows);

                            DataTable chunk = dt.Clone(); // Create a clone of the table structure
                            for (int i = startRow; i < endRow; i++)
                            {
                                chunk.ImportRow(dt.Rows[i]);
                            }

                            using (SqlTransaction transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                                    {
                                        bulkCopy.BulkCopyTimeout = 0;
                                        bulkCopy.DestinationTableName = "CandidateData";

                                        foreach (DataColumn column in chunk.Columns)
                                        {
                                            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                                        }
                                        bulkCopy.WriteToServer(chunk);
                                    }

                                    transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show($"Error importing data to database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return; // Stop further processing on error
                                }
                            }
                        }

                        MessageBox.Show("Data imported to database successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No data to import.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}