using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNetCoreMVC.Filters;

namespace MyNetCoreMVC.Controllers
{
    [AddHeader("Author", "Rick Anderson")]
    public class SampleController : Controller
    {
        public IActionResult Index()
        {
            return Content("Examine the headers using the F12 developer tools.");
        }

        [ServiceFilter(typeof(MyActionFilterAttribute))]
        public IActionResult Index2()
        {
            return Content("Header values by configuration.");
        }

    }
}
