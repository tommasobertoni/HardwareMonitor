using HardwareMonitor.Client.Temperature.Utils;

namespace HardwareMonitor.Client.Controller.Monitors
{
    public class TemperatureMonitor : AbstractMonitor
    {
        public TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient Service { get; set; }

        private TemperatureUISettingsHandler _settings;

        public TemperatureMonitor() : base()
        {
            _settings = new TemperatureUISettingsHandler();
        }

        protected override bool TriggerEvent(int elapsedTime) => elapsedTime >= _settings.UpdateTime;

        public override int? GetCPUsCount() => Service?.GetCPUsCount();

        public float? GetAvgCPUsTemperature() => Service?.GetAvgCPUsTemperature();

        public float? GetCPUTemperature(int cpuIndex) => Service?.GetCPUTemperature(cpuIndex);
    }
}
