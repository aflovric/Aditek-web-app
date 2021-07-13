using Aditek.Models;
using Aditek.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Aditek")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 3;
            })
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<IImageData, SqlImageData>();
            services.AddScoped<IJobReferenceData, SqlJobReferenceData>();
            services.AddScoped<IJobReferenceJobTypeData, SqlJobReferenceJobTypeData>();
            services.AddScoped<IJobTypeData, SqlJobTypeData>();
            services.AddMvc(options =>
                options.EnableEndpointRouting = false
            );
            services.AddControllersWithViews();
        }
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
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}