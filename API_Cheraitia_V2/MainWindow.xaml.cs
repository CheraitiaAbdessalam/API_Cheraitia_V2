using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using API_Cheraitia_V2.Models;
using System.Windows.Controls;
using System.Runtime.CompilerServices;

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

                // ✅ PRÉVISIONS CORRIGÉES - adapter tmin/tmax vers tmp_min/tmp_max
                var days = new List<object>
                {
                    new {
                        date = root.fcst_day_0?.date,
                        tmp_min = root.fcst_day_0?.tmin,
                        tmp_max = root.fcst_day_0?.tmax,
                        condition = root.fcst_day_0?.condition,
                        icon_big = root.fcst_day_0?.icon_big
                    },
                    new {
                        date = root.fcst_day_1?.date,
                        tmp_min = root.fcst_day_1?.tmin,
                        tmp_max = root.fcst_day_1?.tmax,
                        condition = root.fcst_day_1?.condition,
                        icon_big = root.fcst_day_1?.icon_big
                    },
                    new {
                        date = root.fcst_day_2?.date,
                        tmp_min = root.fcst_day_2?.tmin,
                        tmp_max = root.fcst_day_2?.tmax,
                        condition = root.fcst_day_2?.condition,
                        icon_big = root.fcst_day_2?.icon_big
                    },
                    new {
                        date = root.fcst_day_3?.date,
                        tmp_min = root.fcst_day_3?.tmin,
                        tmp_max = root.fcst_day_3?.tmax,
                        condition = root.fcst_day_3?.condition,
                        icon_big = root.fcst_day_3?.icon_big
                    },
                    new {
                        date = root.fcst_day_4?.date,
                        tmp_min = root.fcst_day_4?.tmin,
                        tmp_max = root.fcst_day_4?.tmax,
                        condition = root.fcst_day_4?.condition,
                        icon_big = root.fcst_day_4?.icon_big
                    }
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

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(city))
            {
                MessageBox.Show("Veuillez entrer une ville.");
                return;
            }

            await LoadDataAsync(city);
        }
    }
}