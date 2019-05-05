using System;

namespace StrazMiejskaSimulator
{
    class CashChangeIncident : Incident
    {


        Random rnd = new Random();

        public CashChangeIncident(EIncidentType givenType)
        {
            type = givenType;
        }

        public override bool PerformIncident(Location location)
        {
            int value = CalculateValueAccordingToType();
            Console.WriteLine(GetDescriptionForIncident(this, String.Empty), value);
            BudgetManager.AlterBudget(value);
            return true;
        }

        int CalculateValueAccordingToType()
        {
            int value = 0;

            if(type == EIncidentType.CashSink)
            {
                value = rnd.Next(3, 13) * 100 * (-1);
            }
            else if(type == EIncidentType.CashSource)
            {
                value = rnd.Next(3, 16) * 100;
            }
            else
            {
                throw new ArgumentException("This Incident is of invalid type: " + nameof(type));
            }

            return value;
        }

    }
}
