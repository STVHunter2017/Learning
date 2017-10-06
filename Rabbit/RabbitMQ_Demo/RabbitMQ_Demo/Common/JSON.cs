using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Common
{
    public class JSON
    {
        public static CustomMessage Convert(string message)
        {
            JsonSerializerSettings serSettings = new JsonSerializerSettings();
            serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.DeserializeObject<CustomMessage>(message, serSettings);
        }

        public static string Convert(Liftoff liftoff)
        {
            JsonSerializerSettings serSettings = new JsonSerializerSettings();
            serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(liftoff, serSettings);
        }

        public static void WriteObjectToFile(Liftoff liftoff)
        {
            int randomNumber = new Random().Next(1000, 2000);
            using (StreamWriter sw
                = File.CreateText(randomNumber.ToString() + "-" 
                + liftoff.baselineStartTime.ToString("MM-dd-yyyy") + ".json"))
            {
                sw.Write(JSON.Convert(liftoff));
            }
        }
    }
}
