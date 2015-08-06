using HardwareMonitor.Client.Domain.Entities;
using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface ITemperatureUI : IView
    {
        void SetTemperatureAlertLevel(int tal);

        void SetUpdateTime(int updateTime);

        void SetObserversCount(int observersCount);

        void SetNotificationMethod(NotificationMethod notification);

        
        event EventHandler OnTemperatureAlertLevelChanged;
        event EventHandler OnUpdateTimeChanged;
        event EventHandler OnObserversCountChanged;
        event EventHandler OnNotificationMethodChanged;
    }
}
