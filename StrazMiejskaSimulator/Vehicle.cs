using System;

namespace StrazMiejskaSimulator
{
    class Vehicle
    {

        public enum EVehicle
        {
            OnFoot,
            Horse,
            Car
        }

        public EVehicle type { get; private set; }
        public string name { get; private set; }
        public int extraPatrol { get; private set; }

        public Vehicle(EVehicle givenType)
        {
            type = givenType;
            name = GetVehicleName(type);
            extraPatrol = CalculateExtraPatrolDays(givenType);
        }

        string GetVehicleName(EVehicle type)
        {
            Database database = Database.Instance;
            string[,] VehicleNames = database.GetDataFor(Database.EData.VehicleNames);

            for(int i = 0; i < VehicleNames.GetLength(0); i++)
            {
                string typeName = type.ToString();
                if (VehicleNames[i,0] == typeName)
                {
                    return VehicleNames[i,1];
                }
            }

            throw new ArgumentOutOfRangeException("No actual name exists for this type of vehicle: " + type);
        }

        int CalculateExtraPatrolDays(EVehicle type)
        {
            switch(type)
            {
                case EVehicle.Horse:
                    return 1;
                case EVehicle.Car:
                    return 2;
                default:
                    return 0;
            }
        }
    }


}
