using System;
using static AcUdpCommunication.AcUdpConnection;

namespace GUI.Connectors.Base
{
    public interface IConnector
    {
        void Connect(string ipAddress, string port);
        void Disconnect();
        event EventHandler<BaseUpdateEventArgs> CarUpdate;
    }
}
