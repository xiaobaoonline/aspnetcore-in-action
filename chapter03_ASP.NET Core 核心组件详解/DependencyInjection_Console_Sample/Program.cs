using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjection_Console_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMyDisposableService, MyDisposableService>();

            var serviceProvider = services.BuildServiceProvider();

            var myDisposableService = serviceProvider.GetService<IMyDisposableService>();

            



            var scope = serviceProvider.CreateScope();



            

            var s = scope.ServiceProvider.GetService<IMyDisposableService>();


            var scopex = scope.ServiceProvider.CreateScope();



            var sssss = scopex == scope;


            var s2 = scopex.ServiceProvider.GetService<IMyDisposableService>();


            serviceProvider.Dispose();



            scopex.Dispose();
            scope.Dispose();
            Console.WriteLine("Hello World!");


            #region 



            #endregion
        }
    }

    interface IMyService
    {

    }

    class MyService : IMyService
    {

    }


    interface IMyDisposableService : IDisposable
    {

    }

    class MyDisposableService : IMyDisposableService
    {
        public void Dispose()
        {
            //释放资源
            Console.WriteLine("资源释放了" + this.GetHashCode());
        }
    }
}
