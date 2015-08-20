
using System.IO.Pipes;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IController
    {
        PipeStream LauncherPipe { get; set; }

        ITemperatureUI TemperatureUI { set; }

        void AddObserver(ITemperatureObserver temperatureObserver);

        bool RemoveObserver(ITemperatureObserver temperatureObserver);
    }

    public enum ControllerCommand
    {
        UPDATE, RESTART
    }
}
