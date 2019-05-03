using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class WelcomeScreen
    {

        public WelcomeScreen()
        {
            DisplayMessage();
            DisplayDate();
            DisplayWeather();
        }

        void DisplayMessage()
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine("Katowice, siedziba Straży Miejskiej");

        }

        void DisplayDate()
        {
            DateTime date = DateManager.GetCurrentDate();
            Console.WriteLine(date);
        }

        void DisplayWeather()
        {
           WeatherManager.Weather weather = WeatherManager.GenerateWeather();
           Console.WriteLine(weather.temperature + "°C " + weather.description);
        }
    }
}
