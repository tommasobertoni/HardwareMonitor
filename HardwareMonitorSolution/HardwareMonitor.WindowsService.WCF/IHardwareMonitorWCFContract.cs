using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HardwareMonitor.WindowsService.WCF
{
    [ServiceContract]
    public interface IHardwareMonitorWCFContract
    {
        [OperationContract]
        float? GetAvgCPUsTemperature();

        [OperationContract]
        float? GetCPUTemperature(int cpuIndex);
    }
}
