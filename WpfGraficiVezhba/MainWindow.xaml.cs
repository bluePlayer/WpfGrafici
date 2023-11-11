using System;
using System.Collections.Generic;
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
using WpfGraficiVezhba.Code;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace WpfGraficiVezhba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChartDataMVVM chartData;

        public MainWindow()
        {

            InitializeComponent();

            chartData = new ChartDataMVVM();

            DataContext = chartData;

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                NBRMKursnaListaVebServis.KursSoapClient client = new NBRMKursnaListaVebServis.KursSoapClient();

                client.ChannelFactory.CreateChannel();

                Console.WriteLine(client.GetExchangeRateD(new DateTime(2023, 11, 10), DateTime.Today));
                Console.WriteLine(client.GetExchangeRate("10.11.2023", "11.11.2023"));

                Console.WriteLine(client.GetExchangeRatesD(new DateTime(2023, 11, 10), DateTime.Today));
                Console.WriteLine(client.GetExchangeRates("10.11.2023", "11.11.2023"));
            }
            catch(Exception ex)
            {

            }

        }

        public class RegInfoResponseObj
        {
            public bool status { get; set; }
            public int code { get; set; }
            public string message { get; set; }
        }

        public class RegInfoRequestObj
        {
            public string fullname { get; set; }
        }

        public const string REST_SERVICE_URI = "http://localhost:19006/";

        public async Task<RegInfoResponseObj> RestServiceVezhba(RegInfoRequestObj requestObj)
        {
            RegInfoResponseObj responseObj = new RegInfoResponseObj();

            try
            {
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri(REST_SERVICE_URI);

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = await client.PostAsJsonAsync("api/WebApi/PostRegInfo", requestObj).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonConvert.DeserializeObject<RegInfoResponseObj>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj.code = 602;
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return responseObj;
        }

        public static void saveInfo(string fullname)
        {
            try
            {
                // Query.  
                string query = "INSERT INTO [Register] ([fullname])" +
                                " Values ('" + fullname + "')";

                // Execute.  
                //DAL.executeQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Initialization.  
                string fullname = "Vladimir Z", obj;

                // Save Info to Local DB.  
                saveInfo(fullname);

                // Sending data to server.  
                RegInfoRequestObj requestObj = new RegInfoRequestObj { fullname = fullname };
                RegInfoResponseObj responseObj = await RestServiceVezhba(requestObj);

                // Veerification.  
                if (responseObj.code == 600)
                {
                    // Display Message  
                    MessageBox.Show("You are Successfully! Registered", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (responseObj.code == 602)
                {
                    // Display Message  
                    MessageBox.Show("We are unable to register you at the moment, please try again later", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                // Display Message  
                MessageBox.Show("Something goes wrong, Please try again later.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);

                Console.Write(ex);
            }
        }
    }
}
