using System;
using System.Collections.Generic;

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

                foreach (KeyValuePair<Incident.EIncidentType, int> pair in location.PossibleIncidents)
                {
                    if (choice <= choiceStack + pair.Value)
                    {
                        Incident incident =  EIncidentToIncidentConverter(pair.Key);
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

        public Incident EIncidentToIncidentConverter(Incident.EIncidentType incidentType)
        {
            Incident incident = null;

            switch (incidentType)
            {
                case Incident.EIncidentType.NewItem:
                    incident = new NewItemIncident();
                    break;
                case Incident.EIncidentType.Fight:
                    incident = new FightIncident();
                    break;
                case Incident.EIncidentType.NewVehicle:
                    incident = new NewVehicleIncident();
                    break;
                case Incident.EIncidentType.CashSink:
                    incident = new CashChangeIncident(Incident.EIncidentType.CashSink);
                    break;
                case Incident.EIncidentType.CashSource:
                    incident = new CashChangeIncident(Incident.EIncidentType.CashSource);
                    break;
            }

            return incident;
        }

    }
}
