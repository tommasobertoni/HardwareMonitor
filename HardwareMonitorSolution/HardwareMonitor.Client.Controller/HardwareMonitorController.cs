using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Domain.Utils;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Threading.Thread;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "HardwareMonitor";
        private readonly static string _TEMPERATURE_UI_NAME = $"{_APPLICATION_NAME} - Temperature";

        private ITemperatureUI _temperatureUI;
        public ITemperatureUI TemperatureUI {
            get
            {
                return _temperatureUI;
            }

            set
            {
                _temperatureUI = value;

                _temperatureUI.Name = _TEMPERATURE_UI_NAME;
                _temperatureUI.OnTemperatureAlertLevelChanged += (s, e) => { };
                _temperatureUI.OnUpdateTimeChanged += (s, e) => { };
                _temperatureUI.OnObserversCountChanged += (s, e) => { };
                _temperatureUI.OnNotificationMethodChanged += (s, e) => { };
                _temperatureUI.OnViewExit += (s, e) => { };
                _temperatureUI.OnRequestUpdate += (s, e) => { };
            }
        }

        private NotifyIcon _trayIcon;

        private HardwareMonitorServiceReference.HardwareMonitorWCFContractClient _service;
        private SettingsStorage _settings;
        private BackgroundWorker _bworker;

        public HardwareMonitorController()
        {
            #region Init tray icon
            _trayIcon = new NotifyIcon()
            {
                Text = _APPLICATION_NAME,
                Visible = true,
                Icon = Properties.Resources.ohmlogo
            };

            var trayMenuStrip = new ContextMenuStrip();

            trayMenuStrip.Items.Add("Temperature Settings", Properties.Resources.Settings,
                (s, e) => TemperatureUI?.Show(true));

            trayMenuStrip.Items.Add(new ToolStripSeparator());
            trayMenuStrip.Items.Add("Exit", null, (object sender, EventArgs e) => /*Application.Exit()*/ TemperatureUI?.Hide());

            _trayIcon.ContextMenuStrip = trayMenuStrip;
            #endregion

            #region Init Background Worker
            _bworker = new BackgroundWorker();
            _bworker.WorkerReportsProgress = true;
            _bworker.WorkerSupportsCancellation = true;
            _bworker.DoWork += _bworker_DoWork;
            _bworker.ProgressChanged += _bworker_ProgressChanged;
            #endregion

            _service = new HardwareMonitorServiceReference.HardwareMonitorWCFContractClient();

            Application.ApplicationExit += (s, e) =>
            {

            };
        }

        private void _bworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void _bworker_DoWork(object sender, DoWorkEventArgs e)
        {
            int secElapsed = 0;
            while (!_bworker.CancellationPending)
            {
                Sleep(1000);
                //if (++secElapsed >= )
                _bworker.ReportProgress(0);
            }
        }
    }
}
