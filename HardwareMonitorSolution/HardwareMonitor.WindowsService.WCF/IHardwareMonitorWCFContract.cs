using System.ServiceModel;

namespace HardwareMonitor.WindowsService.WCF
{
    [ServiceContract]
    public interface IHardwareMonitorWCFContract
    {
        [OperationContract]
        int GetCPUsCount();

        [OperationContract]
        float? GetAvgCPUsTemperature();

        [OperationContract]
        float? GetCPUTemperature(int cpuIndex);
    }
}
