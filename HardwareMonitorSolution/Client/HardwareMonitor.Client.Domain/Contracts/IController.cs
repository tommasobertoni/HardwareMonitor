
namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IController
    {
        IClientSettingsUI SettingsUI { set; }

        ITemperatureUI TemperatureUI { set; }

        void AddObserver(ITemperatureObserver temperatureObserver);

        bool RemoveObserver(ITemperatureObserver temperatureObserver);
    }
}
