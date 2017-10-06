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
    public class PumpSpeedSensor
    {
        string server = Constants.Server;
        string topic = Constants.PumpSpeedSensor;
        MqttClient client = null;
        int i = 1;
        int start = 3;
        public PumpSpeedSensor()
        {
            client = new MqttClient(server);
            client.MqttMsgPublished += client_MqttMsgPublished;

            var clientId = topic;
            client.Connect(clientId);
        }

        public void Publish()
        {
            int speed = 1;
            int mod = start % 3;
            if(mod == 0)
            {                
                if (i <= 5) speed = new Random().Next(100,3000);
                if (i > 5 && i <= 70) speed = new Random().Next(3000, 3300);
                if (i > 70 && i <= 75) speed = new Random().Next(3300,3600);
                if (i > 75)
                {
                    speed = new Random().Next(3300,3600);
                    i = 1;
                    start++;
                }
            } else if (mod == 1)
            {
                if (i <= 5) speed = new Random().Next(100, 3000);
                if (i > 5 && i <= 70) speed = new Random().Next(3000, 3300);
                if (i > 70 && i <= 75) speed = new Random().Next(100, 3000);
                if (i > 75)
                {
                    speed = new Random().Next(100, 3000);
                    i = 1;
                    start++;
                }
            } else
            {
                // scenario -- too short -- liftoff calculations cannot be done.
                if (i <= 5) speed = new Random().Next(100, 3000);
                if (i > 5 && i <= 30) speed = new Random().Next(3000, 3300);
                if (i > 30 && i <= 75) speed = new Random().Next(100, 3000);
                if (i > 75)
                {
                    speed = new Random().Next(100, 3000);
                    i = 1;
                    start++;
                }
            }          
            CustomMessage cMesage = new CustomMessage(topic, DateTime.Now, speed.ToString());
            var msg = cMesage.ToString();
            client.Publish(topic, Encoding.Default.GetBytes(msg), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            i++;
        }

        private void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Message ID:" + e.MessageId + " Data Point:" + topic);
        }
    }
}
