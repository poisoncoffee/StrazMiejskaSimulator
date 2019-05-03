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
            Strongman
        }

        public EAIType type { get; protected set; }
        public string name { get; protected set; }
        public int hp { get; protected set; }
        public int atk { get; protected set; }
        public int iq { get; protected set; }
        public int happiness { get; protected set; }
        public Dictionary<string,string> Attacks { get; protected set; } = new Dictionary<string, string>();
        public List<Item> Inventory { get; protected set; } = new List<Item>();

        public AI(EAIType givenType)
        {
            type = givenType;
            InitializeAttacksList();
            GenerateAI();
        }

        protected void InitializeAttacksList()
        {
        PlainFileReader reader = PlainFileReader.Instance;
        Attacks = reader.ReadFileToStringStringDictionary(reader.GetFilePathFor(Convert.ToString(type)));
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
        public abstract int PerformAttack(AI you, AI opponent);


    }

}

