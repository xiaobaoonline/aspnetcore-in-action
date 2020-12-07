using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory_Sample.Clients
{
    public class OrderServiceClient
    {
        IHttpClientFactory _httpClientFactory;

        public OrderServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task Get()
        {
            var client = _httpClientFactory.CreateClient();

            //使用client发起HTTP请求
            await client.GetAsync("https://localhost:5001/api/orders");
        }
    }
}
