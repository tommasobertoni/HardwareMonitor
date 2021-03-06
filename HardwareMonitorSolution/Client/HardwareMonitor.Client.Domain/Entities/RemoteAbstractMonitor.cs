﻿using System.ComponentModel;
using static System.Threading.Thread;

namespace HardwareMonitor.Client.Domain.Monitors
{
    public abstract class RemoteAbstractMonitor
    {
        public const string SERVICE_NAME = "HardwareMonitorWinService";

        public const int SMALL_TICK = 500;

        public delegate void EventHandler();
        public event EventHandler OnEventTriggered;

        private BackgroundWorker _bworker;

        public RemoteAbstractMonitor()
        {
            InitWorker();
        }

        private void InitWorker()
        {
            _bworker = new BackgroundWorker();
            _bworker.WorkerReportsProgress = true;
            _bworker.WorkerSupportsCancellation = true;
            _bworker.DoWork += _bworker_DoWork;
            _bworker.ProgressChanged += _bworker_ProgressChanged;
        }
        
        protected virtual void StartWorkerAsync()
        {
            if (!_bworker.IsBusy) _bworker.RunWorkerAsync();
        }

        public virtual void StopWorkerAsync() => _bworker.CancelAsync();

        private void _bworker_ProgressChanged(object sender, ProgressChangedEventArgs e) => OnEventTriggered?.Invoke();

        private void _bworker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.ReportProgress(0);

            int elapsedTime = 0;
            while (!worker.CancellationPending)
            {
                Sleep(SMALL_TICK);
                if (!worker.CancellationPending && TriggerEvent(elapsedTime += SMALL_TICK))
                {
                    worker.ReportProgress(0);
                    elapsedTime = 0;
                }
            }
        }

        protected abstract bool TriggerEvent(int elapsedTime);

        public abstract int? GetCPUsCount();
    }
}
