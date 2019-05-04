using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{

    abstract class AI
    {
        public enum EAIType
        {
            Cop,
            Grandma,
            Drunkard,
            Priest,
            Strongman,
            Teenager
        }

        public Dictionary<EAIType, Database.EData> EdataForEAIType = new Dictionary<EAIType, Database.EData>()
        {
            { EAIType.Cop, Database.EData.CopAttacks },
            { EAIType.Grandma, Database.EData.GrandmaAttacks },   
            { EAIType.Drunkard, Database.EData.DrunkardAttacks },
            { EAIType.Priest, Database.EData.PriestAttacks },
            { EAIType.Strongman, Database.EData.StrongmanAttacks },
            { EAIType.Teenager, Database.EData.TeenagerAttacks }
        };

        public EAIType type { get; protected set; }
        public string name { get; protected set; }
        public int hp { get; protected set; }
        public int atk { get; protected set; }
        public int iq { get; protected set; }
        public int happiness { get; protected set; }
        public string[,] Attacks { get; protected set; }
        public List<Item> Inventory { get; protected set; } = new List<Item>();

        public AI(EAIType givenType)
        {
            type = givenType;
            InitializeAttacksList();
            GenerateAI();
        }

        protected void InitializeAttacksList()
        {
            Database database = Database.Instance;
            Attacks = database.GetDataFor(ConvertEdataToEAIType(type));
        }

        private Database.EData ConvertEdataToEAIType(EAIType eaiType)
        {
            foreach(KeyValuePair<EAIType, Database.EData> pair in EdataForEAIType)
            {
                if (pair.Key == eaiType)
                    return pair.Value;
            }

            throw new ArgumentException("No matching data type EData for provided EAIType: " + Enum.GetName(typeof(EAIType), eaiType));
        }

        public bool IsAlive()
        {
            if(CalculateTotalHp() <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int CalculateTotalHp()
        {
            int hpValue = 0;
            hpValue += hp;

            foreach (Item item in Inventory)
            {
                hpValue += item.hpImpact;
            }

            return hpValue;

        }

        int CalculateAttackValue()
        {
            int atkValue = 0;
            atkValue += atk;
            foreach (Item item in Inventory)
            {
                atkValue += item.atkImpact;
            }

            return atkValue;
        }

        int CalculateIqValue()
        {
            int iqValue = 0;
            iqValue += iq;
            foreach (Item item in Inventory)
            {
                iqValue += item.IQImpact;
            }

            return iqValue;
        }

        public int CalculateHappinessValue()
        {
            int happinessValue = 0;
            happinessValue += happiness;
            foreach (Item item in Inventory)
            {
                happinessValue += item.happinessImpact;
            }

            return happinessValue;
        }

        public void EquipAI(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine("{1} posiada teraz przedmiot {0}", item.name, name);
        }

        public void TakeDamage(int value)
        {
            hp -= value;
        }

        protected int CalculateDamage(string dmgType, AI you, AI opponent)
        {
            Random rnd = new Random();
            int dmg = 0;

            switch (dmgType)
            {
                case "atk":
                    dmg = (you.CalculateAttackValue() + rnd.Next(-3, 3) - opponent.CalculateAttackValue() + rnd.Next(-3, 3));
                    break;
                case "iq":
                    dmg = (you.CalculateIqValue() + rnd.Next(-3, 3) - opponent.CalculateIqValue() + rnd.Next(-3, 3));
                    break;
            }

            if (dmg > 0)
            {
                return dmg;
            }
            else
            {
                return 0;
            }
        }

        protected abstract void GenerateAI();

        public int PerformAttack(AI you, AI opponent)
        {
            Random rnd = new Random();
            int dmg = 0;
            int index = rnd.Next(0, you.Attacks.GetLength(0));
            dmg = CalculateDamage(you.Attacks[index, 1], you, opponent);
            Console.WriteLine(you.Attacks[index, 0], you.name, opponent.name);
            return dmg;
        }
    }


}

