using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IClientSettingsOperations
    {
        event EventHandler OnSavedSettings;
    }
}
