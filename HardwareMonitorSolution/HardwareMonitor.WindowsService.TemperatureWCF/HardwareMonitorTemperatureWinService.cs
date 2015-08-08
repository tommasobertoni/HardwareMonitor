using HardwareMonitor.Temperature;
using System;
using System.ServiceModel;
using System.Timers;
using static System.Math;

namespace HardwareMonitor.WindowsService.TemperatureWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class HardwareMonitorTemperatureWinService : IHardwareMonitorTemperatureWCFContract
    {
        public const int DEFAULT_SLEEP_TIME_MILLIS = 2000;
        public const int MIN_SLEEP_TIME_MILLIS = 500;

        protected readonly int CURRENT_SLEEP_TIME_MILLIS;

        private Timer _timer;

        public HardwareMonitorTemperatureWinService() : this(DEFAULT_SLEEP_TIME_MILLIS)
        { }

        public HardwareMonitorTemperatureWinService(int stmillis)
        {
            CURRENT_SLEEP_TIME_MILLIS = Max(stmillis, MIN_SLEEP_TIME_MILLIS);

            #region Init Timer
            _timer = new Timer();
            _timer.AutoReset = true;
            _timer.Interval = CURRENT_SLEEP_TIME_MILLIS;
            _timer.Elapsed += Timer_Tick;
            _timer.Start();
            #endregion
        }

        private void Timer_Tick(object sender, ElapsedEventArgs e)
        {
            CPUsTemperatureMonitor.INSTANCE.UpdateAvgTemperature();
        }

        public int GetCPUsCount()
        {
            return CPUsTemperatureMonitor.INSTANCE.GetCPUsCount();
        }

        public float? GetAvgCPUsTemperature()
        {
            return CPUsTemperatureMonitor.INSTANCE.GetAvgTemperature();
        }

        public float? GetCPUTemperature(int cpuIndex)
        {
            return CPUsTemperatureMonitor.INSTANCE.GetCPUTemperature(cpuIndex);
        }
    }
}
