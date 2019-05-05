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
            PlainFileReader reader = PlainFileReader.Instance;
            type = location;
            name = reader.ReadString(reader.GetFilePathFor("Definitions",location));
            Descriptions = reader.ReadFileToList(reader.GetFilePathFor("Descriptions", location));
            PossibleIncidents = reader.ReadFileToDictionary(reader.GetFilePathFor("Incidents", location));
            PossibleMobs = reader.ReadFileToDictionary(reader.GetFilePathFor("PossibleMobs", location));
        }

        public void DisplayLocationDescription()
        {
            Random rnd = new Random();
            Console.WriteLine(Descriptions[rnd.Next(0, Descriptions.Count)]);
        }
    }
}
