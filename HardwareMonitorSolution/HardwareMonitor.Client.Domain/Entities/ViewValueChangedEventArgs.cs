using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Client.Domain.Entities
{
    public class ViewValueChangedEventArgs : EventArgs
    {
        public object Value { get; set; }

        public bool Save { get; set; }
    }
}
