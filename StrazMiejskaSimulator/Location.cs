using System;
using System.Collections.Generic;

namespace StrazMiejskaSimulator
{
    class Location
    {
        public ELocations type { get; private set; }
        public string name { get; private set; }
        public List<string> Descriptions { get; private set; }
        public Dictionary<Incident.EIncidentType, int> PossibleIncidents { get; private set; }
        public Dictionary<AI.EAIType, int> PossibleMobs { get; private set; }

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
            PossibleIncidents = GetPossibleIncidents(location);
            PossibleMobs = GetPossibleMobs(location);
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

        private Dictionary<Incident.EIncidentType, int> GetPossibleIncidents(ELocations location)
        {
            Dictionary<Incident.EIncidentType, int> Incidents = new Dictionary<Incident.EIncidentType, int>();
            Database database = Database.Instance;
            string[,] rawData = database.GetDataFor(database.GetIncidentsEDataForLocation(location));

            for(int i = 0; i < rawData.GetLength(0); i++)
            {
                foreach (Incident.EIncidentType incidentType in Enum.GetValues(typeof(Incident.EIncidentType)))
                {
                    if(Enum.GetName(typeof(Incident.EIncidentType), incidentType) == rawData[i,0])
                    {
                        Incidents.Add(incidentType, Convert.ToInt16(rawData[i, 1]));
                    }
                }
            }

            return Incidents;
        }

        private Dictionary<AI.EAIType, int> GetPossibleMobs(ELocations location)
        {
            Dictionary<AI.EAIType, int> Mobs = new Dictionary<AI.EAIType, int>();
            Database database = Database.Instance;
            string[,] rawData = database.GetDataFor(database.GetMobsEDataForLocation(location));

            for (int i = 0; i < rawData.GetLength(0); i++)
            {
                foreach (AI.EAIType aiType in Enum.GetValues(typeof(AI.EAIType)))
                {
                    if (Enum.GetName(typeof(AI.EAIType), aiType) == rawData[i, 0])
                    {
                        Mobs.Add(aiType, Convert.ToInt16(rawData[i, 1]));
                    }
                }
            }

            return Mobs;
        }
    }
}
