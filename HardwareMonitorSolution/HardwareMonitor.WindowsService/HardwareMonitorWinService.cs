using HardwareMonitor.WindowsService.TemperatureWCF;
using System.ServiceModel;
using System.ServiceProcess;
using static System.Math;

namespace HardwareMonitor.WindowsService
{
    public partial class HardwareMonitorService : ServiceBase
    {
        public const int DEFAULT_UPDATE_TIME_SPAN = 2000;
        public const int MIN_UPDATE_TIME_SPAN = 1000;

        protected readonly int CURRENT_UPDATE_TIME_SPAN;

        internal static ServiceHost _temperatureHost = null;

        public HardwareMonitorService() : this(DEFAULT_UPDATE_TIME_SPAN)
        { }

        public HardwareMonitorService(int stmillis)
        {
            InitializeComponent();
            CURRENT_UPDATE_TIME_SPAN = Max(stmillis, MIN_UPDATE_TIME_SPAN);
        }

        protected override void OnStart(string[] args)
        {
            #region Temperature Host
            _temperatureHost?.Close();
            _temperatureHost = new ServiceHost(new HardwareMonitorTemperatureWinService(CURRENT_UPDATE_TIME_SPAN));
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
