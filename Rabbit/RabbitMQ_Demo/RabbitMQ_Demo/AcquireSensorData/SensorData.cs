using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using Common;

namespace DataStorage
{   
    class SensorData
    {
        private string server = Constants.Server;
        private string[] topic = new[] { Constants.PumpSpeedSensor, Constants.PumpAcousticSensor_Kurtosis,
                Constants.PumpAcousticSensor_RMS, Constants.PumpVibrationSensor_Axial_RMS };
        private byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,
                MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,
                MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,
                MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE};
        private MqttClient client = null;
        private readonly IRawDataRepository _rawDataRepository;
        private int siteID = 1;
        private int assetID = 1;
        public SensorData()
        {
            client = new MqttClient(server);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            var clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            _rawDataRepository = new RawDataRepository();
            _rawDataRepository.Drop(siteID, assetID);        
            _rawDataRepository.Create(siteID, assetID);
        }

        public void Subscribe()
        {
            client.Subscribe(topic, qosLevels);
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var str = Encoding.Default.GetString(e.Message);
            CustomMessage cMessage = JSON.Convert(str);            

            _rawDataRepository.Insert(siteID, assetID, cMessage.Topic, cMessage.EventDate.ToUniversalTime(), 
                Convert.ToDouble(cMessage.Value));
            Console.WriteLine(str);
        }
    }
}
