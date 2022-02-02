using GUI;
using System;
using System.Net;
using System.Net.Sockets;

namespace ProjectCarsConsoleApp
{
    class Program
    {
        public const string IP_ADDRESS = "192.168.2.229";
        public const string COM_ID = "COM12";
        public const int BANDWIDTH = 9600;
        //public static SerialPort port;

        static void Main(string[] args)
        {
            //port = new SerialPort(COM_ID, BANDWIDTH, Parity.None, 8, StopBits.One);
            //string[] portNames = SerialPort.GetPortNames();
            // port.Open();
            var listener = new UdpClient(5606);
            var groupEP = new IPEndPoint(IPAddress.Parse(IP_ADDRESS), 5606);

            using (var connection = new PCConnection(listener, groupEP))
            {
                while (true)
                {
                    connection.ReadPackets();
                    Console.WriteLine(connection.Rpm);
                }
            }

            // port.Write("DISCONNECTED#");

        }
    }
}
