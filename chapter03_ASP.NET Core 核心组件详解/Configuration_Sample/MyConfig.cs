using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configuration_Sample
{
    public class MyConfig
    {
        public string Name { get; set; }

        public int MinAge { get; set; }

        public string Title { get; private set; }
    }
}
