using HardwareMonitor.Client.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HardwareMonitor.Client.TemperatureWCF
{
    public class TemperatureObserver : ITemperatureObserver
    {
        public static readonly TemperatureObserver Instance = new TemperatureObserver();

        public readonly List<IWCFTemperatureWatcher> subscribedChannels = new List<IWCFTemperatureWatcher>();

        private TemperatureObserver()
        { }

        public float LastMeasuredAvgCPUsTemperature { get; private set; }

        void ITemperatureObserver.OnAvgCPUsTemperatureChanged(float temperature)
        {
            LastMeasuredAvgCPUsTemperature = temperature;
            lock (subscribedChannels)
            {
                List<int> disconnectedClients = new List<int>();
                for (int i = 0; i < subscribedChannels.Count; i++)
                {
                    try
                    {
                        subscribedChannels[i].AvgCPUsTemperatureChanged(LastMeasuredAvgCPUsTemperature);
                    }
                    catch (Exception)
                    {
                        disconnectedClients.Add(i);
                    }
                }

                foreach (int position in disconnectedClients)
                    subscribedChannels.RemoveAt(position);
            }
        }
    }

    /// <summary>
    /// http://stackoverflow.com/questions/5739501/how-can-a-wcf-service-raise-events-to-its-clients#answer-5739663
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class TemperatureWCFClientBroadcast : ITemperatureWCFClientBroadcast
    {
        public TemperatureWCFClientBroadcast()
        { }

        void ITemperatureWCFClientBroadcast.SubscribeClient()
        {
            var channel = OperationContext.Current.GetCallbackChannel<IWCFTemperatureWatcher>();
            if (!TemperatureObserver.Instance.subscribedChannels.Contains(channel))
            {
                TemperatureObserver.Instance.subscribedChannels.Add(channel);
                channel.AvgCPUsTemperatureChanged(TemperatureObserver.Instance.LastMeasuredAvgCPUsTemperature);
            }
        }

        void ITemperatureWCFClientBroadcast.UnsubscribeClient()
        {
            TemperatureObserver.Instance.subscribedChannels.Remove(OperationContext.Current.GetCallbackChannel<IWCFTemperatureWatcher>());
        }
    }
}
