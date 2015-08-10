using HardwareMonitor.Client.Temperature.Utils;
using System;
using System.Threading;

namespace HardwareMonitor.Client.Controller.Monitors
{
    public class TemperatureMonitor : AbstractMonitor
    {
        public event EventHandler OnServiceReady;

        private TemperatureUISettingsHandler _settings;
        public TemperatureUISettingsHandler Settings
        {
            get
            {
                return _settings;
            }
        }

        private TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient _service;

        public bool IsServiceReady {
            get
            {
                return _service != null;
            }
        }

        public TemperatureMonitor() : base()
        {
            _settings = new TemperatureUISettingsHandler();

            new Thread(() =>
            {
                Console.WriteLine("A");
                _service = new TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient();
                Console.WriteLine("B");
                OnServiceReady?.Invoke();
                Console.WriteLine("C");
            }).Start();
        }

        protected override bool TriggerEvent(int elapsedTime) => elapsedTime >= _settings.UpdateTime;

        public override int? GetCPUsCount() => _service?.GetCPUsCount();

        public float? GetAvgCPUsTemperature() => _service?.GetAvgCPUsTemperature();

        public float? GetCPUTemperature(int cpuIndex) => _service?.GetCPUTemperature(cpuIndex);

        public override void Stop()
        {
            base.Stop();
            _service?.Close();
        }
    }
}
