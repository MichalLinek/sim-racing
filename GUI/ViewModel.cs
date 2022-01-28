using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
   
        public ViewModel() {}
        
        public string AppId { get; set; }
        private float _value = 0;
        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }

        private bool _displayRpm = false;
        public bool DisplayRPM
        {
            get
            {
                return _displayRpm;
            }

            set
            {
                _displayRpm = value;
                NotifyPropertyChanged("DisplayRPM");
            }
        }

        private bool _sendToArduino = false;
        public bool SendToArduino
        {
            get
            {
                return _sendToArduino;
            }

            set
            {
                _sendToArduino = value;
                NotifyPropertyChanged("SendToArduino");
            }
        }

        private bool _isConnected = false;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }

            set
            {
                _isConnected = value;
                _isDisconnected = !_isConnected;
                NotifyPropertyChanged("IsConnected");
                NotifyPropertyChanged("IsDisconnected");
            }
        }

        private bool _isDisconnected = true;
        public bool IsDisconnected
        {
            get
            {
                return _isDisconnected;
            }

            set
            {
                _isDisconnected = value;
                NotifyPropertyChanged("IsDisconnected");
            }
        }

        private string _ipAddress = "";
        public string IpAddress
        {
            get
            {
                return _ipAddress;
            }

            set
            {
                _ipAddress = value;
                NotifyPropertyChanged("IpAddress");
            }
        }

        private string _ipPort = "";
        public string IpPort
        {
            get
            {
                return _ipPort;
            }

            set
            {
                _ipPort = value;
                NotifyPropertyChanged("IpPort");
            }
        }

        private string _bandwidth = "";
        public string Bandwidth
        {
            get
            {
                return _bandwidth;
            }

            set
            {
                _bandwidth = value;
                NotifyPropertyChanged("Bandwidth");
            }
        }

        private string _comPort = "";
        public string ComPort
        {
            get
            {
                return _comPort;
            }

            set
            {
                _comPort = value;
                NotifyPropertyChanged("ComPort");
            }
        }
    }
}
