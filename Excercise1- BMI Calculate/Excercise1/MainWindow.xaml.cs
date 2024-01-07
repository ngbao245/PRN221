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

namespace Excercise1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            txtHeight.Text = string.Empty;
            txtWeight.Text = string.Empty;
            lbBMI.Content = string.Empty;
            lbStatus.Content = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Height = txtHeight.Text;
            string Weight = txtWeight.Text;
            if (Height.Equals("") || Weight.Equals(""))
            {
                MessageBox.Show("Please enter both height and weight.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (double.TryParse(Height, out double height) && float.TryParse(Weight, out float weight))
            {   
                double bmi = weight / ((height / 100) * (height / 100));
                lbBMI.Content = bmi.ToString("F1");

                if (bmi < 18.5)
                {
                    lbStatus.Content = "UnderWeight";
                    lbStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#25ABE2"));
                }
                else if (bmi <= 24.9)
                {
                    lbStatus.Content = "Normal";
                    lbStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6B7F36"));
                }
                else if (bmi <= 29.9)
                {
                    lbStatus.Content = "OverWeight";
                    lbStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0AA1D"));
                }
                else if (bmi <= 34.9)
                {
                    lbStatus.Content = "Obese";
                    lbStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0AA1D"));
                }
                else
                {
                    lbStatus.Content = "Extremly Obese";
                    lbStatus.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DA2026"));
                }
            }

        }

        private void Refresh()
        {
            txtHeight.Text = string.Empty;
            txtWeight.Text = string.Empty;
            lbBMI.Content = string.Empty;
            lbStatus.Content = string.Empty;
            lbStatus.Background = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}