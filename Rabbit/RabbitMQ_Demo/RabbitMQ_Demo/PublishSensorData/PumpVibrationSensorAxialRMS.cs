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
    public class PumpVibrationSensorAxialRMS
    {
        string server = Constants.Server;
        string topic = Constants.PumpVibrationSensor_Axial_RMS;
        MqttClient client = null;
        public PumpVibrationSensorAxialRMS()
        {            
            client = new MqttClient(server);
            client.MqttMsgPublished += client_MqttMsgPublished;

            var clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);            
        }

        public void Publish()
        {
            CustomMessage cMesage = new CustomMessage(topic, DateTime.Now, new Random().NextDouble().ToString());
            var msg = cMesage.ToString();
            client.Publish(topic, Encoding.Default.GetBytes(msg), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
        }

        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Message ID:" + e.MessageId + " Data Point:" + topic);
        }
    }
}
