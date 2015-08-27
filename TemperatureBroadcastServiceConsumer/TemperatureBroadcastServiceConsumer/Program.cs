using System;
using System.ServiceModel;

namespace TemperatureBroadcastServiceConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var callback = new MyServiceCallback();

            Console.WriteLine("InstanceContext");
            var instanceContext = new InstanceContext(callback);
            Console.WriteLine("service");
            var service = new TemperatureBroadcastServiceReference.TemperatureWCFClientBroadcastClient(instanceContext);
            Console.WriteLine("CreateChannel");
            var client = service.ChannelFactory.CreateChannel();
            Console.WriteLine("SubscribeClient");
            try
            {
                client.SubscribeClient();
                Console.WriteLine("Client subscribed");
            }
            catch (EndpointNotFoundException)
            {
                Console.WriteLine("Endpoint not found!");
            }

            Console.ReadLine();
            client.UnsubscribeClient();
            Console.WriteLine("Client unsubscribed");
            service.Close();
            Console.WriteLine("Service closed");
            Console.ReadLine();
        }
    }
}
