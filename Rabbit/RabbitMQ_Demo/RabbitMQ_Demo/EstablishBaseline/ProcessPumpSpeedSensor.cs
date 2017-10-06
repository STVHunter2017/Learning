using Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Baseline
{
    public class ProcessPumpSpeedSensor
    {
        string server = Constants.Server;
        MqttClient client = null;
        string[] topic = new[] { Constants.PumpSpeedSensor };
        byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
        bool isBaselineEstablished = false;
        DateTime baselineEstablishTimeStart;
        bool waitTimerInitiated = false;
        private int siteID = 1;
        private int assetID = 1;
        public ProcessPumpSpeedSensor()
        {
            client = new MqttClient(server);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            var clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
        }

        public void Subscribe()
        {
            client.Subscribe(topic, qosLevels);
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var str = Encoding.Default.GetString(e.Message);
            CustomMessage cMessage = JSON.Convert(str);
            //Console.WriteLine("Speed is {0}", cMessage.Value);
            if (int.Parse(cMessage.Value) >= 3000 && int.Parse(cMessage.Value) <= 3300) // indicates pump is in lift off state.
            {   
                if (!isBaselineEstablished)
                {
                    double waitInSeconds = 0;
                    //Console.WriteLine("Trying to etablish baseline");
                    if (!waitTimerInitiated)
                    {
                        baselineEstablishTimeStart = cMessage.EventDate;
                        waitTimerInitiated = true;
                    } else
                    {
                        TimeSpan t = cMessage.EventDate.Subtract(baselineEstablishTimeStart);
                        waitInSeconds = t.TotalSeconds;                        
                        if (waitInSeconds >= 60) // set to 1 minute(s) for now
                        {
                            //Console.WriteLine("waitInSeconds: " + waitInSeconds);
                            // We can now establish baseline parameters for this seal from the database.
                            //Console.WriteLine("Liftoff calculations possible once Pump State changes");                           
                            isBaselineEstablished = true;
                            waitTimerInitiated = false;
                        }
                    }
                    Console.WriteLine("Pump is in liftoff state - waitInSeconds: {0}", waitInSeconds);
                } else
                {
                    Console.WriteLine("Pump is in liftoff state - liftoff conditions met");
                }

            } else if (int.Parse(cMessage.Value) < 3000) // pump is either stopping or in stopped state
            {
                Console.WriteLine("Pump is either stopping or in stopped state");
                if (isBaselineEstablished) CalculateLiftOff(baselineEstablishTimeStart, cMessage.EventDate, false);                
                isBaselineEstablished = false;
                waitTimerInitiated = false;
            } else // pump is running
            {
                Console.WriteLine("Pump is running");                
                if (isBaselineEstablished) CalculateLiftOff(baselineEstablishTimeStart, cMessage.EventDate, true);
                isBaselineEstablished = false;
                waitTimerInitiated = false;
            }
        }

        private void CalculateLiftOff(DateTime startDate, DateTime endDate, bool lastValue)
        {
            Console.WriteLine("Proceeding to do liftoff calculations");
            double minRMS, maxRMS, thresholdRMS;
            if (CalculateRMSThreshold(startDate, endDate, out minRMS, out maxRMS, out thresholdRMS))
            {
                double liftoffRMS, liftoffSpeed;
                DateTime liftoffTime;
                CalculateLiftoffSpeed(startDate, endDate, lastValue, thresholdRMS, 
                    out liftoffRMS, out liftoffTime, out liftoffSpeed);
                Liftoff lift = 
                        CreateLiftoffObject(startDate, endDate, 
                        minRMS, maxRMS, thresholdRMS, liftoffRMS, liftoffSpeed, liftoffTime);
                JSON.WriteObjectToFile(lift);
                CSVUtil.WriteObjectToFile(lift);
            }
        }

        private bool CalculateRMSThreshold(DateTime startDate, DateTime endDate, out double minRMS, out double maxRMS,
            out double thresholdRMS)
        {
            bool bReturn = false;
            minRMS = maxRMS = thresholdRMS = 0;
            IList<double> values = new List<double>();
            IRawDataRepository rawDataRepository = new RawDataRepository();
            IEnumerable<BsonDocument> bsonData =
                rawDataRepository.Get(siteID, assetID, Constants.PumpAcousticSensor_RMS, startDate, endDate);
            foreach (BsonDocument doc in bsonData)
            {
                values.Add(doc["value"].AsDouble);
            }
            if (values.Count > 0)
            {
                minRMS = values.Min();
                maxRMS = values.Max();
                thresholdRMS = minRMS + 0.1 * (maxRMS - minRMS);
                bReturn = true;
                Console.WriteLine("minRMS: {0}, maxRMS: {1}, thresholdRMS: {2}", minRMS, maxRMS, thresholdRMS);
            }
            return bReturn;
        }

        private bool CalculateLiftoffSpeed(DateTime startDate, DateTime endDate, bool lastValue, double thresholdRMS,
            out double liftoffRMS, 
            out DateTime liftoffTime, out double liftoffSpeed)
        {
            bool bReturn = false;
            liftoffRMS = liftoffSpeed = 0;
            liftoffTime = DateTime.MinValue;

            IRawDataRepository rawDataRepository = new RawDataRepository();
            IEnumerable<BsonDocument> bsonData =
                rawDataRepository.Get(siteID, assetID, Constants.PumpAcousticSensor_RMS, thresholdRMS, startDate, endDate);
            if (bsonData?.Count() > 0)
            {
                // get liftoff RMS and liftoff Time
                BsonDocument doc;
                if (lastValue)
                {
                    doc = bsonData.ElementAt(bsonData.Count() - 1);
                }
                else
                {
                    doc = bsonData.ElementAt(0);
                }

                liftoffTime = doc["event_date"].ToUniversalTime().ToLocalTime();
                liftoffRMS = doc["value"].AsDouble;
                Console.WriteLine("liftoff RMS: {0}", liftoffRMS);
                Console.WriteLine("liftoff Time: {0}", liftoffTime);

                // get liftoff speed from the database
                bsonData = rawDataRepository.Get(siteID, assetID, Constants.PumpSpeedSensor,
                    doc["event_date"].ToUniversalTime(), doc["event_date"].ToUniversalTime().AddSeconds(1));
                if (bsonData?.Count() > 0)
                {
                    liftoffSpeed = bsonData.ElementAt(0)["value"].AsDouble;
                    Console.WriteLine("liftoff Speed: {0}", liftoffSpeed);
                    bReturn = true;
                }
            }
            return bReturn;
        }

        private Liftoff CreateLiftoffObject(DateTime startDate, DateTime endDate, double minRMS, double maxRMS,
            double thresholdRMS, double liftoffRMS, double liftoffSpeed, DateTime liftoffTime)
        {
            Liftoff lift = new Liftoff();
            lift.minRMS = minRMS;
            lift.maxRMS = maxRMS;
            lift.thresholdRMS = thresholdRMS;
            lift.liftoffRMS = liftoffRMS;
            lift.liftoffSpeed = liftoffSpeed;
            lift.liftoffTime = liftoffTime;
            lift.baselineStartTime = startDate;
            lift.baselineEndTime = endDate;

            IRawDataRepository rawDataRepository = new RawDataRepository();
            IEnumerable<BsonDocument> bsonData =
                rawDataRepository.Get(siteID, assetID, Constants.PumpSpeedSensor, startDate, endDate);
            int index = 0;
            foreach (BsonDocument doc in bsonData)
            {
                if(index == 0)
                {
                    lift.SpeedData = new Point[bsonData.Count()];                   
                }               
                lift.SpeedData[index] = new Point();
                lift.SpeedData[index].X = doc["event_date"].ToUniversalTime().ToLocalTime().ToString();
                lift.SpeedData[index].Y = doc["value"].AsDouble.ToString();
                index++;
            }
            index = 0;
            bsonData =
                rawDataRepository.Get(siteID, assetID, Constants.PumpAcousticSensor_RMS, startDate, endDate);
            foreach (BsonDocument doc in bsonData)
            {
                if (index == 0)
                {
                    lift.AcousticData = new Point[bsonData.Count()];
                }
                lift.AcousticData[index] = new Point();
                lift.AcousticData[index].X = doc["event_date"].ToUniversalTime().ToLocalTime().ToString();
                lift.AcousticData[index].Y = doc["value"].AsDouble.ToString();
                index++;
            }
            return lift;
        }
    }
}
