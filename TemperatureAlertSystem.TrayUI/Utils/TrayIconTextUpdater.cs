using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureAlertSystem.TrayUI.Utils
{
    public class TrayIconTextUpdater
    {
        public delegate void OnUpdateText();

        public event OnUpdateText OnUpdateTextEvent;

        public int UPDATE_TIME_MILLIS { get; set; }

        private Thread _updateTextThread;
        private bool _isUpdating;

        public TrayIconTextUpdater()
        {
            UPDATE_TIME_MILLIS = 1000;
            _updateTextThread = new Thread(update);
            _updateTextThread.IsBackground = true;
        }

        public void StartUpdate()
        {
            if (!_isUpdating)
            {
                _isUpdating = true;
                _updateTextThread.Start();
            }
        }

        public void StopUpdate()
        {
            if (_isUpdating)
            {
                _isUpdating = false;
                _updateTextThread.Interrupt();
            }
        }

        private void update()
        {
            while (_isUpdating)
            {
                OnUpdateTextEvent();

                try
                {
                    Thread.Sleep(UPDATE_TIME_MILLIS);
                }
                catch (ThreadInterruptedException ex)
                {
                    //exception launched when stop watching
                    _updateTextThread.Join();
                }
            }
        }
    }
}
