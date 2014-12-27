using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureAlertSystem.Domain
{
    internal class CpuSensorsFilter
    {
        private Computer _computer;

        private CpuSensorsFilter()
        {
            _computer = new Computer()
            {
                CPUEnabled = true
            };
            _computer.Open();
        }

        public readonly static CpuSensorsFilter Instance = new CpuSensorsFilter();

        public List<ISensor> filter(SensorType requestedSensorType)
        {
            var requestedSensors = new List<ISensor>();

            var cpus = _computer.Hardware.ToList().Select(hardware => hardware.HardwareType == HardwareType.CPU);

            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType != HardwareType.CPU) continue;

                hardware.Update();
                var cpuSensors = hardware.Sensors;

                foreach (var sensor in cpuSensors)
                    if (sensor.SensorType == requestedSensorType)
                        requestedSensors.Add(sensor);
            }

            return requestedSensors;
        }
    }
}
