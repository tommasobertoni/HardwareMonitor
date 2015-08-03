using HardwareManager.Temperature;
using HardwareMonitor.WindowsService.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HardwareMonitor.WindowsService
{
    public partial class HardwareMonitorWinService : ServiceBase, IHardwareMonitorWCFContract
    {
        public readonly static int DEFAULT_SLEEP_TIME_MILLIS = 5000;
        public readonly static int MIN_SLEEP_TIME_MILLIS = 500;

        private BackgroundWorker _bworker;
        protected readonly int CURRENT_SLEEP_TIME_MILLIS;
        private bool _isUpdating;

        public HardwareMonitorWinService(int stmillis = -1)
        {
            InitializeComponent();
            
            if (stmillis < MIN_SLEEP_TIME_MILLIS) stmillis = DEFAULT_SLEEP_TIME_MILLIS;
            CURRENT_SLEEP_TIME_MILLIS = stmillis;

            _bworker = new BackgroundWorker();
            _bworker.WorkerReportsProgress = true;
            _bworker.DoWork += _bworker_DoWork;
            _bworker.ProgressChanged += _bworker_ProgressChanged;
        }

        private void _bworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //_isUpdating = true;
            CPUsTemperatureMonitor.INSTANCE.UpdateAvgTemperature();
            //_isUpdating = false;
        }

        private void _bworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!_bworker.CancellationPending)
            {
                /*if (!_isUpdating)*/ _bworker.ReportProgress(0);
                Thread.Sleep(CURRENT_SLEEP_TIME_MILLIS);
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
    }
}
