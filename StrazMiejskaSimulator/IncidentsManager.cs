using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class IncidentsManager
    {
        Random rnd = new Random();


        public IncidentsManager()
        {

        }

        public void LaunchIncidentFlow(Location location)
        {

            Incident incident = GetRandomIncident(location);

            if (incident != null)
            {
                incident.PerformIncident(location);
                Console.ReadKey();
            }
            else
            {
                throw new NullReferenceException("Could not generate Incident");
            }

        }

        Incident GetRandomIncident (Location location)
        {
           
                int sum = 0;
                int choice = 0;
                int choiceStack = 0;

                foreach (int chance in location.PossibleIncidents.Values)
                {
                    sum += chance;
                }


                choice = rnd.Next(1, sum+1);

                foreach (KeyValuePair<string, int> pair in location.PossibleIncidents)
                {
                    if (choice <= choiceStack + pair.Value)
                    {
                        Incident incident =  StringToIncidentConverter(pair.Key);
                        incident.location = location.type;
                        return incident;
                    }
                    else
                    {
                        choiceStack += pair.Value;
                    }
                }

                throw new ArgumentException();
            
        }

        public Incident StringToIncidentConverter(string incidentString)
        {
            Incident incident = null;

            switch (incidentString)
            {
                case "NewItem":
                    incident = new NewItemIncident();
                    break;
                case "Fight":
                    incident = new FightIncident();
                    break;
                case "NewVehicle":
                    incident = new NewVehicleIncident();
                    break;
                case "CashSink":
                    incident = new CashChangeIncident(Incident.EIncidentType.CashSink);
                    break;
                case "CashSource":
                    incident = new CashChangeIncident(Incident.EIncidentType.CashSource);
                    break;
            }

            return incident;
        }

    }
}
