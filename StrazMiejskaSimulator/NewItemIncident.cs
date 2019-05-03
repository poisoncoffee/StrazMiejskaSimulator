using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class NewItemIncident : Incident
    {
        ItemsManager itemsManager = new ItemsManager();
        CopManager copManager = CopManager.Instance;

        public NewItemIncident()
        {
            type = EIncidentType.NewItem;
        }

        public override bool PerformIncident(Location location)
        {
            Item item = itemsManager.GenerateItem();
            Console.WriteLine(GetDescriptionForIncident(this, String.Empty), item.name);
            itemsManager.DisplayItemStats(item);
            Console.WriteLine("Wybierz Strażnika, któremu chcesz zaekwipować przedmiot: ");
            copManager.DisplayCurrentlyHiredShort();
            Cop cop = copManager.SelectCopOrQuit();
            if (cop != null)
            {
                cop.EquipAI(item);
            }
            else
            {
                Console.WriteLine("Nie wybrano żadnego strażnika.");
            }

            return true;
        }



    }
}
