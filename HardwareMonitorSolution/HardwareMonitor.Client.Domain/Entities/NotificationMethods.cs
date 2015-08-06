using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Client.Domain.Entities
{
    public enum NotificationMethod
    {
        MESSAGE_BOX,
        TRAY_NOTIFICATION,
        NONE
    }

    public class NotificationMethodEventArgs : EventArgs
    {
        public NotificationMethod Value { get; set; }
    }
}
