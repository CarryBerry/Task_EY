using System;
using System.IO;
using System.Linq;

namespace Task_EY.EntityModel
{
    public class DbHandler 
    {
        private int i = 0;
        ProgressBar progress = new ProgressBar();
        private object _locker = new object();

        public void DbAdd(string path, int lineCount = 100000)
        {
            lock (_locker)
            {
                string[] records;

                using (DataContext context = new DataContext())
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        lineCount = File.ReadLines(path).Count(); //Needs if we don't know exactly 
                                                                  //how many rows this file contains
                                                                  //else comment this field
                        while (!sr.EndOfStream)
                        {
                            records = sr.ReadLine().Split(' ');
                           
                            i++;

                            Content content = new Content() // DateLine, LatinLine, CyrillicLine, IntLine, DoubleLine
                            {
                                ContentId = i,
                                DateLine = Convert.ToDateTime(records[0]),
                                LatinLine = records[1],
                                CyrillicLine = records[2],
                                IntLine = Convert.ToInt32(records[3]),
                                DoubleLine = Convert.ToDouble(records[4])
                            };

                            context.Contents.Add(content);
                            context.SaveChanges();

                            progress.UpdateMessage(i, lineCount);
                        }
                    }
                }
                progress.CompleteMessage();
            }
        }
    }
}
