using System;
using System.Collections.Generic;

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
            Database database = Database.Instance;
            string[,] WeathersDefinitions = database.GetDataFor(Database.EData.Weather);
            string description = "";
            float smallestDifference = 100.0f;

            for(int i = 0; i < WeathersDefinitions.GetLength(0); i++)
            {
                if (Math.Abs(Convert.ToSingle(WeathersDefinitions[i,0]) - temperature) < smallestDifference)
                {
                    smallestDifference = Math.Abs(Convert.ToSingle(WeathersDefinitions[i, 0]) - temperature);
                    description = WeathersDefinitions[i, 1];
                }
            }

            return description;
        }
    }
}
