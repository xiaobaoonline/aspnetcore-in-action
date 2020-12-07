using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration.Memory;
namespace Configuration_Sample1
{
    class Program
    {
        static void Main(string[] args)
        {

            EnvironmentVariables_All_Sample();
            EnvironmentVariables_Prefix_Sample();
            return;


            IConfigurationBuilder builder = new ConfigurationBuilder();
            var configData = new Dictionary<string, string>() {
                                { "abc:aa","v"},

                                { "abc:aa:aaa","v2"}
                            };
            //builder.Add(new MemoryConfigurationSource { InitialData = configData });
            //builder.AddJsonFile("section.json");


            var switchMappings = new Dictionary<string, string>
            {
                { "-k1","key1"},
                { "-k2","key2"},
                { "--k3","key2"},
            };


            builder.AddCommandLine(args, switchMappings);

            builder.AddEnvironmentVariables();



            //builder.AddInMemoryCollection(configData);
            //builder.AddCommandLine(args);
            IConfigurationRoot root = builder.Build();

            var key1 = root["key2"];
            Console.WriteLine(key1);


            #region Section
            //Key: key0  , Path: key0 , Value: value0
            var sectionKey0 = root.GetSection("key0");

            //Key: section0  , Path: section0 , Value: null
            var section0 = root.GetSection("section0");

            //Key: section1  , Path: section1 , Value: null
            var section1 = root.GetSection("section1");





            //Key: section2  , Path: section2 , Value: null
            var section2 = root.GetSection("section2");

            //Key: subsection0  , Path: section2:subsection0 , Value: null
            var subsection0 = root.GetSection("section2:subsection0");
            var value1 = subsection0["key0"]; // value200
            var value2 = section2["subsection0:key0"]; //value200
            var value3 = root["section2:subsection0:key0"]; // value200





            //Key: subsection1  , Path: section2:subsection1 , Value: null
            var subsection1 = root.GetSection("section2:subsection1");

            //Key: key0  , Path: section2:subsection1:key0 , Value: value210
            var subsectionKey0 = root.GetSection("section2:subsection1:key0");

            var sec = root.GetChildren().ToList();

            var sss = sec.Select(p => p.Exists());

            #endregion


            Console.WriteLine("Hello World!");

            Console.WriteLine("Hello 小明!");

            var name = "小张";
            Console.WriteLine($"Hello {name}!");




            //Microsoft.Extensions.Configuration.con

            //IConfigurationSource
            //IConfigurationBuilder
            //IConfigurationProvider
            //IConfigurationSection
            //IConfiguration
            //IConfigurationRoot




            var sections = root.GetChildren().ToList();

            var s = sections.First().GetChildren();
        }






        #region EnvironmentVariables
        static void EnvironmentVariables_All_Sample()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();  //加载所有环节变量
            IConfigurationRoot root = builder.Build();

            Console.WriteLine($"Key1:{root["Key1"]}");
            Console.WriteLine($"Section1:Key1:{root["Section1:Key1"]}");
            Console.WriteLine($"Key1 in Section1:{root.GetSection("Section1")["Key1"]}");
            Console.WriteLine($"MyEnv_Key1:{root["MyEnv_Key1"]}");
            Console.WriteLine($"MyEnv_Section1:Key1:{root["MyEnv_Section1:Key1"]}");
        }

        static void EnvironmentVariables_Prefix_Sample()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables("MyEnv_"); //加载指定前缀的环节变量
            IConfigurationRoot root = builder.Build();
            Console.WriteLine($"Key1:{root["Key1"]}");
            Console.WriteLine($"Section1:Key1:{root["Section1:Key1"]}");
            Console.WriteLine($"Key1 in Section1:{root.GetSection("Section1")["Key1"]}");
            Console.WriteLine($"MyEnv_Key1:{root["MyEnv_Key1"]}");
            Console.WriteLine($"MyEnv_Section1:Key1:{root["MyEnv_Section1:Key1"]}");
        }
        #endregion
    }
}