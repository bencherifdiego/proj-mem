using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    class LoadSave
    {
        public string[] GetSavesFromDir()
        {
            //Get all save files in the saves folder and returns an array
            string[] fileInSave = Directory.GetFiles(@"..\..\.\saves\", "*.csv");
            return fileInSave;
        }

        public string[,] GetSavefileData(string selectedSave)
        {
            //Get savefile
            string filePath = (@"..\..\.\saves\" + selectedSave);

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

            return resultVals;
        }
    }
}
