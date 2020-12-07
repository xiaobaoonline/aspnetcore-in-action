using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter07_Samples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataProtectionController : ControllerBase
    {
        [HttpGet]
        public string RunDemo([FromServices] IDataProtectionProvider provider, string input = "要保护的数据")
        {
            string output = "";

            var protector = provider.CreateProtector("用途说明");
            // 加密数据
            string protectedPayload = protector.Protect(input);
            output += $"加密后的数据: {protectedPayload}";

            // 解密数据
            string unprotectedPayload = protector.Unprotect(protectedPayload);
            output += $"解密后的数据: {unprotectedPayload}";
            return output;
        }
    }
}
