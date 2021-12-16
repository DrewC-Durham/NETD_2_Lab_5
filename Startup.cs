/* *******************************************
 * AUTHOR: Drew Cross
 * SUBMITTED TO: Prof. Alaadin Addas
 * DATE: December 16 2021
 * TITLE: Lab 5
 * DESCRIPTION:
 *      The purpose of this lab was to build a ASP.NET Core website that had 2 tables one which required a foreign key
 *      from the other. We were to use the entity framework to generate all classes and their members, as well as the tables.
 *      The EF also generated the CRUD operations for us and their respective pages and error handling. I modified some of the HTML dropdowns to only
 *      include logical responses based on the logic of my websites topic.
 * VERSION UPDATES:
 *      1.0 - December 16th (Drew) - Restarted and finished entire project.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//Make sure you import the models namespace.
using Lab_5_Final.Models;
//Make sure the following packages are installed via the nu get package manager.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Lab_5_Final
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

            services.AddRazorPages();
            services.AddMvc();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Here, we disable endpoint routing. This was an issue when upgrading from ASP.NET CORE 2.0 to ASP.NET Core 3.0
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //Adding connection
            string connection = @"Server=(localdb)\mssqllocaldb;Database=Lab_5_Final;Trusted_Connection=True;ConnectRetryCount=0";
            //Adding Db Context
            services.AddDbContext<FootballContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            /*
             
               To create database, go to tools --> Nu-Get Package Manager --> Package Manager Console --> type in command: Add-Migration InitialCreate
               MAKE SURE YOU install the Microsoft.EntityFramework.Tools from the Nu-Get package manager first.
               You should now see a directory named migrations in your solution explorer.
               Once you see the folder (directory) named migration. Go back to your Package Manager Console.
               Input: Update-Database.
               Go to View --> SQL Server Object Explorer, you should see the new DB!

            */
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

            app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
