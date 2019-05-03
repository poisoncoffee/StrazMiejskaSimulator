using System;
using System.Collections.Generic;
using StrazMiejskaSimulator.Utilities;
using System.Text;

namespace StrazMiejskaSimulator
{
    class Cop : AI
    {
       
        public int aiID { get; private set; }
        public int price { get; private set; }

        public Cop (AI.EAIType givenType) : base (givenType)
        {

        }

        void InitializeAttackList()
        {

        }

        protected override void GenerateAI()
        {
            aiID = IDGenerator.GenerateNewID();
            name = CreateCopName();
            atk = Helpers.GetRandomValue(5, 30);
            hp = Helpers.GetRandomValue(10, 60);
            happiness = Helpers.GetRandomValue(0, 5);
            price = Helpers.GetRandomValue(5, 10) * 100;
            iq = GetRandomIQ(10, 60, 110);

        }

        string CreateCopName()
        {
            Database database = Database.Instance;
            string[,] FirstNames = database.GetDataFor(Database.EData.CopFirstNames);
            string[,] LastNames = database.GetDataFor(Database.EData.CopLastNames);
            string finalName = FirstNames[Helpers.GetRandomValue(0,FirstNames.GetLength(0)),0] + " " + LastNames[Helpers.GetRandomValue(0, LastNames.GetLength(0)), 0];
            return finalName;
        }

        int GetRandomIQ(int chance, int minRange, int maxRange)
        {
            //chance = 1 means 1% chance
            int randomNumber = Helpers.GetRandomValue(minRange, maxRange);
            int randomChance = Helpers.GetRandomValue(1, 100);
            if (chance >= randomChance)
            {
                return randomNumber * (-1);
            }
            else
                return randomNumber;
        }

        public override int PerformAttack(AI you, AI opponent)
        {
            Random rnd = new Random();
            int dmg = 0;
            int randomAttack = rnd.Next(0, you.Attacks.Count);
            int index = 0;
            foreach (KeyValuePair<string, string> pair in Attacks)
            {
                if (randomAttack == index)
                {
                    dmg = CalculateDamage(pair.Value, you, opponent);
                    Console.WriteLine(pair.Key, you.name, opponent.name);

                    return dmg;
                }
                else
                {
                    index++;
                }
            }

            throw new IndexOutOfRangeException("randomAttacks went out of Attacks range!" + randomAttack + "/" + you.Attacks.Count);
        }

        string ChooseAttackType()
        {
            bool performLoop = true;

            Console.WriteLine("Twoja kolej! [a] - atak fizyczny | [i] - atak umysłowy");

            while (performLoop)
            {
                string choice = Console.ReadKey().ToString();
                switch(choice)
                {
                    case "a":
                        return "atk";
                    case "i":
                        return "iq";
                }
            }

            throw new Exception("Something went terribly wrong while selecting attack type. You went outside the loop you weren't supposed to");
        }

        public void Regenerate()
        {
            hp += Helpers.GetRandomValue(30, 60);
            happiness++;
        }

        public void DescreaseHappiness()
        {
            happiness--;
        }
    }

}
