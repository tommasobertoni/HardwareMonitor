using HardwareMonitor.Temperature;
using HardwareMonitor.WindowsService.WCF;
using System;
using System.ComponentModel;
using System.ServiceProcess;
using static System.Math;
using static System.Threading.Thread;

namespace HardwareMonitor.WindowsService
{
    public partial class HardwareMonitorWinService : ServiceBase, IHardwareMonitorWCFContract
    {
        public const int DEFAULT_SLEEP_TIME_MILLIS = 2000;
        public const int MIN_SLEEP_TIME_MILLIS = 500;
        public const int INIT_DELAY_MILLIS = 5000;

        private BackgroundWorker _bworker;
        protected readonly int CURRENT_SLEEP_TIME_MILLIS;

        public HardwareMonitorWinService() : base()
        {
        }

        public HardwareMonitorWinService(int stmillis = DEFAULT_SLEEP_TIME_MILLIS)
        {
            InitializeComponent();
            
            CURRENT_SLEEP_TIME_MILLIS = Min(stmillis, MIN_SLEEP_TIME_MILLIS);

            #region Init Background Worker
            _bworker = new BackgroundWorker();
            _bworker.WorkerReportsProgress = true;
            _bworker.WorkerSupportsCancellation = true;
            _bworker.DoWork += _bworker_DoWork;
            _bworker.ProgressChanged += _bworker_ProgressChanged;
            #endregion
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        private int c;

        private void _bworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CPUsTemperatureMonitor.INSTANCE.UpdateAvgTemperature();
        }

        private void _bworker_DoWork(object sender, DoWorkEventArgs e)
        {
            Sleep(INIT_DELAY_MILLIS);
            while (!_bworker.CancellationPending)
            {
                Sleep(CURRENT_SLEEP_TIME_MILLIS);
                _bworker.ReportProgress(0);
            }
        }

        protected override void OnStart(string[] args)
        {
            _bworker.RunWorkerAsync();
        }

        protected override void OnStop()
        {
            _bworker.CancelAsync();
        }

        public float? GetAvgCPUsTemperature()
        {
            return CPUsTemperatureMonitor.INSTANCE.GetAvgTemperature();
        }

        public float? GetCPUTemperature(int cpuIndex)
        {
            return CPUsTemperatureMonitor.INSTANCE.GetCPUTemperature(cpuIndex);
        }

        public int GetCPUsCount()
        {
            return CPUsTemperatureMonitor.INSTANCE.GetCPUsCount();
        }
    }
}
