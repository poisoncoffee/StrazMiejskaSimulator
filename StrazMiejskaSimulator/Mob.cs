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
            PlainFileReader reader = PlainFileReader.Instance;
            Dictionary<string, string> MobNames = reader.ReadFileToStringStringDictionary(reader.GetFilePathFor("MobNames"));

            foreach(KeyValuePair<string, string> pair in MobNames)
            {
                if(pair.Key == Convert.ToString(type))
                {
                    return pair.Value;
                }
            }

            throw new ArgumentOutOfRangeException("No actual name exists for this type of mob:" + type);
        }

    }
}
