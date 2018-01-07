using System.IO;

namespace Task_EY
{
    public class FileCreator
    {
        public void Create(string path = "example.txt", int lines = 100000) // create file with 
        {                                                                   // generated lines
            using (var output = File.Create(path))
            {
                using (StreamWriter writer = new StreamWriter(output))
                {
                    for (int i = 0; i <lines; i++)
                    {
                        writer.WriteLine(Initializer.Initialize().GenerateLine());
                    }
                }
            }
        }

        public void CreateMany(int numberToCreate = 100, string fileName = "file") // create specific amount
        {                                                                         // of files
            for (int i = 1; i <= numberToCreate; i++)
            {
                string filePath = fileName + i.ToString() + ".txt";
                Create(filePath);
            }
        }
    }
}
