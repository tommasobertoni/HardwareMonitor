using HardwareMonitor.Domain;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareManager.Temperature
{
    public class CPUsTemperatureMonitor : AbstractHardwareMonitor
    {
        public static readonly CPUsTemperatureMonitor INSTANCE = new CPUsTemperatureMonitor();

        private Computer _computer;
        private List<Tuple<IHardware, ISensor>> _cpusTemperatureSensors;

        private float? _avg = null;
        private Dictionary<int, float> _cache = new Dictionary<int, float>();

        private CPUsTemperatureMonitor()
        {

            _computer = new Computer()
            {
                CPUEnabled = true
            };

            _computer.Open();

            RetriveTemperatureSensors();
        }

        public override int GetCPUsCount()
        {
            return _cpusTemperatureSensors.Count;
        }

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
            var count = 0;
            float sum = 0;
            foreach (var tuple in _cpusTemperatureSensors)
            {
                var temperature = tuple.GetValueFromTuple();
                if (temperature != null)
                {
                    sum += temperature.Value;
                    _cache[count++] = temperature.Value; //also updates cpu values
                }
            }

            if (count > 0)
            {
                _avg = sum / count;
                return true;
            }

            return false;
        }

        public bool UpdateCPUTemperature(int cpuIndex)
        {
            if (cpuIndex < 0 || cpuIndex > _cpusTemperatureSensors.Count) return false;

            float temperature;
            if (_cache.TryGetValue(cpuIndex, out temperature)) _avg = temperature;
            else
            {
                temperature = _cpusTemperatureSensors[cpuIndex].GetValueFromTuple().Value;
                _avg = temperature;
                _cache[cpuIndex] = temperature;
            }

            return true;
        }

        public float? GetAvgTemperature(bool forceUpdate = false)
        {
            if (_avg == null && forceUpdate) UpdateAvgTemperature();
            return _avg;
        }

        public float? GetCPUTemperature(int cpuIndex, bool forceUpdate = false)
        {
            float temperature;
            if (_cache.TryGetValue(cpuIndex, out temperature)) return temperature;
            else if (forceUpdate && UpdateCPUTemperature(cpuIndex)) return _cache[cpuIndex];
            else return null;
        }
    }
}
