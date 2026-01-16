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
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Collections.ObjectModel;


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

            var asyntaskwait = GetDataAsync();
        }


        public async Task<string> GetDataAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://www.prevision-meteo.ch/services/json/Annecy");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Root root = JsonConvert.DeserializeObject<Root>(data);

                CurrentCondition currentcondition = root.current_condition;

                FcstDay0 fcstday0 = root.fcst_day_0;

                FcstDay1 fcstday1 = root.fcst_day_1;

                FcstDay3 fcstday2 = root.fcst_day_3;
                FcstDay4 fcstDay4 = root.fcst_day_4;





                return data;
            }
            else
            {
                return null;
            }
        }

    }
}    
