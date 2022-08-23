using colours.Data;
using colours.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace colours
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
            var server = Configuration["DBServer"] ?? "ms-sql-server";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword"] ?? "P@ssw0rd";
            var database = Configuration["DataBase"] ?? "colours-db";

            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlServer($"Server={server},{port}; Initial Catalog={database}; ID={user}; Password={password};Trusted_Connection=True; MultipleActiveResultSets=True;"
                    //b =>
                    //b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                )
            );

            

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "colours", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceProvider.GetService<ApplicationDbContext>().Database.EnsureDeleted();
                serviceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }
                

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "colours v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context1 = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            //    context1.Database.Migrate();
            //    context1.SaveChanges();
            //}
            //serviceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
