﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory_Sample.Clients
{
    public class NamedOrderServiceClient
    {
        IHttpClientFactory _httpClientFactory;

        const string _clientName = "orderService";  //定义客户端名称

        public NamedOrderServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task Get()
        {
            var client = _httpClientFactory.CreateClient(_clientName); //使用客户端名称获取客户端

            //使用client发起HTTP请求,这里使用相对路径来访问
            await client.GetAsync("/api/orders");
        }
    }
}
