using System;
using System.IO;
using System.Xml;

namespace csvReaderApp
{
    class csvReader
    {
        static void Main(string[] args)
        {
            string inputFile = @"D:\Files\my-file.csv";

            //Reading CSV File Content - Row Wise
            using (var reader = new StreamReader(inputFile))
            {
                while (!reader.EndOfStream)
                {
                    //Reading entire row without any delimeter
                    /*var line = reader.ReadLine();
                    Console.WriteLine(line);*/

                    //Reading entire Row with delimeter
                    var line = reader.ReadLine();
                    var row = line.Split(',');
                    foreach (var item in row)
                    {
                        Console.Write(item + "\t");
                    }
                    Console.WriteLine();
                }
            }

            //Reading entire CSV content using 'File.ReadAllLines'
            //Here csv content is stored into array memory, where every index of array=entire row in csv
            //For large files, prefer processing linebyLine to avoid memory issues
            string[] csvFileData = File.ReadAllLines(inputFile);            
            
            foreach(var item in csvFileData) {                                
                var csvLine = item.Split(',');
                
                foreach(var lineItem in csvLine)  {
                    Console.Write(lineItem+"\t"); }
                Console.WriteLine(); 
            }


            //Reading entire CSV content using 'File.ReadAllText'
            //here csv content is stored into one string 
            string fileContent = File.ReadAllText(inputFile);
            string[] lines = fileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines) {
                var csvLine = line.Split(',');

                foreach (var lineItem in csvLine)
                {
                    Console.Write(lineItem + "\t");
                }
                Console.WriteLine();
            }

        }
    }
}
