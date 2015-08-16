using System;
using TemperatureBroadcastServiceConsumer.TemperatureBroadcastServiceReference;

namespace TemperatureBroadcastServiceConsumer
{
    public class MyServiceCallback : TemperatureBroadcastServiceReference.ITemperatureWCFClientBroadcastCallback
    {
        void ITemperatureWCFClientBroadcastCallback.AvgCPUsTemperatureChanged(float temperature)
        {
            Console.WriteLine($"Received: {temperature}");
        }
    }
}
