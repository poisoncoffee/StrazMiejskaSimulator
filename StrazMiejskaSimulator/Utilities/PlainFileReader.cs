using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using StrazMiejskaSimulator.Utilities;

using System.Text;

namespace StrazMiejskaSimulator
{
    class PlainFileReader
    {
        private static PlainFileReader instance;

        private PlainFileReader()
        {

        }

        public static PlainFileReader Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PlainFileReader();
                }
                return instance;
            }
        }

        public string ReadString(string path)
        {
            string readString = null;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    readString = sr.ReadLine();

                    if (readString == null)
                    {
                        throw new Exception();
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:" + path);
                Console.WriteLine(e.Message);
            }

            return readString;
        }


        public List<string> ReadFileToList(string path)
        {
            List<string> mainList = new List<string>();
            string readString;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((readString = sr.ReadLine()) != null)
                    {
                      mainList.Add(readString);
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:" + path);
                Console.WriteLine(e.Message);
            }

            return mainList;
        }

        public Dictionary<string, int> ReadFileToDictionary (string path)
        {
            Dictionary<string, int> mainDictionary = new Dictionary<string, int>();
            string readString;
            int seperatedInt;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((readString = sr.ReadLine()) != null)
                    {
                        string[] seperatedData = readString.Split(';');
                        seperatedInt = int.Parse(seperatedData[1], CultureInfo.InvariantCulture);
                        mainDictionary.Add(seperatedData[0], seperatedInt);                       
   
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:" + path);
                Console.WriteLine(e.Message);
            }

            return mainDictionary;
        }

        public Dictionary<string, string>ReadFileToStringStringDictionary(string path)
        {
            Dictionary<string, string> mainDictionary = new Dictionary<string, string>();
            string readString;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((readString = sr.ReadLine()) != null)
                    {
                        string[] seperatedData = readString.Split(';');
                        mainDictionary.Add(seperatedData[0], seperatedData[1]);
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:" + path);
                Console.WriteLine(e.Message);
            }

           return mainDictionary;
        }

        public Dictionary<int, string> ReadFileToIntStringDictionary(string path)
        {
            Dictionary<int, string> mainDictionary = new Dictionary<int, string>();
            string readString;
            int seperatedInt;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((readString = sr.ReadLine()) != null)
                    {
                        string[] seperatedData = readString.Split(';');
                        seperatedInt = int.Parse(seperatedData[0], CultureInfo.InvariantCulture);
                        mainDictionary.Add(seperatedInt, seperatedData[1]);

                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:" + path);
                Console.WriteLine(e.Message);
            }

            return mainDictionary;
        }

        public Dictionary<string, Dictionary<string, string>> ReadFileToNestedDictionary (string path)
        {
            Dictionary<string, Dictionary<string, string>> mainDictionary = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> childDictionary = new Dictionary<string, string>();
            string readString;
            string mainKey;

            try
            {
               using (StreamReader sr = new StreamReader(path))
                {

                    while ((readString = sr.ReadLine()) != null)
                    {
                        childDictionary.Clear();
                        string[] seperatedData = readString.Split(';');
                        for (int i = 1; i < seperatedData.Length; i++)
                        {
                            string[] seperatedSubData = seperatedData[i].Split(':');
                            childDictionary.Add(seperatedSubData[0], seperatedSubData[1]);
                        }
                        mainKey = seperatedData[0];
                        mainDictionary.Add(mainKey, childDictionary);
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            return mainDictionary;
        }

        public Dictionary<string, List<string>> ReadFileToListDictionary(string path)
        {
            Dictionary<string, List<string>> mainDictionary = new Dictionary<string, List<string>>();

            string readString;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {

                    while ((readString = sr.ReadLine()) != null)
                    {
                        List<string> childList = new List<string>();
                        string[] seperatedData = readString.Split(';');
                        //checks if this key already exists in mainDictionary

                        bool keyExists = false;
                        foreach(KeyValuePair<string, List<string>> pair in mainDictionary)
                        {
                            if (seperatedData[0] == pair.Key)
                            {
                              pair.Value.Add(seperatedData[1]);
                              keyExists = true;
                            }
                        }
                            if(!keyExists)
                            {
                            childList.Add(seperatedData[1]);
                            mainDictionary.Add(seperatedData[0], childList);
                            }                     
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            return mainDictionary;
        }

        //WIP refactoring of whole program to consistently use this one method
        public string[,] ReadFileToStringArray(string path)
        {
            List<string> readLine = new List<string>();
            string readString;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while ((readString = sr.ReadLine()) != null)
                    {
                        readLine.Add(readString);
                    }

                    sr.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:" + path);
                Console.WriteLine(e.Message);
            }

            string[] columnHeaders = readLine[0].Split(';');
            int columns = columnHeaders.GetLength(0);
            int rows = readLine.Count - 1;

            string[,] dataArray = new string[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                string[] seperatedData = readLine[i + 1].Split(';');
                for (int j = 0; j < seperatedData.Length; j++)
                {
                    dataArray[i, j] = seperatedData[j];
                }
            }

            return dataArray;
        }

        public string GetFilePathFor (string data)
        {
            Dictionary<string, string> FilePaths = new Dictionary<string, string>() {
                { "MobNames" , @"..\Configs\MobNames.txt" },
                { "VehicleNames" , @"..\Configs\VehicleNames.txt" },
                { "Cop" , @"..\Configs\AIs\Cop.txt" },
                { "Strongman" , @"..\Configs\AIs\Strongman.txt" },
                { "Grandma" , @"..\Configs\AIs\Grandma.txt" },
                { "Drunkard" , @"..\Configs\AIs\Drunkard.txt" },
                { "Priest" , @"..\Configs\AIs\Priest.txt" },
                { "Teenager" , @"..\Configs\AIs\Teenager.txt" },
                { "Weather" , @"..\Configs\Weathers.txt" }
            };

            foreach (KeyValuePair<string, string> pair in FilePaths)
            {
                if (data == pair.Key)
                {
                    return pair.Value;
                }
            }

            throw new ArgumentOutOfRangeException("Data type in path request is not valid: " +  data);

        }

        public string GetFilePathFor(string data, Location.ELocations location)
        {
            string pathTemplate = @"..\Configs\Locations\" + Convert.ToString(location) + @"\";

            Dictionary<string, string> FilePaths = new Dictionary<string, string>() {
                { "Definitions" , pathTemplate + "LocationDef.txt" },
                { "Descriptions" , pathTemplate + "LocationDescs.txt" },
                { "Incidents" , pathTemplate + "LocationEvents.txt" },
                { "PossibleMobs" , pathTemplate + "LocationMobs.txt" }
            };

            foreach (KeyValuePair<string, string> pair in FilePaths)
            {
                if (data == pair.Key)
                {
                    return pair.Value;
                }
            }

            throw new ArgumentOutOfRangeException("Data type in path request is not valid: " + data);
        }

    }
}
