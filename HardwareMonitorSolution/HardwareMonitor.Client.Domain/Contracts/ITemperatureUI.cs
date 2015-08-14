using HardwareMonitor.Client.Domain.Entities;
using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface ITemperatureUI : IView
    {
        void SetAvgCPUsTemperature(float temperature);
        
        event EventHandler<ViewValueChangedEventArgs> OnUpdateTimeChanged;
        event EventHandler<ViewValueChangedEventArgs> OnObserversCountChanged;
    }
}
