using System;
using System.Collections.Generic;

namespace StrazMiejskaSimulator
{
    abstract class Incident
    {
        public enum EIncidentType
        {
            NewItem,
            Fight,
            NewVehicle,
            CashSink,
            CashSource
        }

        public EIncidentType type;
        public Location.ELocations location;

        protected Incident()
        {

        }

        public abstract bool PerformIncident(Location location);

        protected string GetDescriptionForIncident(Incident incident, string keyPart)
        {
            Database database = Database.Instance;
            string[,] Descriptions = database.GetDataFor(Database.EData.Descriptions);
            string key = Convert.ToString(type) + "_" + Convert.ToString(incident.location);
            if (keyPart != String.Empty)
            {

                key = Convert.ToString(type) + "_" + Convert.ToString(incident.location) + "_" + keyPart;
            }
            List<string> MatchingDescriptions = new List<string>();
            Random rnd = new Random();

            for (int i = 0; i < Descriptions.GetLength(0); i++)
            {
                if(Descriptions[i,0] == key)
                {
                    MatchingDescriptions.Add(Descriptions[i, 1]);
                }
            }

            if(MatchingDescriptions.Count != 0)
            {
                return MatchingDescriptions[rnd.Next(0, MatchingDescriptions.Count)];            
            }
            else
            {
                throw new ArgumentException("Could not find description for an incident: " + key);
            }
        }





    }
}
