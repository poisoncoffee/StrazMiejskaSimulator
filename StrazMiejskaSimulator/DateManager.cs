using System;

namespace StrazMiejskaSimulator
{
    class DateManager
    {

        static DateTime currentDate;

        public DateManager()
        {
        }

        public void InitializeDate()
        {
            currentDate = new DateTime(2018, 12, 16, 7, 30, 52);
        }
 
        public static void NextDay()
        {
            Random rnd = new Random();
            currentDate = currentDate.AddDays(1);
            currentDate = currentDate.AddHours(rnd.Next(-1, 1));
            currentDate = currentDate.AddMinutes(rnd.Next(-40, 40));
            currentDate = currentDate.AddSeconds(rnd.Next(-59, 59));
        }

        public static DateTime GetCurrentDate()
        {
            return currentDate;
        }

    }
}
