using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
namespace Options_Sample.Services
{
    public class MyService2
    {
        IOptionsMonitor<MyOption> _options;
        public MyService2(IOptionsMonitor<MyOption> options)
        {
            _options = options;

            //默认配置
            var option = _options.CurrentValue;
            //读取命名配置
            var namedOption = _options.Get("myOption2");

            var nn = _options.Get(string.Empty);


            _options.OnChange(option =>
            {
                Console.WriteLine("配置变化了");
            });
        }
    }
}
