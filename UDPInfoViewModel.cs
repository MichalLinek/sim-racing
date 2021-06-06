using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauge
{
    public class UDPInfoViewModel : INotifyPropertyChanged
    {
        private bool audioPlayed = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        

        public UDPInfoViewModel()
        {
            _speed = 0;
        }
        

        int _speed;
        public int Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                if (!audioPlayed && _speed > 60)
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\micha\Desktop\wazzup.wav");
                    player.Play();
                    this.audioPlayed = true;
                }
                _speed = value;
                NotifyPropertyChanged("Speed");
                NotifyPropertyChanged("DisplaySpeed");
            }
        }
        
        public string DisplaySpeed
        {
            get
            {
                return $"Speed = {_speed}";
            }
        }

        int _gear;
        public int Gear
        {
            get
            {
                return _gear;
            }

            set
            {
                _gear = value;
                NotifyPropertyChanged("Gear");
                NotifyPropertyChanged("DisplayGear");
            }
        }

        public string DisplayGear
        {
            get
            {
                return $"Gear = {_gear}";
            }
        }

        int _rpm;
        public int RPM
        {
            get
            {
                return _rpm;
            }

            set
            {
                _rpm = value;
                NotifyPropertyChanged("RPM");
                NotifyPropertyChanged("DisplayRPM");
            }
        }

        public string DisplayRPM
        {
            get
            {
                return $"RPM = {_rpm}";
            }
        }

        byte _throttle;
        public byte Throttle
        {
            get
            {
                return _throttle;
            }

            set
            {
                _throttle = value;
                NotifyPropertyChanged("Throttle");
                NotifyPropertyChanged("DisplayThrottle");
            }
        }

        public string DisplayThrottle
        {
            get
            {
                return $"Throttle = {(int)(_throttle)}";
            }
        }

        byte _brake;
        public byte Brake
        {
            get
            {
                return _brake;
            } 

            set
            {
                _brake = value;
                NotifyPropertyChanged("Brake");
                NotifyPropertyChanged("DisplayBrake");
            }
        }

        public string DisplayBrake
        {
            get
            {
                return $"Brake = {(int)_brake}";
            }
        }
    }
}
