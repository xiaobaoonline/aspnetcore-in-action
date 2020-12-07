using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
namespace Options_Sample.Services
{
    public class MyService
    {
        IOptions<MyOption> _options;
        public MyService(IOptions<MyOption> options)
        {
            _options = options;
        }
    }
}
