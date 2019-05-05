using System;
using System.Collections.Generic;
using System.IO;

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

    }
}
