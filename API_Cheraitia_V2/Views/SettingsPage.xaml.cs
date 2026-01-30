using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace API_Cheraitia_V2.Views
{
    /// <summary>
    /// Logique d'interaction pour SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            // : Initialiser le ComboBox
            CityComboBox.ItemsSource = _cities;
            CityComboBox.SelectedIndex = _cities.IndexOf("Annecy"); // Sélection par défaut
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedItem != null)
            {
                CityComboBox.Text = CityComboBox.SelectedItem.ToString();
            }
        }
        private ObservableCollection<string> _cities = new ObservableCollection<string>
        {
            "Paris", "Lyon", "Marseille", "Toulouse", "Nice", "Nantes", "Montpellier",
            "Strasbourg", "Bordeaux", "Lille", "Rennes", "Reims", "Toulon", "Grenoble", "Dijon", "Angers",
            "Annecy", "Geneve", "Lausanne"
        };

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityComboBox.Text.Trim();

          

            // Ajoute la ville à la liste si elle n'existe pas déjà
            if (!_cities.Contains(city))
            {
                _cities.Add(city);
                // Rafraîchir la source pour afficher la nouvelle ville
                CityComboBox.ItemsSource = null;
                CityComboBox.ItemsSource = _cities;
            }

        }
    }
}
