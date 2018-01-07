using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_EY
{
    public class ConsoleRecorder
    {
        private object _obj = new object();
        private Monitor monitor;

        public void ConsoleRecordEntry(string fileEvent, string filePath)
        {
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
