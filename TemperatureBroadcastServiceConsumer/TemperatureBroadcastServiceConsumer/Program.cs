using System;
using System.ServiceModel;

namespace TemperatureBroadcastServiceConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var callback = new MyServiceCallback();
            
            var instanceContext = new InstanceContext(callback);
            var service = new TemperatureBroadcastServiceReference.TemperatureWCFClientBroadcastClient(instanceContext);
            var client = service.ChannelFactory.CreateChannel();
            client.SubscribeClient();
            Console.WriteLine("Client subscribed");

            Console.ReadLine();
            client.UnsubscribeClient();
            Console.WriteLine("Client unsubscribed");
            service.Close();
            Console.WriteLine("Service closed");
            Console.ReadLine();
        }
    }
}
