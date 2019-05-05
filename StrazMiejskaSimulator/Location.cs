using System;
using System.Collections.Generic;

namespace StrazMiejskaSimulator
{
    class Location
    {
        public ELocations type { get; private set; }
        public string name { get; private set; }
        public List<string> Descriptions { get; private set; }
        public Dictionary<string, int> PossibleIncidents { get; private set; }
        public Dictionary<string, int> PossibleMobs { get; private set; }

        public enum ELocations
        {
            Marketplace,
            ParkingLot,
            Church,
            GreenSquare
        }

        public Location(ELocations givenType)
        {
            GenerateLocation(givenType);
        }

        void GenerateLocation(ELocations location)
        {
            type = location;
            name = GetELocationsName(type);
            Descriptions = GetELocationsDescriptions(type);
            PlainFileReader reader = PlainFileReader.Instance;
            PossibleIncidents = reader.ReadFileToDictionary(reader.GetFilePathFor("Incidents", location));
            PossibleMobs = reader.ReadFileToDictionary(reader.GetFilePathFor("PossibleMobs", location));
        }

        public void DisplayLocationDescription()
        {
            Random rnd = new Random();
            Console.WriteLine(Descriptions[rnd.Next(0, Descriptions.Count)]);
        }

        private string GetELocationsName(ELocations type)
        {
            Database database = Database.Instance;
            string[,] Names = database.GetDataFor(Database.EData.LocationNames);
            for (int i = 0; i < Names.GetLength(0); i++)
            {
                if (Names[i, 0] == Enum.GetName(typeof(ELocations), type))
                {
                    return Names[i, 1]; 
                }
            }

            throw new ArgumentException("No name was found for given ELocation type: " + Enum.GetName(typeof(ELocations), type));
        }

        private List<string> GetELocationsDescriptions(ELocations type)
        {
            Database database = Database.Instance;
            List<string> LocationDescriptions = new List<string>();
            string[,] AllDescriptions = database.GetDataFor(Database.EData.LocationDescriptions);
            for (int i = 0; i < AllDescriptions.GetLength(0); i++)
            {
                if (AllDescriptions[i, 0] == Enum.GetName(typeof(ELocations), type))
                {
                    LocationDescriptions.Add(AllDescriptions[i, 1]);
                }
            }

            if (LocationDescriptions.Count > 0)
            {
                return LocationDescriptions;
            }
                    
           throw new ArgumentException("No name was found for given ELocation type: " + Enum.GetName(typeof(ELocations), type));
        }
    }
}
