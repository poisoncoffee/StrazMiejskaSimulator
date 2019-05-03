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
            Descriptions
        }

        PlainFileReader reader = PlainFileReader.Instance;

        static Dictionary<EData, string[,]> mainDatabase = new Dictionary<EData, string[,]>();

        static readonly Dictionary<EData, string> pathDictionary = new Dictionary<EData, string>()
        {
            { EData.VehicleNames , @".\Configs\VehicleNames.txt" },
            { EData.Descriptions , @".\Configs\Descriptions.txt" }
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
            foreach(KeyValuePair<EData, string[,]> pair in mainDatabase)
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
