using System;

namespace HardwareMonitor.Client.Controller.Contracts
{
    public interface IClientSettingsOperations
    {
        event EventHandler<bool> OnToggleBroadcastServices;
        event EventHandler OnSavedSettings;
    }
}
