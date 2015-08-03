using HardwareMonitor.Domain.Utils;

namespace HardwareMonitor.Domain
{
    public abstract class AbstractHardwareMonitor
    {
        private SettingsStorage _settings;

        public abstract int GetCPUsCount();

        protected SettingsStorage GetSettings()
        {
            if (_settings == null) _settings = new SettingsStorage(string.Format("HWM{0}", GetType().FullName));
            return _settings;
        }
    }
}
