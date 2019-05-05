using System;
using System.Collections.Generic;

namespace StrazMiejskaSimulator
{
    class CopManager
    {
        private static List<Cop> CurrentlyHired;
        private static List<Cop> CurrentlyOnVacation;
        private static readonly CopManager instance = new CopManager();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        // czemu

        ItemsManager itemsManager = new ItemsManager();

        private CopManager()
        {
            CurrentlyHired = new List<Cop>();
            CurrentlyOnVacation = new List<Cop>();
        }

        public static CopManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void LauchHireFlow()
        {
            List<Cop> Pretendents = CreatePretendentsList();
            DisplayCopsFromList(Pretendents);
            DisplayHireDialog(Pretendents);
        }


        List<Cop> CreatePretendentsList()
        {
            List<Cop> Pretendents = new List<Cop>();
            for (int i = 0; i < 3; i++)
            {
                Cop cop = new Cop(AI.EAIType.Cop);
                Pretendents.Add(cop);
            }
            return Pretendents;
        }

        void DisplayHireDialog(List<Cop> Pretendents)
        {
            bool exit = false;
            string input;
            do
            {
                bool copFound = false;
                Console.WriteLine("Wpisz numer w nawiasach [****] aby zatrudnić danego Strażnika. Wpisz [q] aby zakończyć zatrudnianie.");
                input = Console.ReadLine().ToString();

                foreach (Cop cop in Pretendents)
                {
                    if (input == cop.aiID.ToString())
                    {
                        copFound = true;
                        if (IsAllowedToHire())
                        {
                            Hire(cop);
                            Pretendents.Remove(cop);
                            break;
                        }
                        else
                        Console.WriteLine("Niestety, nie możesz zatrudnić tego Stażnika");
                    }
                }
                if (input == "q")
                {
                    exit = true;
                    break;
                }
                else if (Pretendents.Count == 0)
                {
                    Console.WriteLine("Nie ma już więcej kandydatów.");
                    exit = true;
                    break;
                }
                else if (!copFound)
                {
                    Console.WriteLine("Wprowadzono niepoprawne dane lub zatrudniono już tego Strażnika.");
                }
            }
            while (!exit);
        }

        public bool IsAllowedToHire()
        {  
           return true;
        }      

        void Hire(Cop cop)
        {
            CurrentlyHired.Add(cop);
            Console.WriteLine("Zatrudniono Strażnika " + cop.name + "! Otrzymuje on numer służbowy: [" + cop.aiID + "].");           

        }

        public static bool FireCopByID(int id)
        {
            foreach (Cop cop in CurrentlyHired)
            {
                if (id == cop.aiID)
                {
                    CurrentlyHired.Remove(cop);
                    Console.WriteLine("[!!!] Tracisz Strażnika " + cop.name);
                    return true;
                }
            }

            Console.WriteLine("Podano nieprawidłowe ID Strażnika. Nie można go wyrzucić.");
            return false;
        }

        public List<Cop> GetHiredCopList()
        {
            List<Cop> CopList = CurrentlyHired;
            return CopList;
        }

