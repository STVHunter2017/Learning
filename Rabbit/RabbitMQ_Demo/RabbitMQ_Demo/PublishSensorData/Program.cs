using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorDataSimulator;
using System.Threading;

namespace SensorDataSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MQTT Client - SensorDataSimulator - Publisher");            
            Console.WriteLine("Simulating 2 data points Pump Acoustic RMS and Pump Speed");
            Console.WriteLine("These 2 data points will be published every second");
            
            PumpAcousticSensorKurtosis pumpAcousticSensorKurtosis = new PumpAcousticSensorKurtosis();
            PumpAcousticSensorRMS pumpAcousticSensorRMS = new PumpAcousticSensorRMS();
            PumpSpeedSensor pumpSpeedSensor = new PumpSpeedSensor();
            PumpVibrationSensorAxialRMS pumpVibrationSensorAxialRMS = new PumpVibrationSensorAxialRMS();

            Thread.Sleep(1000 * 30);
            int i = 1;
            while (true)
            {                
                //pumpAcousticSensorKurtosis.Publish();
                pumpAcousticSensorRMS.Publish();                
                pumpSpeedSensor.Publish();
                //pumpVibrationSensorAxialRMS.Publish();
                Thread.Sleep(1000);
                i++;
                if(i > 75)
                {
                    i = 1;
                    Thread.Sleep(1000);                  
                }
            }
        }
    }
}
