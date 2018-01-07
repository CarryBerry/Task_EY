using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_EY.EntityModel
{
    public class ProgressBar
    {
        private int progress = 0;

        public void UpdateMessage(int iterator, long lineCount)
        {
            //progress = Convert.ToInt32(iterator * 100 / lineCount);

            //if (progress < lineCount)
            //{
                Console.WriteLine("Progress: {0}/{1}", progress.ToString(), lineCount.ToString());
            //}
            //else
            //{
            //}
        }

        public void CompleteMessage()
        {
            Console.WriteLine("Operation has been successfully completed.", progress.ToString());
        }
    }
}
