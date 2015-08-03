using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareManager.Temperature
{
    internal static class Utils
    {
        public static float? GetValueFromTuple(this Tuple<IHardware, ISensor> tuple)
        {
            tuple.Item1.Update();
            return tuple.Item2.Value;
        }
    }
}
