using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using API_Cheraitia_V2.Models;

namespace API_Cheraitia_V2
{
    public partial class MainWindow : Window
    {
        private const string ApiUrl = "https://www.prevision-meteo.ch/services/json/";

        public MainWindow()
        {
            InitializeComponent();
            LoadDataAsync("Annecy"); // Vous pouvez changer "Annecy" par une autre ville
        }

        private async Task LoadDataAsync(string city)
        {
            try
            {
                var json = await GetDataAsync(city);
                if (json == null)
                {
                    MessageBox.Show("Erreur lors de la récupération des données météo.");
                    return;
                }

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

                // Prévisions
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
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Erreur de connexion : {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Une erreur est survenue : {e.Message}");
            }
        }

        private async Task<string> GetDataAsync(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(ApiUrl + city);
                    return response;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}