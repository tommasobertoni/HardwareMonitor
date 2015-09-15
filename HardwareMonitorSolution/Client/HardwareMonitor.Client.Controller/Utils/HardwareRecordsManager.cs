using static HardwareMonitor.Client.Domain.Utils.LogsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace HardwareMonitor.Client.Controller.Utils
{
    class HardwareRecordsManager
    {
        public static bool Save(HardwareMonitorType hwmType, ICollection<Tuple<float, DateTime>> values, out string currentFolderName)
        {
            currentFolderName = null;
            try
            {
                if (values.Count < 1) return false;

                var mainRecordsFolder = $"{GetLogsFolderPath()}\\Records";
                if (!Directory.Exists(mainRecordsFolder)) Directory.CreateDirectory(mainRecordsFolder);

                currentFolderName = $"{mainRecordsFolder}\\{DateTime.Now.ToString("yyyy_MM_dd")}";
                if (!Directory.Exists(currentFolderName)) Directory.CreateDirectory(currentFolderName);

                StringBuilder sb = new StringBuilder();

                var fileName = $"{currentFolderName}\\{hwmType}.dat";

                int n = 0;
                while (File.Exists(fileName))
                    fileName = $"{currentFolderName}\\{hwmType} ({++n}).dat";

                File.WriteAllLines(fileName, values.Select(ValuesFormatter));

                return true;
            }
            catch (Exception ex)
            {
                Log($"HardwareRecordsManager Save: {ex}", LogLevel.Warning);
                return false;
            }
        }

        //maybe from config file would be nice?
        private static string ValuesFormatter(Tuple<float, DateTime> timedValues) =>
            $"{timedValues.Item2.ToString("dd/MM@HH:mm:ss")}\t{timedValues.Item1.ToString(CultureInfo.InvariantCulture)}";
    }
}
