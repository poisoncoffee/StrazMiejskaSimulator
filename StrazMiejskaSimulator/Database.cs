using System;
using System.Collections.Generic;
using System.Text;

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
            TeenagerAttacks
        }

        PlainFileReader reader = PlainFileReader.Instance;

        static Dictionary<EData, string[,]> mainDatabase = new Dictionary<EData, string[,]>();

        static readonly Dictionary<EData, string> pathDictionary = new Dictionary<EData, string>()
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
        };


        public bool UpdateDatabase()
        {
            foreach (KeyValuePair<EData, string> pair in pathDictionary)
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



    }
}
