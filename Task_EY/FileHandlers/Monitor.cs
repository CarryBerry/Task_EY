﻿using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Task_EY.EntityModel;

namespace Task_EY
{
    public class Monitor
    {
        private ConsoleRecorder _consoleRecorder = new ConsoleRecorder();
        private readonly int _lineCount; //change if amount of lines should be differ than 100.000
        private readonly FileSystemWatcher _watcher;
        private readonly DbHandler _handler;
        private Task _task;
        private readonly object _obj = new object();
        private bool _actionStopper = true; // stops monitoring when triggered

        public Monitor(int lineCount = 100000) // monitors certain folder and adds occured files to db
        {
            _lineCount = lineCount;
            _handler = new DbHandler();
            _watcher = new FileSystemWatcher();
            _watcher.Path = ConfigurationManager.AppSettings["DataPath"]; // Pass can be changed
            _watcher.Filter = "*.txt";                                   // in app config
            _watcher.NotifyFilter = NotifyFilters.FileName;

            _watcher.Created += OnChanged;
            _watcher.Deleted += Watcher_Deleted;
            _watcher.Created += Watcher_Created;
            _watcher.Changed += Watcher_Changed;
            _watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (_actionStopper)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _actionStopper = false;
            _watcher.Created -= OnChanged;
            _watcher.Deleted -= Watcher_Deleted;
            _watcher.Created -= Watcher_Created;
            _watcher.Changed -= Watcher_Changed;
            _watcher.Renamed -= Watcher_Renamed;
        }

        // triggers when file renaming
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "renamed to " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        // triggers when file changing
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "updated";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        // triggers when file creating
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        // triggers when file deleting
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "deleted";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj) // used to maintenance one task per time
            {
                string path = ConfigurationManager.AppSettings["LoggerPath"]; // Pass can be changed
                using (StreamWriter writer = new StreamWriter(path, true))    // in App.config
                {
                    writer.WriteLine($"{DateTime.Now:dd/MM/yyyy hh:mm:ss} file {filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }

        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            _task = new Task
                (() => CallHandler(sender, e));
            _task.Start();
        }

        public void CallHandler(object sender, FileSystemEventArgs e)
        {
            var path = e.FullPath;
            _handler.DbAdd(path, _lineCount);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _watcher.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void MoveToMonitorFolder(int amount, string fileName = "file")
        {
            string monitorFolder = ConfigurationManager.AppSettings["DataPath"];

            for (int i = 1; i <= amount; i++)
            {
                lock (_obj)
                {
                    string filePath = fileName + i.ToString() + ".txt";
                    try
                    {
                        if (!File.Exists(filePath))
                        {
                            throw new FileNotFoundException();
                            // This statement ensures that the file is created
                        }

                        // Ensure that the target does not exist.
                        if (File.Exists(monitorFolder + "\\" + fileName))
                            File.Delete(monitorFolder + "\\" + fileName);

                        // Move the file.
                        File.Move(filePath, monitorFolder);
                        //Console.WriteLine("{0} was moved to {1}.", filePath, monitorFolder);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine("File not exist: {0}", e.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }

                }
            }
        }
    }
}