        public bool IsAnyoneHired()
        {
            if (CurrentlyHired.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DisplayCurrentlyHired()
        {
            DisplayCopsFromList(CurrentlyHired);
        }

        public void DisplayCopsFromList(List<Cop> copList)
        {
            foreach (Cop cop in copList)
            {
                Console.WriteLine("");
                Console.WriteLine("[{0}]\t{1}", cop.aiID, cop.name);
                Console.WriteLine("ATK: {0} | HP: {1} | IQ: {2} | SZCZĘŚCIE: {3}", cop.atk, cop.hp, cop.iq, cop.happiness);
                Console.WriteLine("Koszt dniówki: ----------{0} zł", cop.price);
                Console.WriteLine("Zaekwipowane przedmioty: ");
                if (cop.Inventory.Count == 0)
                {
                    Console.WriteLine("----------brak");
                }
                else
                {
                    foreach (Item item in cop.Inventory)
                    {
                        itemsManager.DisplayItemStats(item);
                    }
                }
            }

        }

        public void DisplayCurrentlyHiredShort()
        {
            ItemsManager itemsManager = new ItemsManager();

            foreach (Cop cop in CurrentlyHired)
            {
                Console.WriteLine("");
                Console.WriteLine("[{0}]\t{1}", cop.aiID, cop.name);
                Console.WriteLine("ATK: {0} | HP: {1} | IQ: {2} | SZCZĘŚCIE: {3}", cop.atk, cop.hp, cop.iq, cop.happiness);
                Console.WriteLine("Zaekwipowane przedmioty: ");
                if (cop.Inventory.Count == 0)
                {
                    Console.WriteLine("----------brak");
                }
                else
                {
                    foreach (Item item in cop.Inventory)
                    {
                        itemsManager.DisplayItemStats(item);
                    }
                }
            }
        }

        public Cop SelectCop()
        {
                while (true)
                {
                    string input = Console.ReadLine().ToString();

                    foreach (Cop cop in CurrentlyHired)
                    {
                        if (input == cop.aiID.ToString())
                        {
                            return cop;
                        }
                    }
                    Console.WriteLine("Wprowadzono nieprawidłowe ID. Wpisz ID ponownie:");
                }
           
        }

        public Cop SelectCopOrQuit()
        {
            if (CurrentlyHired.Count != 0)
            {
                bool repeat = true;

                while (repeat)
                {
                    string input = Console.ReadLine().ToString();

                    foreach (Cop cop in CurrentlyHired)
                    {
                        if (input == cop.aiID.ToString())
                        {
                            return cop;
                        }
                        else if (input == "q")
                        {
                            repeat = false;
                        }
                    }
                    Console.WriteLine("Wprowadzono nieprawidłowe ID. Wpisz ID ponownie:");
                }
            }
            else
            {
                Console.WriteLine("Brak zatrudnionych Strażników.");
            }

            return null;
        }

        public static void PaySalaries()
        {
            Console.ReadKey();
            Console.WriteLine("____________________________________________________");

            for (int i = 0; i < CurrentlyHired.Count; i++)
            {
                if(BudgetManager.AlterBudget(CurrentlyHired[i].price * (-1)))
                {
                    Console.WriteLine("Wypłacono pensję w wysokości {0}zł Strażnikowi {1}", CurrentlyHired[i].price, CurrentlyHired[i].name);
                }
                else
                {
                    if(CurrentlyHired[i].CalculateHappinessValue() > 0)
                    {
                        Console.WriteLine("Strażnik {0} nie dostał wypłaty i czuje się teraz nieszczęśliwy w swojej pracy. Traci 1 punkt Szczęścia.", CurrentlyHired[i].name);
                        CurrentlyHired[i].DescreaseHappiness();
                    }
                    else
                    {
                        Console.WriteLine("Strażnik {0} nie dostał wypłaty, w związku z czym odchodzi z pracy.", CurrentlyHired[i].name);
                        FireCopByID(CurrentlyHired[i].aiID);
                        i--;
                    }
                }
            }
        }

        void SendForVacation(Cop cop)
        {
            CurrentlyOnVacation.Add(cop);
            CurrentlyHired.Remove(cop);
        }

        public static void ReturnFromVacation()
        {
            foreach(Cop cop in CurrentlyOnVacation)
            {
                cop.Regenerate();
                CurrentlyHired.Add(cop);
                Console.WriteLine("Strażnik {0} wraca z urlopu! Czuje się świetnie - ma teraz {1} HP i {2} punktów Szczęścia! ", cop.name, cop.hp, cop.happiness);
            }

            CurrentlyOnVacation.Clear(); ;
            Console.WriteLine("____________________________________________________");
        }

        public void ManageCopsMenu()
        {
            DisplayCurrentlyHired();
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("Wybierz Strażnika którym chcesz zarządzać. Wpisz [q] aby zakończyć.");
            Console.WriteLine("____________________________________________________");
            Cop choosenCop = SelectCopOrQuit();
            if (choosenCop != null)
            {
                Console.WriteLine("Wybierz, co chcesz zrobić z danym Strażnikiem. Wpisz [q] aby zakończyć.");
                Console.WriteLine("[f] - Zwolnij");
                Console.WriteLine("[v] - Wyślij na urlop");
                Console.WriteLine("[q] - Wyjdź");
                string input = Console.ReadLine().ToString();

                switch(input)
                {
                    case "f":
                        FireCopByID(choosenCop.aiID);
                        break;
                    case "v":
                        SendForVacation(choosenCop);
                        break;
                    case "q":
                        break;
                    default:
                        Console.WriteLine("Wprowadzono niepoprawne polecenie");
                        break;
                }
            }
        }


    }
}
