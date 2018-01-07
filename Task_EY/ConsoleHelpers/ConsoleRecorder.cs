using System;
using System.Threading;

namespace Task_EY
{
    public class ConsoleRecorder
    {
        private object _obj = new object();
        private Monitor monitor;

        public void ConsoleRecordEntry(string fileEvent, string filePath) // writes all changes with files
        {                                                                // to console
            lock (_obj)
            {
                Console.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} file {filePath} was {fileEvent}");
            }
        }
        
        public void OnStart()
        {
            monitor = new Monitor();
            Thread loggerThread = new Thread(monitor.Start);
            loggerThread.Start();
        }

        public void OnStop()
        {
            monitor.Stop();
            Thread.Sleep(1000);
            monitor.Dispose();
        }
    }
}
