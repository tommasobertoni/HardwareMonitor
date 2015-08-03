using OpenHardwareMonitor.Hardware;
using System;

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
