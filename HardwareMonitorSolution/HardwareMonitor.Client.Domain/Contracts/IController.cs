
namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IController
    {
        ITemperatureUI TemperatureUI { set; }

        void AddObserver(ITemperatureObserver temperatureObserver);

        bool RemoveObserver(ITemperatureObserver temperatureObserver);
    }
}
