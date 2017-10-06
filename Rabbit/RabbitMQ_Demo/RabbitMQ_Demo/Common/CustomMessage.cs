using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CustomMessage
    {
        public DateTime EventDate { get; set; }
        public string Value { get; set; }

        public string Topic { get; set; }

        public CustomMessage(string topic, DateTime eventDate, string value)
        {
            EventDate = eventDate;
            Topic = topic;
            Value = value;            
        }
        public override string ToString()
        {
            return "{" +
                "'EventDate':'" + EventDate.ToLocalTime() +
                "', 'Value':'" + Value + '\'' +
                ", 'Topic':'" + Topic + '\'' +
                '}';
        }
    }
}
