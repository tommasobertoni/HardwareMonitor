using HardwareMonitor.Temperature;
using System;
using System.ServiceModel;
using static System.Math;

namespace HardwareMonitor.WindowsService.TemperatureWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class HardwareMonitorTemperatureWinService : IHardwareMonitorTemperatureWCFContract
    {
        public const int DEFAULT_UPDATE_TIME_SPAN = 2000;
        public const int MIN_UPDATE_TIME_SPAN = 1000;

        protected readonly int CURRENT_UPDATE_TIME_SPAN;
        private DateTime _lastUpdateTime;

        public HardwareMonitorTemperatureWinService() : this(DEFAULT_UPDATE_TIME_SPAN)
        { }

        public HardwareMonitorTemperatureWinService(int stmillis)
        {
            CURRENT_UPDATE_TIME_SPAN = Max(stmillis, MIN_UPDATE_TIME_SPAN);
        }

        public int GetCPUsCount()
        {
            return CPUsTemperatureMonitor.Instance.GetCPUsCount();
        }

        public float? GetAvgCPUsTemperature()
        {
            bool update;
            if (_lastUpdateTime != null)
            {
                var now = DateTime.Now;
                var lastUpdateElapsedTime = now - _lastUpdateTime;
                update = lastUpdateElapsedTime.TotalMilliseconds >= CURRENT_UPDATE_TIME_SPAN;
                if (update) _lastUpdateTime = now;
            }
            else update = true;
            
            return CPUsTemperatureMonitor.Instance.GetAvgTemperature(update);
        }

        public float? GetCPUTemperature(int cpuIndex)
        {
            return CPUsTemperatureMonitor.Instance.GetCPUTemperature(cpuIndex);
        }
    }
}
