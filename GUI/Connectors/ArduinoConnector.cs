using GUI.Logger;
using System;
using System.IO.Ports;

namespace GUI.Connectors
{
    public class ArduinoConnector
    {
        public SerialPort port;
        private readonly string DELIMITER = "#";

        public void Connect(string comPort, string bandwidth)
        {
            FileLogger.LogInfo($"Connecting to Arduino on port: {comPort} and bandwith: {bandwidth}");
            port = new SerialPort(comPort, Convert.ToInt32(bandwidth), Parity.None, 8, StopBits.One);
            port.Open();
        }

        public void ResetRPM()
        {
            if (isPortAvailable)
            {
                FileLogger.LogInfo("Arduino RPM Gauge Reset");
                port.WriteLine("0" + DELIMITER);
            }
        }

        public void Close()
        {
            if (isPortAvailable) {
                FileLogger.LogInfo("Closing Connection to Arduino");
                ResetRPM();
                port.Close();
            }
        }

        public void SendToArduino(string data)
        {
            if (isPortAvailable)
            {
                port.WriteLine(data + DELIMITER);
            }
        }

        public bool isPortAvailable => (port != null) && (port.IsOpen); 
    }
}
