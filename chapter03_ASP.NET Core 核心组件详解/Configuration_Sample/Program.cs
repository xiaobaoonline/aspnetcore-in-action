using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Configuration_Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureHostConfiguration(builder =>
        {
            builder.AddJsonFile("myhostconfig.json", optional: true); //注入json配置文件
        })
        .ConfigureAppConfiguration(builder =>
        {
            builder.AddJsonFile("myappsettings.json", optional: true);//注入json配置文件

            //添加Json配置
            builder.AddJsonFile(path: "cfg.json", optional: true, reloadOnChange: true);
            //builder.AddJsonStream(stream: jsonStream); //从文件流读配置

            //添加Xml配置
            builder.AddXmlFile(path: "cfg.xml", optional: true, reloadOnChange: true);
            //builder.AddXmlStream(stream: xmlStream); //从文件流读配置

            //添加Ini配置
            builder.AddIniFile(path: "cfg.ini", optional: true, reloadOnChange: true);
            //builder.AddIniStream(stream: iniStream); //从文件流读配置


            //加载 key per file
            var filedir = Path.Combine(Directory.GetCurrentDirectory(), "cfgdir");
            builder.AddKeyPerFile(directoryPath: filedir, optional: false);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }
}
