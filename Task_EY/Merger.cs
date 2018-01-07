using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_EY
{
    public class Merger
    {
        private string line = null;

        public int MergeFiles(string path1, string path2, string charsToRemoveLine = "")
        {
            //string line_to_delete = "the line i want to delete";
            int counter = 0;

            using (var output = File.Create("output.txt")) //!!! Create new pathes !!!
            {
                foreach (var file in new[] { path1, path2 }) //!!! Create new pathes !!!
                {
                    //Deleter(output, file);
                    using (StreamReader reader = new StreamReader(file))
                    {
                        using (StreamWriter writer = new StreamWriter(output))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains(charsToRemoveLine)/*String.Compare(line, lineToDelete) == 0*/)
                                {
                                    line.Remove(0);
                                    counter++;
                                }

                                writer.WriteLine(line);
                            }
                        }
                    }

                    //using (var input = File.OpenRead(file))
                    //{
                    //    input.CopyTo(output, 2048);
                    //}
                }
            }

            return counter;
        }

        //private void Deleter(FileStream output, string file)
        //{
        //    //using (var output = File.Create("output.txt"))
        //    //{
        //        //foreach (var file in new[] { "file1.txt", "file2.txt" })
        //        //{
        //            using (StreamReader reader = new StreamReader(file))
        //            {
        //                //using (StreamWriter writer = new StreamWriter(output))
        //                //{
        //                    while ((line = reader.ReadLine()) != null)
        //                    {
        //                        if (String.Compare(line, line_to_delete) == 0)
        //                            line.Remove(0);

        //                    //    writer.WriteLine(line);
        //                    //}
        //                //}
        //        //}
        //        }
        //    }
        //}

       

//using (StreamReader reader = new StreamReader("C:\\input")) {
//    using (StreamWriter writer = new StreamWriter("C:\\output")) {
//        while ((line = reader.ReadLine()) != null) {
//            if (String.Compare(line, line_to_delete) == 0)
//                continue;

//            writer.WriteLine(line);
//        }
//    }
//}
    }
}
