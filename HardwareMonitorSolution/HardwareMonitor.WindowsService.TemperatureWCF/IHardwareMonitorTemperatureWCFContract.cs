using HardwareMonitor.WindowsService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.WindowsService.TemperatureWCF
{
    [ServiceContract]
    public interface IHardwareMonitorTemperatureWCFContract : IHardwareMonitorWCFContract
    {
        [OperationContract]
        float? GetAvgCPUsTemperature();

        [OperationContract]
        float? GetCPUTemperature(int cpuIndex);
    }
}
