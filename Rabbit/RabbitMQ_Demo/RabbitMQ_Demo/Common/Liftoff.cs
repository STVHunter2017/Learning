using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Point {
        public string X;
        public string Y;
    } 
    public class Liftoff
    {
        public double minRMS;
        public double maxRMS;
        public double thresholdRMS;
        public DateTime baselineStartTime;
        public DateTime baselineEndTime;

        public DateTime liftoffTime;
        public double liftoffRMS;
        public double liftoffSpeed;        

        public Point[] SpeedData;
        public Point[] AcousticData;
    }
}
