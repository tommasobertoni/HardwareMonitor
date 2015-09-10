using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.WindowsService.Domain
{
    [ServiceContract]
    public interface IHardwareMonitorWCFContract
    {
        [OperationContract]
        int GetCPUsCount();
    }
}
