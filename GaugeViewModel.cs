using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauge
{
    public class GaugeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private float ratio = 2.85f;
        private float degreeToSpeedRatio;
        private int zero = -105;

        public GaugeViewModel()
        {
            Angle = -105;
            Value = 0;
            degreeToSpeedRatio = 200.0f / 215.0f;
        }

        int _angle;
        public int Angle
        {
            get
            {
                return _angle;
            }

            set
            {
                if (value >= -180 && value <= 220)
                {
                    _angle = (int)(zero + (value * ratio));
                    Value = (_angle + 105) * degreeToSpeedRatio;
                    NotifyPropertyChanged("Angle");
                }
            }
        }

        float _value;
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
                NotifyPropertyChanged("ValueRounded");
            }
        }
        
        public string ValueRounded
        {
            get
            {
                return $"{((int) _value)}";
            }
        }
    }
}
