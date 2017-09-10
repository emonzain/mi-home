﻿using Newtonsoft.Json.Linq;

namespace MiHomeLib.Devices
{
    public class ThSensor : MiHomeDevice
    {
        public ThSensor(string sid) : base(sid) {}

        public float? Voltage { get; private set; }
        public float? Temperature { get; private set; }
        public float? Humidity { get; private set;  }

        public override void ParseData(string command)
        {
            var jObject = JObject.Parse(command);

            if (jObject["temperature"] != null && float.TryParse(jObject["temperature"].ToString(), out float t))
            {
                Temperature = t / 100;
            }

            if (jObject["humidity"] != null && float.TryParse(jObject["humidity"].ToString(), out float h))
            {
                Humidity = h / 100;
            }
            
            if (jObject["voltage"] != null && float.TryParse(jObject["voltage"].ToString(), out float v))
            {
                Voltage = v / 1000;
            }
        }

        public override string ToString()
        {
            return $"{(!string.IsNullOrEmpty(Name) ? "Name: "+ Name +", " : string.Empty)}Temperature: {Temperature}°C, Humidity: {Humidity}%, Voltage: {Voltage}V";
        }
    }
}