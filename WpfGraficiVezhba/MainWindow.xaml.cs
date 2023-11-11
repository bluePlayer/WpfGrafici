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
    }
}
