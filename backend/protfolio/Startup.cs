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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Net.Http;
using Microsoft.AspNetCore.Http;

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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddDbContext<ApplicationContext>();

            services.AddTransient<IUserContext, ApplicationContext>();
            services.AddTransient<ISpheresContext, ApplicationContext>();
            services.AddTransient<IProjectContext, ApplicationContext>();
            services.AddTransient<AuthenticateService>();
            services.AddTransient<ProjectRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<SpheresRepository>();
            services.AddTransient<SearchService>();


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
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseAuthentication();
            app.UseCookiePolicy(cookiePolicyOptions);
            //app.UseIdentity();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            //RegistrateUsers(app);
            //AddSpheres(app);
            //AddSpecs(app);
        }

        private void RegistrateUsers(IApplicationBuilder app)
        {
            var users = new List<RegisterModel>();

            for(int i=0;i<10; i++)
            {
            }

            using (var serv = app.ApplicationServices.CreateScope())
            {
                var prov = serv.ServiceProvider;
                var auth = prov.GetRequiredService<AuthenticateService>();
                var context = prov.GetRequiredService<ApplicationContext>();

                var c = context.Contacts.Add(new Contact()
                {
                    Name = "Телефон"
                }).Entity;
                context.Users.Add(new User()
                {
                    Description = "Студент 5 курса Самарского университета, кафедра наноинженерии. Участник олимпиады Молодой специалист. Участник студвестны 2019. Староста группы. Председатель студенческого профсоюза. ",
                    FirstName = "Кирилл",

                    SecondName = "Иванов",

                    Email = "kirill-ivanov@sm.com",

                    BirthDate = new DateTime(1998, 3, 12),

                    Gender = Gender.Male,
                    Password = new byte[64],
                    Salt = new byte[64],
                    RegisterDate = DateTime.Now,
                });

                context.Add(new User()
                {
                    FirstName = "Роман",

                    SecondName = "Димитров",

                    Email = "dimitrov@sm.com",

                    Password = new byte[64],
                    Salt = new byte[64],

                    BirthDate = new DateTime(1999, 01, 11),

                    Gender = Gender.Male,

                    Description = "Студент 3 курса Самарского университета, факультет государственного и муниципального управления. Стипендиат программы Правительства Самарской области. "
                });

                context.Add(new User()
                {
                    FirstName = "Ольга",

                    SecondName = "Петрова",

                    Email = "petrova-olga@sm.com",

                    Password = new byte[64],
                    Salt = new byte[64],

                    BirthDate = new DateTime(1996, 6, 15),

                    Gender = Gender.Female,

                    Description = "Выпускник Самарского экономического университета,специальность - Экология и природопользование.Все свое свободное время я посвящаю любимому хобби - рисованию. "
                });

                context.Add(new User()
                {
                    FirstName = "Константин",

                    SecondName = "Григорьев",

                    Email = "grigoriev-ks@sm.com",

                    Password = new byte[64],
                    Salt = new byte[64],

                    BirthDate = new DateTime(1994, 7, 3),

                    Gender = Gender.Female,

                    Description = "Магистр по специальности информационная безопасность Самарского университета. Участник Летней школы Информационных технологий МФТО."
                });


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
                foreach(var s in spheres)
                {
                    context.Spheres.Add(s);
                }
                context.SaveChanges();
            }
        }

        private void AddSpecs(IApplicationBuilder app)
        {
            var specs = new List<Specialization>();

            for(int i=0; i<4; i++)
            {
                specs.Add(new Specialization()
                {
                    Name = $"SPEC_NAME_{i}"
                });
            }

            using (var serv = app.ApplicationServices.CreateScope())
            {
                var prov = serv.ServiceProvider;
                var context = prov.GetRequiredService<ApplicationContext>();
                foreach (var s in specs)
                {
                    context.Specializations.Add(s);
                }

                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 1,
                    SpecializationId = 1
                });

                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 1,
                    SpecializationId = 2
                });

                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 1,
                    SpecializationId = 3
                });

                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 2,
                    SpecializationId = 1
                });

                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 2,
                    SpecializationId = 2
                });

                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 3,
                    SpecializationId = 3
                });

                var users = context.Users.ToList();
                foreach(var u in users)
                {
                    context.UserSpecializations.Add(new UserSpecializations()
                    {
                        UserId = u.Id,
                        SpecializationId = 1,
                        SphereId = 1
                    });
                }

                context.SaveChanges();
            }
        }

        
    }
}
