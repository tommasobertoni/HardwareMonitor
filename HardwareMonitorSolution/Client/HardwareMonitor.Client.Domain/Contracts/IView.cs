using HardwareMonitor.Client.Domain.Entities;
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

        void ForceTheme(Theme theme);

        event EventHandler<string> OnNotification;
        event EventHandler OnViewExit;
        event EventHandler OnRequestUpdate;
    }
}
