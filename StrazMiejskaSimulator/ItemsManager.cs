using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class ItemsManager
    {
        static List<string> ItemNamesList;
        Random rnd = new Random();

        public ItemsManager()
        {

        }

        public static bool InitializeItemsList()
        {
            PlainFileReader reader = PlainFileReader.Instance;
            ItemNamesList = reader.ReadFileToList(reader.GetFilePathFor("Item"));
            return true;
        }

        public Item GenerateItem()
        {
            Item item = new Item();
            item.name = GenerateItemName();
            item.atkImpact = rnd.Next(-3, 10);
            item.hpImpact =  rnd.Next(-3, 10);
            item.IQImpact = rnd.Next(-10, 10);
            item.happinessImpact = rnd.Next(0, 2);
            return item;
        }

        string GenerateItemName()
        {
            string name = ItemNamesList[rnd.Next(0, ItemNamesList.Count)];
            return name;
        }

        public void DisplayItemStats(Item item)
        {
            string atkImpact = GetPositiveOrNegativeStat(item.atkImpact);
            string hpImpact = GetPositiveOrNegativeStat(item.hpImpact);
            string IQImpact = GetPositiveOrNegativeStat(item.IQImpact);
            string happinessImpact = GetPositiveOrNegativeStat(item.happinessImpact);
            Console.WriteLine("{0} | ATK: {1} | HP: {2} | IQ: {3} | SZCZĘŚCIE: {4}", item.name, atkImpact, hpImpact, IQImpact, happinessImpact);
        }

        string GetPositiveOrNegativeStat(int value)
        {
            string valueString;
            if (value > 0)
            {
                valueString = "+" + value.ToString();
            }
            else
            {
                valueString = value.ToString();
            }

            return valueString;
        }

    }
}
