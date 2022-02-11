using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Sokka06.Pipeliner
{
    public class Logger
    {
        public enum LogType
        {
            Normal,
            Warning,
            Error
        }
        
        public struct LogEntry
        {
            public DateTime DateTime;
            public LogType Type;
            public string Text;

            public LogEntry(DateTime dateTime, LogType type, string text)
            {
                DateTime = dateTime;
                Type = type;
                Text = text;
            }
        }

        public bool Enabled = true;
        public StringBuilder Logs { get; private set; }
        public event Action OnLogChanged;

        public Logger(bool enabled = true)
        {
            Enabled = enabled;
            Logs = new StringBuilder();
        }

        public virtual void Log(string text, LogType type = LogType.Normal)
        {
            if (!Enabled)
                return;
            
            var entry = new LogEntry
            {
                DateTime = DateTime.Now,
                Text = text,
                Type = type
            };
            
            AppendEntry(entry);
        }

        protected virtual void AppendEntry(LogEntry entry)
        {
            //if (Logs.)
            Logs.AppendLine($"{FormatDateTime(entry.DateTime)}: {FormatBody(entry.Text)}");
            OnLogChanged?.Invoke();
        }

        protected virtual string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToLongTimeString();
        }
        
        protected virtual string FormatBody(string text)
        {
            return text;
        }

        public virtual void Clear()
        {
            Logs.Clear();
            OnLogChanged?.Invoke();
        }

        public override string ToString()
        {
            return Logs.ToString();
        }
    }
}