using System;

namespace Baseline
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("MQTT Client - Baselining - Subscriber");
            ProcessPumpSpeedSensor pumpSpeedSensorProcess = new ProcessPumpSpeedSensor();
            pumpSpeedSensorProcess.Subscribe();            
            Console.WriteLine("Listening....");
        }       
    }
}
