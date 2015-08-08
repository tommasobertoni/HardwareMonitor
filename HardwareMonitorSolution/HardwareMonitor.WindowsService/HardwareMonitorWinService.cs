﻿using HardwareMonitor.WindowsService.TemperatureWCF;
using System;
using System.ServiceModel;
using System.ServiceProcess;
using static System.Math;

namespace HardwareMonitor.WindowsService
{
    public partial class HardwareMonitorWinService : ServiceBase
    {
        private const string _BASE_ADDRESS = "net.tcp://localhost:9292/";

        public const int DEFAULT_SLEEP_TIME_MILLIS = 2000;
        public const int MIN_SLEEP_TIME_MILLIS = 500;

        protected readonly int CURRENT_SLEEP_TIME_MILLIS;

        internal static ServiceHost _temperatureHost = null;

        public HardwareMonitorWinService() : this(DEFAULT_SLEEP_TIME_MILLIS)
        { }

        public HardwareMonitorWinService(int stmillis)
        {
            InitializeComponent();
            CURRENT_SLEEP_TIME_MILLIS = Max(stmillis, MIN_SLEEP_TIME_MILLIS);
        }

        protected override void OnStart(string[] args)
        {
            #region Temperature Host
            _temperatureHost?.Close();
            _temperatureHost = new ServiceHost(
                new HardwareMonitorTemperatureWinService(CURRENT_SLEEP_TIME_MILLIS),
                new Uri[]
                {
                    new Uri($"{_BASE_ADDRESS}{nameof(HardwareMonitorTemperatureWinService)}")
                });
            _temperatureHost.Open();
            #endregion
        }

        protected override void OnStop()
        {
            #region Temperature Host
            _temperatureHost?.Close();
            _temperatureHost = null;
            #endregion
        }
    }
}
