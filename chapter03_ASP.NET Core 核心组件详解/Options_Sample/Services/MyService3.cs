using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
namespace Options_Sample.Services
{
    public class MyService3
    {
        IOptionsSnapshot<MyOption> _options;
        public MyService3(IOptionsSnapshot<MyOption> options)
        {
            _options = options;
        }
    }
}
