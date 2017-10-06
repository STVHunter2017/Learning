using System;

namespace DataStorage
{
    class Program
    {       
        static void Main()
        {
            Console.WriteLine("MQTT - Data Storage - Subscriber");
            SensorData sensorData = new SensorData();
            sensorData.Subscribe();
            Console.WriteLine("Listening ....");
        }       
    }
}
