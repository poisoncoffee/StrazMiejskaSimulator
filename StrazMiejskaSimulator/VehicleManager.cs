using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator
{
    class VehicleManager
    {

        private static VehicleManager instance;
        public static Vehicle currentVehicle { get; private set; }

        private VehicleManager()
        {
            currentVehicle = new Vehicle(Vehicle.EVehicle.OnFoot);
        }

        public static VehicleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VehicleManager();
                }
                return instance;
            }
        }

        public static void UpgradeCurrentVehicle(Vehicle vehicle)
        {
            if (vehicle.type > currentVehicle.type)
            {
                currentVehicle = vehicle;
                Console.WriteLine("Gratulacje! Waszym nowym środkiem transportu jest teraz: " + currentVehicle.name);
            }
            else
            {
                Console.WriteLine("Niestety, " + vehicle.name + " wcale nie jest lepszy niż wasz " + currentVehicle.name + ". Środek transportu nie zostanie zmieniony.");
            }
        }

    }
}
