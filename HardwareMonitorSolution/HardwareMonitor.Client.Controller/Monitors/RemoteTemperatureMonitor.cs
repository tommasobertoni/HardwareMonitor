using HardwareMonitor.Client.Temperature.Utils;
using System;
using System.Threading;

namespace HardwareMonitor.Client.Controller.Monitors
{
    public class RemoteTemperatureMonitor : RemoteAbstractMonitor
    {
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

        public RemoteTemperatureMonitor() : base()
        {
            _settings = new TemperatureUISettingsHandler();

            new Thread(() =>
            {
                _service = new TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient();
                StartWorker();
            }).Start();
        }

        protected override bool TriggerEvent(int elapsedTime) => elapsedTime >= _settings.UpdateTime;

        public override int? GetCPUsCount() => _service?.GetCPUsCount();

        public float? GetAvgCPUsTemperature() => _service?.GetAvgCPUsTemperature();

        public float? GetCPUTemperature(int cpuIndex) => _service?.GetCPUTemperature(cpuIndex);

        public override void StopWorker()
        {
            base.StopWorker();
            _service?.Close();
        }
    }
}
