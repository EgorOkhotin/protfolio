using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using protfolio.Data;
using protfolio.Services;
using protfolio.Data.Repos;
using protfolio.Models;

namespace protfolio
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>();

            services.AddTransient<IUserContext, ApplicationContext>();
            services.AddTransient<ISpheresContext, ApplicationContext>();
            services.AddTransient<IProjectContext, ApplicationContext>();
            services.AddTransient<AuthenticateService>();
            services.AddTransient<ProjectRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<SpheresRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            RegistrateUsers(app);
        }

        private void RegistrateUsers(IApplicationBuilder app)
        {
            var users = new List<RegisterModel>();

            for(int i=0;i<10; i++)
            {
                var model = new RegisterModel()
                {
                    FirstName = $"FIRST_NAME_{i}",
                    SecondName = $"SECOND_NAME_{i}",
                    Email = $"EMAIL{i}@sm.com",
                    Password = $"somePass{i}"
                };
                users.Add(model);
            }

            using (var serv = app.ApplicationServices.CreateScope())
            {
                var prov = serv.ServiceProvider;
                var auth = prov.GetRequiredService<AuthenticateService>();
                foreach (var u in users)
                {
                    auth.Registrate(u).Wait();
                }
            }
        }

        private void AddSpheres(IApplicationBuilder app)
        {
            var spheres = new List<Sphere>();

            for(int i=0; i<3; i++)
            {
                spheres.Add(new Sphere()
                {
                    Name = $"SPHERE_{i}"
                });
            }

            using (var serv = app.ApplicationServices.CreateScope())
            {
                var prov = serv.ServiceProvider;
                var context = prov.GetRequiredService<ApplicationContext>();
            }
        }
    }
}
