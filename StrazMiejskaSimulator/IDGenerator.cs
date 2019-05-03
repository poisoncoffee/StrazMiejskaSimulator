using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class IDGenerator
    {
        static List<int> IDList = new List<int>();

        static Random rnd = new Random();

        public static int GenerateNewID()
        {
                int ID = GenerateNumber();
                if (IsNumberValid(ID))
                {
                    IDList.Add(ID);
                    return ID;
                }
            return 0;

         }

        private static int GenerateNumber()
        {
            int number = rnd.Next(1000, 9999);
            return number;
        }

        private static Boolean IsNumberValid(int number)
        {
            foreach (int ID in IDList)
            {
                if(ID == number)
                {
                    return false;
                }
            }
            return true;
        }




    }
}
