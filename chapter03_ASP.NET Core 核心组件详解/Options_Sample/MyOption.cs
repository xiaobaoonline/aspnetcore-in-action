using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Options_Sample
{
    public class MyOption
    {
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "MinAge不能小于0")]
        public int MinAge { get; set; }

        public string Title { get; set; }
    }
}
