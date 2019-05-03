using System;
using System.Collections.Generic;

namespace StrazMiejskaSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            InitializeComponents();
            
            while (true)
            {
                PerformSim();
            }
        }

        static void InitializeComponents()
        {
            Database database = Database.Instance;
            database.UpdateDatabase();
            DateManager dateManager = new DateManager();
            dateManager.InitializeDate();
            WeatherManager weatherManager = new WeatherManager();
            weatherManager.InitializeWeather();
            BudgetManager budgetManager = new BudgetManager();
            budgetManager.InitializeBudget();
            ItemsManager.InitializeItemsList();
            VehicleManager vehicleManager = VehicleManager.Instance;
        }

        static void PerformSim()
        {
            DateManager.NextDay();
            new WelcomeScreen();
            new MainScreen();
            CopManager.ReturnFromVacation();
            CopManager.PaySalaries();        
        }
    }
}
