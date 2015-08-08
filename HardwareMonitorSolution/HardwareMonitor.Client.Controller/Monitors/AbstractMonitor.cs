﻿using System.ComponentModel;
using static System.Threading.Thread;

namespace HardwareMonitor.Client.Controller.Monitors
{
    public abstract class AbstractMonitor
    {
        public const int SMALL_TICK = 500;

        private int _triggerTime;
        public int TriggetTime {
            get
            {
                return _triggerTime;
            }

            set
            {
                _triggerTime = value < 0 ? 0 : value;
            }
        }

        public delegate void EventHandler();
        public event EventHandler OnEventTriggered;

        private BackgroundWorker _bworker;

        public AbstractMonitor()
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

        public void Start()
        {
            _bworker.RunWorkerAsync();
        }

        public void Stop()
        {
            _bworker.CancelAsync();
            InitWorker();
        }

        private void _bworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (OnEventTriggered != null) OnEventTriggered();
        }

        private void _bworker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int elapsedTime = 0;
            while (!worker.CancellationPending)
            {
                Sleep(SMALL_TICK);
                if (TriggerEvent(elapsedTime += SMALL_TICK))
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