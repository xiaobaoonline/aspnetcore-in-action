using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactory_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public async Task<string> Get()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://yourdomain.com");

            //发起GET请求
            var html = await client.GetStringAsync("/index.html");

            //发起POST请求
            var content = new StringContent("the content");
            var message = await client.PostAsync("/api/update", content);

            //上传文件
            var fileContent = new MultipartFormDataContent();
            using (var stream = System.IO.File.Open("myfile.txt", System.IO.FileMode.Open))
            {
                fileContent.Add(new StreamContent(stream), "myfile", "myfile");
                var uploadMessage = await client.PostAsync("/upload", fileContent);
            }

            return await Task.FromResult("");
        }
    }
}
