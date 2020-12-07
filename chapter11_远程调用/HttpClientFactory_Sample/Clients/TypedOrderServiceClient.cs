using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
namespace HttpClientFactory_Sample.Clients
{
    public class TypedOrderServiceClient
    {
        HttpClient _client;
        public TypedOrderServiceClient(HttpClient client)
        {
            _client = client;
        }


        public async Task Get()
        {
            

            await _client.GetAsync("/api/order"); //这里使用相对路径来访问
        }
    }
}
