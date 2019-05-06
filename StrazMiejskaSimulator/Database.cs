using System;
using System.Collections.Generic;

namespace StrazMiejskaSimulator
{
    class Database
    {
        private static Database instance;

        private Database()
        {

        }

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }

        public enum EData
        {
            VehicleNames,
            Descriptions,
            ItemNames,
            CopFirstNames,
            CopLastNames,
            MobNames,
            CopAttacks,
            StrongmanAttacks,
            GrandmaAttacks,
            DrunkardAttacks,
            PriestAttacks,
            TeenagerAttacks,
            Weather,
            LocationNames,
            LocationDescriptions,
            MarketplaceMobs,
            MarketplaceIncidents,
            ParkingLotMobs,
            ParkingLotIncidents,
            ChurchMobs,
            ChurchIncidents,
            GreenSquareMobs,
            GreenSquareIncidents
        }

        PlainFileReader reader = PlainFileReader.Instance;

        static Dictionary<EData, string[,]> mainDatabase = new Dictionary<EData, string[,]>();

        static readonly Dictionary<EData, string> DataSources = new Dictionary<EData, string>()
        {
            { EData.VehicleNames , @".\Configs\VehicleNames.txt" },
            { EData.Descriptions , @".\Configs\Descriptions.txt" },
            { EData.ItemNames , @".\Configs\ItemNames.txt" },
            { EData.CopFirstNames , @".\Configs\CopFirstNames.txt" },
            { EData.CopLastNames , @".\Configs\CopLastNames.txt" },
            { EData.MobNames , @".\Configs\MobNames.txt" },
            { EData.CopAttacks , @".\Configs\AIs\Cop.txt" },
            { EData.StrongmanAttacks , @".\Configs\AIs\Strongman.txt" },
            { EData.GrandmaAttacks , @".\Configs\AIs\Grandma.txt" },
            { EData.DrunkardAttacks , @".\Configs\AIs\Drunkard.txt" },
            { EData.PriestAttacks , @".\Configs\AIs\Priest.txt" },
            { EData.TeenagerAttacks , @".\Configs\AIs\Teenager.txt" },
            { EData.Weather , @".\Configs\Weathers.txt" },
            { EData.LocationNames , @".\Configs\LocationNames.txt" },
            { EData.LocationDescriptions , @".\Configs\LocationDescriptions.txt" },
            { EData.MarketplaceMobs , @".\Configs\Locations\Marketplace\Mobs.txt" },
            { EData.MarketplaceIncidents , @".\Configs\Locations\Marketplace\Incidents.txt" },
            { EData.ParkingLotMobs , @".\Configs\Locations\ParkingLot\Mobs.txt" },
            { EData.ParkingLotIncidents , @".\Configs\Locations\ParkingLot\Incidents.txt" },
            { EData.ChurchMobs , @".\Configs\Locations\Church\Mobs.txt" },
            { EData.ChurchIncidents , @".\Configs\Locations\Church\Incidents.txt" },
            { EData.GreenSquareMobs , @".\Configs\Locations\GreenSquare\Mobs.txt" },
            { EData.GreenSquareIncidents , @".\Configs\Locations\GreenSquare\Incidents.txt" }
        };

        static readonly Dictionary<Location.ELocations, EData> MobDefinitions = new Dictionary<Location.ELocations, EData>()
        {
            { Location.ELocations.Marketplace , EData.MarketplaceMobs },
            { Location.ELocations.ParkingLot , EData.ParkingLotMobs },
            { Location.ELocations.Church , EData.ChurchMobs },
            { Location.ELocations.GreenSquare , EData.GreenSquareMobs }

        };

        static readonly Dictionary<Location.ELocations, EData> IncidentDefinitions = new Dictionary<Location.ELocations, EData>()
        {
            { Location.ELocations.Marketplace , EData.MarketplaceIncidents },
            { Location.ELocations.ParkingLot , EData.ParkingLotIncidents },
            { Location.ELocations.Church , EData.ChurchIncidents },
            { Location.ELocations.GreenSquare , EData.GreenSquareIncidents }

        };

        public bool UpdateDatabase()
        {
            foreach (KeyValuePair<EData, string> pair in DataSources)
            {
                mainDatabase.Add(pair.Key, reader.ReadFileToStringArray(pair.Value));
            }
            return true;
        }

        public string[,] GetDataFor(EData type)
        {
            foreach (KeyValuePair<EData, string[,]> pair in mainDatabase)
            {
                if (pair.Key == type)
                {
                    return pair.Value;
                }
            }

            throw new ArgumentException("No data for type: " + type);
        }

        public EData GetMobsEDataForLocation(Location.ELocations locationType)
        {
            foreach(KeyValuePair<Location.ELocations, EData> pair in MobDefinitions)
            {
                if(pair.Key == locationType)
                {
                    return pair.Value;
                }
            }

            throw new ArgumentException("No EData was found for this type of Location: " + Enum.GetName(typeof(Location.ELocations), locationType));
        }

        public EData GetIncidentsEDataForLocation(Location.ELocations locationType)
        {
            foreach (KeyValuePair<Location.ELocations, EData> pair in IncidentDefinitions)
            {
                if (pair.Key == locationType)
                {
                    return pair.Value;
                }
            }

            throw new ArgumentException("No EData was found for this type of Location: " + Enum.GetName(typeof(Location.ELocations), locationType));
        }



    }
}
