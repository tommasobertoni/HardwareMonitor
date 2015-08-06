using System;

namespace HardwareMonitor.Client.Domain.Contracts
{
    public interface IView
    {
        string Name { get; set; }

        void Show(bool resetPosition = false);

        void Hide();

        void Close();
        

        event EventHandler OnViewExit;
        event EventHandler OnRequestUpdate;
    }
}
