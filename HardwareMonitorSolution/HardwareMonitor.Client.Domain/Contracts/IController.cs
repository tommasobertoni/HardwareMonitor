
namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IController
    {
        ITemperatureUI TemperatureUI { get; set; }
    }
}
