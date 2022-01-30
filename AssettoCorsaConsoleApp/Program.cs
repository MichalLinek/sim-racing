using System;
using System.IO.Ports;

namespace ConsoleApp1
{
    class Program
    {
        public const string IP_ADDRESS = "192.168.1.41";
        public const string COM_ID = "COM12";
        public const int BANDWIDTH = 9600;
        public static SerialPort port;

        static void Main(string[] args)
        {
            port = new SerialPort(COM_ID, BANDWIDTH, Parity.None, 8, StopBits.One);
            //string[] portNames = SerialPort.GetPortNames();
            port.Open();

            using (var connection = new AcUdpCommunication.AcUdpConnection(IP_ADDRESS, AcUdpCommunication.AcUdpConnection.ConnectionType.CarInfo))
            {
                connection.CarUpdate += Connection_CarUpdate;
                connection.Connect();
                Console.ReadKey();
                connection.CarUpdate -= Connection_CarUpdate;
            }

            // port.Write("DISCONNECTED#");
            port.Close();

        }

            private static void Connection_CarUpdate(object sender, AcUdpCommunication.AcUdpConnection.AcUpdateEventArgs e)
            {
                //Console.WriteLine(e.carInfo.Gear - 1);
                port.WriteLine(e.carInfo.engineRPM.ToString() + "#");
            }
        }
    }
