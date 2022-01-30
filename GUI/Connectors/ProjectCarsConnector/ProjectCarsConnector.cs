using GUI.Connectors.Base;
using GUI.Logger;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static AcUdpCommunication.AcUdpConnection;

namespace GUI.Connectors.ProjectCarsConnector
{
    public class ProjectCarsConnector : IConnector
    {
        private UdpClient listener;
        private IPEndPoint groupEP;
        private Thread thread;
        public PCConnection PCConnection;
        public event EventHandler<BaseUpdateEventArgs> CarUpdate;

        public void Connect(string ipAddress, string port)
        {
            FileLogger.LogInfo($"Opening connection to ProjectCars Connector on IP: {ipAddress} and Port: {port}");
            listener = new UdpClient(Convert.ToInt32(port));
            groupEP = new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt32(port));
            PCConnection = new PCConnection(listener, groupEP);
            RunUDPProcessing();
        }

        public void Disconnect()
        {
            CancelPC2();
        }

        private void ReadProjectCarsPacket()
        {
            try
            {
                FileLogger.LogInfo($"Reading packets from Project Cars 2");
                while (true)
                {
                        PCConnection.readPackets();
                        if (CarUpdate != null)
                        {
                            var updateArgs = new BaseUpdateEventArgs()
                            {
                                carInfo = new AcUdpCommunication.CarInfo() { engineRPM = PCConnection.Rpm }
                            };

                            CarUpdate(this, updateArgs);
                        }
                }
            }
            catch (Exception e)
            {
                FileLogger.LogInfo($"Ending reading packets from Project Cars 2, details:\n{ e.Message}");
            }
            finally {
                CancelPC2();
            }
        }

        private void RunUDPProcessing()
        {
            thread = new Thread(ReadProjectCarsPacket);
            thread.Start();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadProjectCarsPacket();
        }

        private void CancelPC2()
        {
            if (thread != null && thread.IsAlive)
            {
                FileLogger.LogInfo($"Closing connection for Project Cars 2");
                thread.Abort();
                PCConnection.Dispose();
            }
        }
    }
}
