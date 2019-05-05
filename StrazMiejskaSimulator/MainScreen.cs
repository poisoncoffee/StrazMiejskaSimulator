using System;

namespace StrazMiejskaSimulator
{
    class MainScreen
    {
        bool dayHasNotFinishedYet = true;
        CopManager copManager = CopManager.Instance;

        public MainScreen()
        {
            DisplayBudgetMessage();

            while (dayHasNotFinishedYet)
            {
                DisplayFeatureSelectionMenu();
                string choice = Console.ReadLine().ToString();

                switch (choice)
                {
                    case "h":
                        copManager.LauchHireFlow();
                        break;
                    case "z":
                        copManager.ManageCopsMenu();
                        break;
                    case "p":
                        if (copManager.IsAnyoneHired())
                        {
                            PatrolManager Patrol = new PatrolManager();
                            Patrol.LaunchPatrolFlow();
                            dayHasNotFinishedYet = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Nie można iść na patrol, nie zatrudniono żadnych Strażników!");
                            break;
                        }
                    default:
                        Console.WriteLine("Wprowadzono niepoprawne dane");
                        break;
                }
            }
        }

        public void DisplayBudgetMessage()
        {
            Console.WriteLine("----------------------------Stan budżetu wynosi:" + BudgetManager.GetAmount());
        }

        public void DisplayFeatureSelectionMenu()
        {
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("[z] - zarządzaj swoimi Strażnikami");
            Console.WriteLine("[h] - zatrudnij nowych Strażników");
            Console.WriteLine("[p] - wyślij swoich Strażników na patrol");
            Console.WriteLine("____________________________________________________");
        }


    }
}
