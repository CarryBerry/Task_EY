using System;

namespace Task_EY.EntityModel
{
    public class ProgressBar //indicates adding to DB process
    {
        private int progress = 0;

        public void UpdateMessage(int iterator, long lineCount)
        {
            progress++;

            Console.WriteLine("Progress: {0}/{1}", progress.ToString(), lineCount.ToString());
        }

        public void CompleteMessage()
        {
            Console.WriteLine("Operation has been successfully completed.");
        }
    }
}
