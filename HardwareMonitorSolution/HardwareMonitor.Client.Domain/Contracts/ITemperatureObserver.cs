using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface ITemperatureObserver
    {
        void OnAvgCPUsTemperatureChanged(float temperature);
    }
}
