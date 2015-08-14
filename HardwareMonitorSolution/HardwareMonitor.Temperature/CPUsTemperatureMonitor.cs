using HardwareMonitor.Domain;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HardwareMonitor.Temperature
{
    public class CPUsTemperatureMonitor : AbstractHardwareMonitor
    {
        public static readonly CPUsTemperatureMonitor INSTANCE = new CPUsTemperatureMonitor();

        private Computer _computer;
        private List<Tuple<IHardware, ISensor>> _cpusTemperatureSensors;

        private float? _avg = null;

        private CPUsTemperatureMonitor()
        {

            _computer = new Computer()
            {
                CPUEnabled = true
            };

            _computer.Open();

            RetriveTemperatureSensors();
        }

        public override int GetCPUsCount() => _cpusTemperatureSensors.Count;

        private void RetriveTemperatureSensors()
        {
            var cpus = _computer.Hardware.Where(hw => hw.HardwareType == HardwareType.CPU);
            _cpusTemperatureSensors = new List<Tuple<IHardware, ISensor>>(cpus.Count());

            foreach (var cpu in cpus)
            {
                _cpusTemperatureSensors.Add(
                    new Tuple<IHardware, ISensor>(cpu, cpu.Sensors
                                                          .Where(sensor => sensor.SensorType == SensorType.Temperature)
                                                          .FirstOrDefault()));
            }
        }

        public bool UpdateAvgTemperature()
        {
            int count = 0;
            float sum = 0;
            foreach (var tuple in _cpusTemperatureSensors)
            {
                var temperature = tuple.GetValueFromTuple();
                if (temperature != null)
                {
                    sum += temperature.Value;
                }
            }

            if (count > 0)
            {
                _avg = sum / count;
                return true;
            }

            return false;
        }

        public float? GetAvgTemperature(bool forceUpdate = false)
        {
            if (_avg == null || forceUpdate) UpdateAvgTemperature();
            return _avg;
        }

        public float? GetCPUTemperature(int cpuIndex, bool forceUpdate = false)
        {
            if (cpuIndex < 0 || cpuIndex > _cpusTemperatureSensors.Count) return null;

            var tuple = _cpusTemperatureSensors[cpuIndex];
            if (forceUpdate) tuple.Item1.Update();
            return tuple.Item2.Value;
        }
    }
}
