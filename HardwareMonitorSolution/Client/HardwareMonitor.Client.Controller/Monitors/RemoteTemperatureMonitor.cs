using HardwareMonitor.Client.Temperature.Utils;
using System.ServiceModel;
using System.Threading;
using static HardwareMonitor.Client.Domain.Utils.LogsManager;
using System;

namespace HardwareMonitor.Client.Controller.Monitors
{
    public class RemoteTemperatureMonitor : RemoteAbstractMonitor
    {
        public TemperatureUISettingsHandler Settings { get; } = new TemperatureUISettingsHandler();

        private TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient _service;

        public bool IsServiceReady => _service != null;

        public RemoteTemperatureMonitor() : base()
        {
            Settings = new TemperatureUISettingsHandler();
            OpenServiceCommunicationAsync();
        }

        private void OpenServiceCommunicationAsync()
        {
            new Thread(() =>
            {
                _service = new TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient();
                StartWorker();
            }).Start();
        }

        protected override bool TriggerEvent(int elapsedTime) => elapsedTime >= Settings.UpdateTime;

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
            base.StopWorker();
            _service = null;
            OpenServiceCommunicationAsync();
            Log($"{$"{additionalInformation}: " ?? ""}{cEx}", LogLevel.WARNING);
        }
    }
}
