using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_EY
{
    public class FileCreator
    {

        public void Create(string path = @"D:\Example.txt")
        {
            //string path = @"E:\AppServ\Example.txt";
            //string path = @"D:\Example.txt";

            //File.AppendAllLines(path, new[] { "The very first line!" });


            using (var output = File.Create(/*"output.txt"*/path))
            {
                using (StreamWriter writer = new StreamWriter(output))
                {
                    for (int i = 0; i <10; i++)
                    {
                        writer.WriteLine(Initializer.Initialize().GenerateLine());
                    }
                    
                }
            }
            
            //    TextWriter tw = new StreamWriter(path);
            //    tw.WriteLine("The very first line!");
            //    tw.Close();
            
            //else if (File.Exists(path))
            //{
            //    TextWriter tw = new StreamWriter(path);
            //    tw.WriteLine("The next line!");
            //    tw.Close();
            //}
        }

        public void CreateMany(int numberToCreate = 100, string path = @"D:\file")
        {
            for (int i = 0; i < numberToCreate; i++)
            {
                string filePath = path + i.ToString() + ".txt";
                Create(filePath);
            }
        }
    }
}
