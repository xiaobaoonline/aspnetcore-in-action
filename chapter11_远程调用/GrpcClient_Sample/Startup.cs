using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcClient_Sample.Interceptors;
using GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using static GrpcServices.OrderGrpc;

namespace GrpcClient_Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            {
                options.Address = new Uri("https://localhost:5001");
                options.Interceptors.Add(new PollyInterceptor());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    OrderGrpcClient service = context.RequestServices.GetService<OrderGrpcClient>();

                    try
                    {
                        var r = service.CreateOrder(new CreateOrderCommand { });
                    }
                    catch
                    {
                    }

                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
