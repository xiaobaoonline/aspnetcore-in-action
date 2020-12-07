using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientFactory_Sample.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactory_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderServiceClient _orderServiceClient;
        public OrderController(OrderServiceClient orderServiceClient)
        {
            _orderServiceClient = orderServiceClient;
        }

        [HttpGet("Get")]
        public async Task Get()
        {
            await _orderServiceClient.Get();
        }

        [HttpGet("NamedGet")]
        public async Task NamedGet([FromServices]NamedOrderServiceClient client)
        {
            await client.Get();
        }


        [HttpGet("TypedGet")]
        public async Task NamedGet([FromServices]TypedOrderServiceClient client)
        {
            await client.Get();
        }
    }
}