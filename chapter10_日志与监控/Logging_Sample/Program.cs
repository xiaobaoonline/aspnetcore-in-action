using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Logging_Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.AddFilter((category, level) =>
                    {
                        if (category.Contains("abc"))
                        {
                            return level > LogLevel.Information;
                        }
                        return false;
                    });


                    builder.AddFilter<DebugLoggerProvider>((category, level) =>
                    {
                        if (category.Contains("efg"))
                        {
                            return level > LogLevel.Trace;
                        }
                        return level> LogLevel.Error;
                    });

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
