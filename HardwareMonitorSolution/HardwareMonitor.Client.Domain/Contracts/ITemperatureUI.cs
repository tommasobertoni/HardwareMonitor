using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface ITemperatureUI : IView, ITemperatureObserver
    {   
        event EventHandler<int> OnUpdateTimeChanged;
    }
}
