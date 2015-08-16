using System.ServiceModel;

namespace HardwareMonitor.Client.TemperatureWCF
{
    /// <summary>
    /// http://stackoverflow.com/questions/5739501/how-can-a-wcf-service-raise-events-to-its-clients#answer-5739663
    /// </summary>
    public interface IWCFTemperatureWatcher
    {
        [OperationContract(IsOneWay = true)]
        void AvgCPUsTemperatureChanged(float temperature);
    }

    [ServiceContract(CallbackContract = typeof(IWCFTemperatureWatcher))]
    public interface ITemperatureWCFClientBroadcast
    {
        [OperationContract(IsOneWay = true)]
        void SubscribeClient();

        [OperationContract(IsOneWay = true)]
        void UnsubscribeClient();
    }
}
