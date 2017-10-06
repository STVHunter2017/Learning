using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CSVUtil
    {
        public static void WriteObjectToFile(Liftoff liftoff)
        {
            int randomNumber = new Random().Next(1000, 2000);
            using (StreamWriter sw
                = File.CreateText(randomNumber.ToString() + "-Speed-"
                + liftoff.baselineStartTime.ToString("MM-dd-yyyy") + ".csv"))
            {
                sw.WriteLine("Time,Speed");
                foreach(Point point in liftoff.SpeedData)
                {
                    sw.WriteLine("{0},{1}", DateTime.Parse(point.X).ToString("yyyy-MM-dd HH:mm:ss"), point.Y);
                }                
            }

            using (StreamWriter sw
                = File.CreateText(randomNumber.ToString() + "-RMS-"
                + liftoff.baselineStartTime.ToString("MM-dd-yyyy") + ".csv"))
            {
                sw.WriteLine("Time,RMS");
                foreach (Point point in liftoff.AcousticData)
                {
                    sw.WriteLine("{0},{1}", DateTime.Parse(point.X).ToString("yyyy-MM-dd HH:mm:ss"), point.Y);
                }
            }
        }
    }
}
