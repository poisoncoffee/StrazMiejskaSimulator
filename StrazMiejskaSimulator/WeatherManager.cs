using System;
using System.Collections.Generic;
using StrazMiejskaSimulator.Utilities;
using System.Xml;

namespace StrazMiejskaSimulator
{
    class WeatherManager
    {
        private static List<Weather> WeatherLog = new List<Weather>();

        public struct Weather
        {
            public float temperature { get; set; }
            public string description { get; set; }
        }

        public WeatherManager()
        {
        }

        public void InitializeWeather()
        {
            Random rnd = new Random();
            Weather newWeather = new Weather();
            WeatherLog.Add(newWeather);
            newWeather.temperature = rnd.Next(-10, 40);
        }

        public static Weather GenerateWeather()
        {
            Random rnd = new Random();
            Weather newWeather = new Weather();
            WeatherLog.Add(newWeather);
            newWeather.temperature = (WeatherLog[WeatherLog.Count - 2].temperature) + rnd.Next(-5,5);
            newWeather.description = GetDescriptionForTemperature(newWeather.temperature);
            return newWeather;
        }

               
        static string GetDescriptionForTemperature(float temperature)
        {
            PlainFileReader reader = PlainFileReader.Instance;
            Dictionary<int, string> WeathersList = reader.ReadFileToIntStringDictionary(reader.GetFilePathFor("Weather"));
            string description = null;
            float difference;
            float smallestDifference = 100.0f;

            foreach(KeyValuePair<int, string> pair in WeathersList)
            {
                difference = Math.Abs(Convert.ToSingle(pair.Key) - temperature);
                if(difference < smallestDifference)
                {
                    smallestDifference = difference;
                    description = pair.Value;
                }
            }            

            return description;
        }
    }
}
