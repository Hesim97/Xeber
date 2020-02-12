using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xeber.Entity;
using Xeber.Models;
using Xeber.Repository.Abstract;
using Xeber.Repository.Concrete.EntityFramework;

namespace Xeber
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<NewsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<INewsRepository, efNewsRepository>();
            services.AddTransient<ICategoryRepository, efCategoryRepository>();
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options=> {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            })               
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(opts => {
                opts.ResourcesPath = "Resources";
            });

            services.AddMvc()
                   .AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
                   .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                   .AddDataAnnotationsLocalization()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<RequestLocalizationOptions>(opts => {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("en-US"),
                    new CultureInfo("az"),                                     
                    new CultureInfo("az-AZ"),
                    new CultureInfo("ru"),
                    new CultureInfo("ru-RU"),
                  };

                opts.DefaultRequestCulture = new RequestCulture("az","az");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });
            services.Configure<RouteOptions>(options =>
               {
                   options.LowercaseUrls = true;
               });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }       
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=News}/{action=Index}/{id?}");
            });

        }
    }
}
