using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SensorDataSimulator
{
    public class PumpAcousticSensorKurtosis
    {
        string server = Constants.Server;
        string topic = Constants.PumpAcousticSensor_Kurtosis;
        int baseLevel = 2;
        MqttClient client = null;
        public PumpAcousticSensorKurtosis()
        {
            client = new MqttClient(server);
            client.MqttMsgPublished += client_MqttMsgPublished;

            var clientId = topic;
            client.Connect(clientId);          
        }

        public void Publish()
        {
            CustomMessage cMesage = new CustomMessage(topic, DateTime.Now, (baseLevel + new Random().NextDouble()).ToString());
            var msg = cMesage.ToString();
            client.Publish(topic, Encoding.Default.GetBytes(msg), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);            
        }

        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Message ID:" + e.MessageId + " Data Point:" + topic);
        }
    }
}
