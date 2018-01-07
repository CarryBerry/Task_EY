using System.IO;

namespace Task_EY
{
    public static class Merger // merge files
    {
        public static int MergeFiles(string path1, string path2, string charsToRemoveLine = "")
        {
            string line = null;
            int counter = 0;

            using (var output = File.Create("merged.txt")) 
            {
                using (StreamWriter writer = new StreamWriter(output))
                {
                    foreach (var file in new[] { path1, path2 })
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            //using (StreamWriter writer = new StreamWriter(output))
                            {
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (line.Contains(charsToRemoveLine))
                                    {
                                        line.Remove(0);
                                        counter++;
                                    }
                                    if (line != null)
                                    {
                                        writer.WriteLine(line);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return counter; // returns amount of deleted lines 
        }
    }
}
