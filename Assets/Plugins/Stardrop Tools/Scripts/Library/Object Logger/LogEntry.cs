
using System;

namespace StardropTools.Log
{
    [Serializable]
    public struct LogEntry
    {
        [UnityEngine.SerializeField] DateTime dateTime;
        [UnityEngine.SerializeField] string entry;

        public DateTime DateTime => dateTime;
        public string Enty => entry;
        public string Log => dateTime.ToString() + ": " + entry;


        public LogEntry(DateTime dateTime, string entry)
        {
            this.dateTime = dateTime;
            this.entry = entry;
        }
    }
}