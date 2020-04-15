using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.Logging
{
    public class FileLogProvider : ILoggerProvider
    {
        private string FilePath;
        public FileLogProvider(string filePath)
        {
            FilePath = filePath;
        }
        public ILogger CreateLogger(string categotry)
        {
            return new FileLogger(FilePath);
        }
        public void Dispose() { }
    }
}
