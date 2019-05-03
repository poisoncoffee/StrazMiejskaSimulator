using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class Mob : AI
    {

        Random rnd = new Random();

        public Mob(AI.EAIType givenType) : base (givenType)
        {

        }

        public override int PerformAttack(AI you, AI opponent)
        {
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



        protected override void GenerateAI()
        {
            atk = rnd.Next(5, 30);
            hp = rnd.Next(10, 60);
            happiness = rnd.Next(0, 5);
            iq = rnd.Next(50, 100);
            name = GetMobName(type);

        }

        string GetMobName(AI.EAIType type)
        {
            Database database = Database.Instance;
            string[,] MobNames = database.GetDataFor(Database.EData.MobNames);

            for(int i = 0; i < MobNames.GetLength(0); i++)
            {
                if(MobNames[i,0] == Convert.ToString(type))
                {
                    return MobNames[i, 1];
                }
            }

            throw new ArgumentOutOfRangeException("No actual name exists for this type of mob:" + type);
        }

    }
}
