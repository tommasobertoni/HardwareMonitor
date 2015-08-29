using HardwareMonitor.Client.Domain.Entities;
using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IClientSettingsUI : IView
    {
        event EventHandler OnSavedSettings;
        event EventHandler<Theme> OnForceTheme;
    }
}
