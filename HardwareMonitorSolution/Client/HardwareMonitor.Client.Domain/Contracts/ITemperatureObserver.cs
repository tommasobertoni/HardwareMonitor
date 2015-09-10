
namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface ITemperatureObserver
    {
        void OnAvgCPUsTemperatureChanged(float temperature);
    }
}
