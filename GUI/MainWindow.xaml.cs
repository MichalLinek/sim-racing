using GUI.Connectors;
using GUI.Connectors.AssettoCorsaConnector;
using GUI.Connectors.Base;
using GUI.Connectors.ProjectCarsConnector;
using GUI.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using static AcUdpCommunication.AcUdpConnection;

namespace GUI
{
    public partial class MainWindow : Window
    {
        public IConnector connector;
        public ArduinoConnector arduinoConnector = new ArduinoConnector();
        public static ViewModel ViewModel;

        public MainWindow()
        {
            try
            {
                connector = new AssettoCorsaConnector();
                FileLogger.LogInfo("App starting");
                InitializeComponent();
                ViewModel = new ViewModel();

                this.DataContext = ViewModel;

                GameTypeComboBox.ItemsSource = new List<string>() { "Assetto Corsa", "Project Cars 2" };
                GameTypeComboBox.SelectedIndex = 0;

                var portNames = SerialPort.GetPortNames();
                PortComboBox.ItemsSource = portNames.ToList();
                PortComboBox.SelectedIndex = 0;
                ViewModel.ComPort = (string)PortComboBox.SelectedValue;
                ViewModel.IpAddress = GetLocalIPAddress();
                
                SettingsHelper.LoadJson(ViewModel, GameTypeComboBox);
            }
            catch(Exception e)
            {
                FileLogger.LogError(e.Message);
            }
            finally
            {
                FileLogger.LogInfo("App started");
            }
        }
        
        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ViewModel.IsConnected)
                {
                    if (ViewModel.SendToArduino)
                    {
                        arduinoConnector.Connect(ViewModel.ComPort, ViewModel.Bandwidth);
                    }
                
                    connector.Connect(ViewModel.IpAddress, ViewModel.IpPort);
                    connector.CarUpdate += Connection_CarUpdate;
                    ViewModel.IsConnected = true;
                    ConnectButton.Content = "Disconnect";
                }
                else
                {
                    connector.CarUpdate -= Connection_CarUpdate;
                    connector.Disconnect();
                    if (ViewModel.SendToArduino)
                    {
                        arduinoConnector.Close();
                    }
                    ViewModel.IsConnected = false;
                    
                    ConnectButton.Content = "Connect";
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogCritical("Error durring Connecting occured" + ex.Message);
            }
        }

        private void Connection_CarUpdate(object sender, BaseUpdateEventArgs e)
        {
            if (ViewModel.DisplayRPM)
                ViewModel.Value = e.carInfo.engineRPM;
            if (ViewModel.SendToArduino)
                arduinoConnector.SendToArduino(e.carInfo.engineRPM.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Value = 0;
            if (ViewModel.SendToArduino)
            {
                arduinoConnector.ResetRPM();
            }
        }

        private void GameTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GameTypeComboBox.SelectedIndex)
            {
                case 0:
                    {
                        connector = new AssettoCorsaConnector();
                        ViewModel.IpPort = Constants.ASSETTO.IP_PORT;
                        break;
                    }
                case 1:
                    {
                        connector = new ProjectCarsConnector();
                        ViewModel.IpPort = Constants.PROJECT_CARS.IP_PORT;
                        break;
                    }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            FileLogger.LogInfo("Application is Closing - disconnecting connectors");
            try
            {
                connector?.Disconnect();
            }
            catch (Exception ex)
            {
                FileLogger.LogInfo("Error during closing the app " + ex.Message);
            }
            finally
            {
                arduinoConnector?.Close();
                FileLogger.LogInfo("Application is Closed");
            }
        }
    }
}
