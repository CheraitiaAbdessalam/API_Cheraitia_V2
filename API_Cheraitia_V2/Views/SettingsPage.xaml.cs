using System.Windows;
using System.Windows.Controls;

namespace API_Cheraitia_V2.Views
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                CityTextBox.Text = mainWindow.SelectedCity;
            }
        }

        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.SelectedCity = CityTextBox.Text;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.CloseParametres();
            }
        }
    }
}
