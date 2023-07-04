
using System;
using System.Collections.Generic;

namespace StardropTools.Log
{
    [Serializable]
    public struct ObjectLogger<T>
    {
        T objectToMonitor;
        List<LogEntry> logs;

        public ObjectLogger(T objectToMonitor)
        {
            this.objectToMonitor = objectToMonitor;
            logs = new List<LogEntry>();
        }

        public void AddLogEntry(T modifiedObject, string entry)
        {
            if (objectToMonitor.GetHashCode() != modifiedObject.GetHashCode())
                return;

            objectToMonitor = modifiedObject;
            logs.Add(new LogEntry(DateTime.Now, entry));
        }

        public void AddLogEntry(string entry)
            => logs.Add(new LogEntry(DateTime.Now, entry));

        public string[] GetLogs()
        {
            string[] stringLogs = new string[logs.Count];

            for (int i = 0; i < stringLogs.Length; i++)
                stringLogs[i] = logs[i].Log;

            return stringLogs;
        }
    }
}