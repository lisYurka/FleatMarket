using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.Logging
{
    public class FileLogger : ILogger
    {
        private string FilePath;
        private static object _lock = new object();
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }
        public IDisposable BeginScope<TState>(TState tState)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            //return true;
            return logLevel == LogLevel.Trace;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventid, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //LogLevel - уровень детализации
            //EventId - идентификатор события
            // TState - объект состояния, который хранит сообщения
            // Exception - инфа об исключении
            // formatter - ф-ция форматирования, которая принимает сообщение для логгирования с помощью двух параметров
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(FilePath, "[" + DateTime.Now + "] --> " + formatter(state, exception) + Environment.NewLine);
                }

            }
        }
    }
}
