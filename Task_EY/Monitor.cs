using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_EY.EntityModel;

namespace Task_EY
{
    public class Monitor
    {
        private ConsoleRecorder _consoleRecorder = new ConsoleRecorder();
        private readonly int _lineCount;
        private readonly FileSystemWatcher _watcher;
        private readonly DbHandler _handler;
        private Task _task;
        private readonly object _obj = new object();
        private bool _actionStopper = true;

        public Monitor(int lineCount = 100000)
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

        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "renamed to " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        // изменение файлов
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "updated";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        // создание файлов
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "created";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "deleted";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
            _consoleRecorder.ConsoleRecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj)
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
            if (!this.disposed)
            {
                if (disposing)
                {
                    _watcher.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
