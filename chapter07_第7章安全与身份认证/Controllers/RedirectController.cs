using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chapter07_Samples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedirectController : ControllerBase
    {
        [HttpGet]
        public IActionResult RunDemo(string returnUrl)
        {
            return LocalRedirect(returnUrl);
        }
    }
}
