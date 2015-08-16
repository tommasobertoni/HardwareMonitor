using System;

namespace HardwareMonitor.Client.Domain.Entities
{
    public class ViewValueChangedEventArgs : EventArgs
    {
        public object Value { get; set; }

        public bool Save { get; set; }
    }
}
