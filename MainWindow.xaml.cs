using PcarsUDP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace Gauge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int PORT_NUMBER = 5606;
        private static IPAddress ADDRESS = IPAddress.Any;
        public MainViewModel MainViewModel { get; set; }

        public MainWindow()
        {
            MainViewModel = new MainViewModel();
            this.DataContext = MainViewModel;
            InitializeComponent();
            RunUDPProcessing();
        }

        private void RunUDPProcessing()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += ReadUDP;
            worker.RunWorkerAsync();
        }

        private void ReadUDP(object sender, DoWorkEventArgs args)
        {
            var listener = new UdpClient(PORT_NUMBER);
            var groupEP = new IPEndPoint(ADDRESS, PORT_NUMBER);

            using (var uDP = new UDPManager(listener, groupEP))
            {
                while (true)
                {
                    uDP.readPackets();
                    this.MainViewModel.GearViewModel.Value = uDP.Gears;
                    this.MainViewModel.GaugeRPMViewModel.Angle = uDP.DisplayRPM;
                    this.MainViewModel.GaugeViewModel.Angle = (int) uDP.Speed;
                    this.MainViewModel.UDPInfo.RPM = uDP.DisplayRPM;
                    this.MainViewModel.UDPInfo.Gear = uDP.GearNumGears;
                    this.MainViewModel.UDPInfo.Speed = (int)uDP.Speed;
                    this.MainViewModel.UDPInfo.Throttle = uDP.Throttle;
                    this.MainViewModel.UDPInfo.Brake = uDP.Brake;

                }
            }
        }
    }
}
