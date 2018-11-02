using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            

            try
            {
                string[] fileInSave = Directory.GetFiles(@"..\..\.\saves\", "*.csv");
                foreach(string save in fileInSave)
                {
                    Console.WriteLine(Path.GetFileName(save));
                }

                //Get savefile
                string filePath = (@"..\..\.\saves\sample.csv");

                //Parse save into string
                string fileData = File.ReadAllText(filePath);

                fileData = fileData.Replace('\n', '\r');

                //Split lines into string
                string[] lines = fileData.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

                //Get total rows and columns
                int totalRows = lines.Length;
                int totalCols = lines[0].Split(';').Length;

                //Make new 2d array
                string[,] resultVals = new string[totalRows, totalCols];
                
                //Place data in array
                for (int row = 0; row < totalRows; row++)
                {
                    string[] line_r = lines[row].Split(';');

                    for (int col = 0; col < totalCols; col++)
                    {
                        resultVals[row, col] = line_r[col];
                    }
                }


                //Write array in console for checking
                for (int i = 0; i < totalRows; i++)
                {
                    for (int j = 0; j < totalCols; j++)
                    {
                        Console.Write(string.Format("{0} ", resultVals[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            Console.Read();
        }
    }
}
