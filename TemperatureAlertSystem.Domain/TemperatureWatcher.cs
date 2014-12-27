using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TemperatureAlertSystem.Domain
{
    public class TemperatureWatcher
    {
        public const int DEFAULT_CHECK_TEMPERATURE_TIMEOUT_MILLIS = 60000;
        public const int DEFAULT_TEMPERATURE_ALERT_LEVEL = 95;
        public const int DEFAULT_TIMEOUT_STEP_MILLIS = 1000;

        public int TimeoutStepMillis { get; set; }

        public int CheckTemperatureTimeoutMillis { get; set; }

        public DateTime LastTemperatureMeasuredDatetime { get; private set; }

        public float TemperatureAlertLevel { get; set; }

        public float LastMeasuredTemperature { get; private set; }

        private Thread _watchingThread;
        private bool _watchTemperatures;
        private int _timeoutBuffer;

        public TemperatureWatcher()
        {
            TimeoutStepMillis = DEFAULT_TIMEOUT_STEP_MILLIS;
            CheckTemperatureTimeoutMillis = DEFAULT_CHECK_TEMPERATURE_TIMEOUT_MILLIS;
            TemperatureAlertLevel = DEFAULT_TEMPERATURE_ALERT_LEVEL;

            _watchingThread = new Thread(watch);
            _watchingThread.IsBackground = true;
        }

        public void Start()
        {
            if (!_watchTemperatures)
            {
                _watchTemperatures = true;
                _timeoutBuffer = CheckTemperatureTimeoutMillis;
                _watchingThread.Start();
            }
        }

        public void Stop()
        {
            _watchTemperatures = false;
            _watchingThread.Interrupt();
        }

        private void watch()
        {
            while (_watchTemperatures)
            {
                if (_timeoutBuffer >= CheckTemperatureTimeoutMillis)
                {
                    _timeoutBuffer = 0;


                    var sensors = CpuSensorsFilter.Instance.filter(SensorType.Temperature);

                    float avgTemperature = 0;
                    var validSensorsCount = 0;
                    foreach (var sensor in sensors)
                    {
                        if (sensor.Value != null)
                        {
                            avgTemperature += sensor.Value.GetValueOrDefault();
                            validSensorsCount++;
                        }
                    }

                    avgTemperature /= validSensorsCount;

                    LastMeasuredTemperature = avgTemperature;
                    LastTemperatureMeasuredDatetime = DateTime.Now;
                    OnTemperatureCheck();

                    if (avgTemperature >= TemperatureAlertLevel)
                        OnHighTemperatureLevelDetected();
                }

                try
                {
                    _timeoutBuffer += TimeoutStepMillis;
                    Thread.Sleep(TimeoutStepMillis);
                }
                catch (ThreadInterruptedException ex)
                {
                    //exception launched when stop watching
                    _watchingThread.Join();
                }
            }
        }

        public delegate void HighTemperatureLevelObserver();

        public event HighTemperatureLevelObserver OnHighTemperatureLevelDetected;

        public delegate void TemperatureCheckObserver();

        public event TemperatureCheckObserver OnTemperatureCheck;
    }
}
