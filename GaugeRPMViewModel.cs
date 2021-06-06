using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauge
{
    public class GaugeRPMViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private float degreeToSpeedRatio;
        private float ratio =  0.036f;
        private int zero = -213;

        public GaugeRPMViewModel()
        {
            Angle = -213;
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
                if (value >=  0 && value <= 8000)
                {
                    _angle = zero + (int)(value * ratio);
                    Value = (_angle + +213); // * degreeToSpeedRatio;
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
