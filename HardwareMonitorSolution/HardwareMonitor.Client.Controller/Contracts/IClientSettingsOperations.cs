using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Client.Controller.Contracts
{
    public interface IClientSettingsOperations
    {
        event EventHandler<bool> OnToggleBroadcastServices;
    }
}
