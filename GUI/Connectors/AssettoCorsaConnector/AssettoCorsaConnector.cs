using AcUdpCommunication;
using GUI.Connectors.Base;
using GUI.Logger;
using System;
using static AcUdpCommunication.AcUdpConnection;

namespace GUI.Connectors.AssettoCorsaConnector
{
    public class AssettoCorsaConnector: IConnector
    {
        private AcUdpConnection ACconnection;
        public event EventHandler<BaseUpdateEventArgs> CarUpdate;

        public void Connect(string ipAddress, string port)
        {
            try
            {
                FileLogger.LogInfo($"Opening connection to Assetto Corsa Connector on IP: {ipAddress} and Port: {port}");
                ACconnection = new AcUdpConnection(ipAddress, ConnectionType.CarInfo, Convert.ToInt32(port));
                ACconnection.CarUpdate += UpdatedEventDelegate;
                ACconnection.Connect();
            }
            catch (Exception ex)
            {
                FileLogger.LogCritical($"Error while connecting to Assetto Corsa Connector, details:\n{ex.Message}");
            }
        }

        public void Disconnect()
        {
            if (ACconnection != null)
            {
                try
                {
                    FileLogger.LogInfo($"Ending sending data from Assetto Corsa Connector");
                    ACconnection.CarUpdate -= UpdatedEventDelegate;
                    ACconnection.Dispose();
                    FileLogger.LogInfo($"Ended sending data from Assetto Corsa Connector");
                }
                catch(Exception ex)
                {
                    FileLogger.LogInfo($"Error on disconnect from Assetto Corsa Connector, details:\n{ex.Message}");
                }
                finally
                {
                    FileLogger.LogInfo($"Ended sending data from Assetto Corsa Connector");
                }
            }
        }

        public void UpdatedEventDelegate(object sender, BaseUpdateEventArgs e)
        {
            CarUpdate?.Invoke(this, e);
        }
    }
}
