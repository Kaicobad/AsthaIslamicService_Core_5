using AsthaIslamicService.CacheManager;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.Repository.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsthaIslamicService
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
            services.AddControllersWithViews();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INameOfAllahService, NameOfAllahService>();
            services.AddScoped<IPrayerTimeService, PrayerTimeService>();
            services.AddScoped<IPrayerTimeCacheManager, PrayerTimeCacheManager>();
            services.AddScoped<IHadithService, HadithService>();
            services.AddScoped<IHadithCacheManager, HadithCacheManager>();
            services.AddScoped<IRamadanService, RamadanService>();
            services.AddScoped<IRamadanCacheManager, RamadanCacheManager>();
            services.AddScoped<IQuranService, QuranService>();
            services.AddScoped<ISurahService, SurahService>();
            services.AddScoped<IQuranCacheManager, QuranCacheManager>();
            services.AddScoped<IQuranService, QuranService>();
            services.AddScoped<IDuaCacheManager, DuaCacheManager>();
            services.AddScoped<IDuaService, DuaService>();
            services.AddScoped<IIslamicNameService, IslamicNameService>();
            services.AddHttpClient<TokenService>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Landing}/{action=Index}/{id?}");
            });
        }
    }
}
