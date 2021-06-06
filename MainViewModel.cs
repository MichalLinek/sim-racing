using PcarsUDP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauge
{
    public class MainViewModel
    {
        public GaugeViewModel GaugeViewModel { get; set; }
        public GearViewModel GearViewModel { get; set; }
        public GaugeRPMViewModel GaugeRPMViewModel { get; set; }
        public UDPInfoViewModel UDPInfo { get; set; }

        public MainViewModel()
        {
            this.GearViewModel = new GearViewModel();
            this.GaugeRPMViewModel = new GaugeRPMViewModel();
            this.GaugeViewModel = new GaugeViewModel();
            this.UDPInfo = new UDPInfoViewModel();
        }
    }
}
