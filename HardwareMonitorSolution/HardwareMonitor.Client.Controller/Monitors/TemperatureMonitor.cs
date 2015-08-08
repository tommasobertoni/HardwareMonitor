using HardwareMonitor.Client.Temperature.Utils;

namespace HardwareMonitor.Client.Controller.Monitors
{
    public class TemperatureMonitor : AbstractMonitor
    {
        private TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient _service;

        private TemperatureUISettingsHandler _settings;

        public TemperatureMonitor() : base()
        {
            _settings = new TemperatureUISettingsHandler();
        }

        protected override bool TriggerEvent(int elapsedTime) => elapsedTime >= _settings.UpdateTime;

        public override int? GetCPUsCount() => _service?.GetCPUsCount();

        public float? GetAvgCPUsTemperature() => _service?.GetAvgCPUsTemperature();

        public float? GetCPUTemperature(int cpuIndex) => _service?.GetCPUTemperature(cpuIndex);
    }
}
