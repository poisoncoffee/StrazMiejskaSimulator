using System;

namespace StrazMiejskaSimulator
{
    class Mob : AI
    {

        Random rnd = new Random();

        public Mob(AI.EAIType givenType) : base (givenType)
        {

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
