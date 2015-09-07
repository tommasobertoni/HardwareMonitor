using HardwareMonitor.Client.Temperature.Utils;
using System.ServiceModel;
using System.Threading;
using static HardwareMonitor.Client.Domain.Utils.LogsManager;

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

        public override int? GetCPUsCount()
        {
            try
            {
                return _service?.GetCPUsCount();
            }
            catch (CommunicationException cEx)
            {
                Handle(cEx, "GetCPUsCount");
                return null;
            }
        }

        public float? GetAvgCPUsTemperature()
        {
            try
            {
                return _service?.GetAvgCPUsTemperature();
            }
            catch (CommunicationException cEx)
            {
                Handle(cEx, "GetAvgCPUsTemperature");
                return null;
            }
        }

        public float? GetCPUTemperature(int cpuIndex)
        {
            try
            {
                return _service?.GetCPUTemperature(cpuIndex);
            }
            catch (CommunicationException cEx)
            {
                Handle(cEx, $"GetCPUTemperature({cpuIndex})");
                return null;
            }
        }

        public override void StopWorker()
        {
            base.StopWorker();
            _service?.Close();
        }

        private void Handle(CommunicationException cEx, string additionalInformation = null)
        {
            Log($"{$"{additionalInformation}: " ?? ""}{cEx}", LogLevel.WARNING);
        }
    }
}
