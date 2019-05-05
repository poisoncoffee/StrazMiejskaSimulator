using System;

namespace StrazMiejskaSimulator
{
    class NewVehicleIncident : Incident
    {
        Vehicle vehicle;
        Random rnd = new Random();

        public NewVehicleIncident()
        {
            type = EIncidentType.NewVehicle;
        }

        public override bool PerformIncident(Location location)
        {
            vehicle = GetRandomVehicle();
            Console.WriteLine(GetDescriptionForIncident(this, Enum.GetName(typeof(Vehicle.EVehicle), vehicle.type)));
            VehicleManager.UpgradeCurrentVehicle(vehicle);
            return true;
        }

        Vehicle GetRandomVehicle()
        {
            Database database = Database.Instance;
            string[,] VehicleNames = database.GetDataFor(Database.EData.VehicleNames);
            int randomVehicle = rnd.Next(1, VehicleNames.GetLength(0));

            Vehicle vehicle = new Vehicle(StringToEvehicleConverter(VehicleNames[randomVehicle, 0]));
            return vehicle;
        }

        Vehicle.EVehicle StringToEvehicleConverter(string vehicleString)
        {
            for (int i = 0; i < Enum.GetValues(typeof(Vehicle.EVehicle)).Length; i++)
            {
                if (Enum.GetName(typeof(Vehicle.EVehicle), i) == vehicleString)
                {
                    return (Vehicle.EVehicle)i;
                }                   
            }

            throw new ArgumentOutOfRangeException("Given string: " + vehicleString + " is not a Vehicle type");
        }
    }
}
