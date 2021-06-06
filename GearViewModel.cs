using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauge
{
    public class GearViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        

        public GearViewModel()
        {
            Value = 'N';
        }
        

        char _value;
        public char Value
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
    }
}
