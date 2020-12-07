using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging_Sample
{
    public class MyService
    {
        ILoggerFactory _loggerFactory;

        ILogger _logger;
        public MyService(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;

            _logger = loggerFactory.CreateLogger<MyService>();
        }


        public void DoWork()
        {
            _logger.LogWarning("some warning");
        }
    }
}
