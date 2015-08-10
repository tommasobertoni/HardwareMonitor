using System;
using System.Drawing;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IView
    {
        Image Icon { get; }

        string Name { get; set; }

        void Show(bool resetPosition = false);

        void Hide();

        void Close();


        event EventHandler<string> OnLog;
        event EventHandler OnViewExit;
        event EventHandler OnRequestUpdate;
    }
}
