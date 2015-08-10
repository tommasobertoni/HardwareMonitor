using HardwareMonitor.Client.Domain.Entities;
using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface ITemperatureUI : IView
    {
        void SetAvgCPUsTemperature(int temperature);

        void SetTemperatureAlertLevel(int tal);

        void SetUpdateTime(int updateTime);

        void SetObserversCount(int observersCount);

        void SetNotificationMethod(NotificationMethod notification);

        
        event EventHandler<ViewValueChangedEventArgs> OnTemperatureAlertLevelChanged;
        event EventHandler<ViewValueChangedEventArgs> OnUpdateTimeChanged;
        event EventHandler<ViewValueChangedEventArgs> OnObserversCountChanged;
        event EventHandler<ViewValueChangedEventArgs> OnNotificationMethodChanged;
    }
}
