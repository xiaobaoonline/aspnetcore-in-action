using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyNetCoreMVC.Controllers
{
    public class DocController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
