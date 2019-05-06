using System;

namespace StrazMiejskaSimulator
{
    class PatrolManager
    {
        


        public void LaunchPatrolFlow()
        {
            DisplayMap();
            Location.ELocations choosenLocation = ChooseLocation();
            Location location = new Location(choosenLocation);
            location.DisplayLocationDescription();

            IncidentsManager incidentsManager = new IncidentsManager();
            for (int i = 0; i < CalculateNumberOfIncidents(); i++)
            {
                incidentsManager.LaunchIncidentFlow(location);
                Console.WriteLine("____________________________________________________");
            }
        }

        public void DisplayMap()
        {
            Console.WriteLine("____________________________________________________");
            Console.WriteLine("[m] - sprawdzaj pozwolenia na handel na Targowisku");
            Console.WriteLine("[p] - idź wypisywać mandaty na Parking");
            Console.WriteLine("[g] - szukaj pijaczków w Parku");
            Console.WriteLine("[c] - idź do Kościoła");
            Console.WriteLine("____________________________________________________");
        }

        Location.ELocations ChooseLocation()
        {
            string input = null;
            bool displayMessageAgain = false;
            Location.ELocations choice = new Location.ELocations();

            do
            {
                displayMessageAgain = false;

                input = Console.ReadLine().ToString();

                switch (input)
                {
                    case "m":
                        Console.WriteLine("Wybrano Targowisko");
                        choice = Location.ELocations.Marketplace;
                        break;
                    case "p":
                        Console.WriteLine("Wybrano Parking");
                        choice = Location.ELocations.ParkingLot;
                        break;
                    case "c":
                        Console.WriteLine("Wybrano Kościół");
                        choice = Location.ELocations.Church;
                        break;
                    case "g":
                        Console.WriteLine("Wybrano Park");
                        choice = Location.ELocations.GreenSquare;
                        break;
                    default:
                        Console.WriteLine("Wprowadzono niepoprawne dane");
                        displayMessageAgain = true;
                        break;
                }
            }

            while (displayMessageAgain);

            return choice;
        }

        public Location.ELocations StringToLocationConverter(string locationString)
        {
            foreach (Location.ELocations location in Enum.GetValues(typeof(Location.ELocations)))
            {
                if (nameof(location) == locationString)
                {
                    return location;
                }
            }
            throw new ArgumentException();
        }

        int CalculateNumberOfIncidents()
        {
            int numberOfPatrols = 3;
            numberOfPatrols += VehicleManager.currentVehicle.extraPatrol;
            return numberOfPatrols;
        }



    }
}
