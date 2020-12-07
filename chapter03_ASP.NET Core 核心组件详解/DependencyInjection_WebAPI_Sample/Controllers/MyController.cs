using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
namespace DependencyInjection_WebAPI_Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private readonly IMyService _myService;

        public MyController(IMyService myService)
        {
            _myService = myService;
        }

        [HttpGet]
        public string Get([FromServices]IMyService myService2)
        {
            var myService3 = this.HttpContext.RequestServices.GetService<IMyService>();
            var data1 = _myService.GetData();
            var data2 = myService2.GetData();
            var data3 = myService3.GetData();
            return "Ok";
        }
    }


    public interface IMyService
    {
        object GetData();
    }
}
