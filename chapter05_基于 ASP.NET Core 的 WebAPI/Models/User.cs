using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
