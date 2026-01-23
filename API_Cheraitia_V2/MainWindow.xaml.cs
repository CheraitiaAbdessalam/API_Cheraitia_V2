using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using System.Security.Policy;
using System.Collections.ObjectModel;


using API_Cheraitia_V2.Models;


namespace API_Cheraitia_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var json = await GetDataAsync();
            if (json == null)
                return;

            Root root = JsonConvert.DeserializeObject<Root>(json);

            // Météo actuelle
            var current = root.current_condition;
            CityNameText.Text = root.city_info?.name ?? "Ville";
            CurrentDateText.Text = current?.date ?? "";
            CurrentTempText.Text = current?.tmp + " °C";
            CurrentConditionText.Text = current?.condition;
            CurrentHumidityText.Text = current?.humidity + " %";
            CurrentWindText.Text = current?.wnd_spd + " km/h";

            if (!string.IsNullOrEmpty(current?.icon_big))
                CurrentWeatherIcon.Source = new BitmapImage(new Uri(current.icon_big));

            // Prévisions : créer une liste avec fcst_day_0 à fcst_day_4
            var days = new List<object>
        {
            root.fcst_day_0,
            root.fcst_day_1,
            root.fcst_day_2,
            root.fcst_day_3,
            root.fcst_day_4
        };

            ForecastItemsControl.ItemsSource = days;
        }
    }

}
