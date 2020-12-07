using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyNetCoreMVC.Filters;
using MyNetCoreMVC.Models;

namespace MyNetCoreMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PositionOptions>(
                     Configuration.GetSection("Position"));
            services.AddScoped<MyActionFilterAttribute>();

            services.AddControllersWithViews(options =>
            {
                var index = options.ValueProviderFactories.IndexOf(
                    options.ValueProviderFactories.OfType<QueryStringValueProviderFactory>().Single());
                options.ValueProviderFactories[index] = new CulturedQueryStringValueProviderFactory();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSingleton<TranslationTransformer>();
            services.AddSingleton<TranslationDatabase>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name: "duck_route",
                                                 areaName: "Duck",
                                                 pattern: "Manage/{controller}/{action}/{id?}");

                endpoints.MapAreaControllerRoute("blog_route", "Blog",
                    "Manage/{controller}/{action}/{id?}");

                endpoints.MapDynamicControllerRoute<TranslationTransformer>
    ("{language}/{controller}/{action}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); 
        }
        public class CulturedQueryStringValueProviderFactory : IValueProviderFactory
        {
            public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                var query = context.ActionContext.HttpContext.Request.Query;
                if (query != null && query.Count > 0)
                {
                    var valueProvider = new QueryStringValueProvider(
                        BindingSource.Query,
                        query,
                        CultureInfo.CurrentCulture);

                    context.ValueProviders.Add(valueProvider);
                }

                return Task.CompletedTask;
            }
        }

    }
}
