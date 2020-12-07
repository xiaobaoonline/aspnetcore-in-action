using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chapter07_Samples
{
    public class Startup
    {
        readonly string AllowXiaobao100Origin = "AllowXiaobao100Origin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();

            services.AddRazorPages(); // chapter7.6

            services.AddDataProtection(); // chapter7.4

            services.AddSingleton<MyAuthenticationHandler>(); // chapter7.1
            var authenticationScheme = "MyPolicy";
            services.AddAuthentication(authenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, MyAuthenticationHandler>(
                    authenticationScheme,
                    options => Configuration.Bind(authenticationScheme, options));

            services.AddSingleton<IAuthorizationHandler, MyAuthorizationHandler>(); //chapter7.2
            services.AddAuthorization(options=> {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().AddRequirements(new MyAuthorizationRequirement()).Build();
            });

            services.AddCors(options => // chapter7.9
            {
                options.AddPolicy(AllowXiaobao100Origin,
                builder =>
                {
                    builder.WithOrigins("https://www.xiaobao100.com");
                });
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();// chapter7.5    
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();// chapter7.9

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
