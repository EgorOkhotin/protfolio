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
                    Name = "Телефон" // 1
                }).Entity;
                context.Users.Add(new User()
                {
                    Description = "Студент 5 курса Самарского университета, кафедра наноинженерии. Участник олимпиады Молодой специалист. Участник студвестны 2019. Староста группы. Председатель студенческого профсоюза. ",
                    FirstName = "Кирилл",

                    SecondName = "Иванов",

                    Email = "kirill-ivanov@sm.com",
                    Image = @"zxBlLTtaer3bvd4lZUrmt2yCsF1iseUj1KK3XHDZjPcXKE6qiNNfobhn9OWncx4/mG32cPJTGaUDYKIa3JfQ0pGxErm+Y/wrtE8XIq18c0N+jqHo3r5LhomidIcuhzDn4N5fTClKhforHDpItwMNqCBj+lqmi32eTPJh4oIiKBIKl7BIwtdyKqRAaK9VUNlpfWJy5zCcN4RuSolb9VSVV8EMz/0J2fpZGMHnj5j7LoNwoILnRSUlS3ijkGD3j4hQy7+jyR0UTbXU8IicCC84eN87Hkq5ZzwXw+NxafZmXaHtIRUsHtR8yAc4+W61OfaPTi97HLPiB9x4qunqL1FfDR1NFNPTtj4X8DOX8x5Aj/4Kq6XGitOJJ7bMwQ5LZGsk4cD94gAAdeRUlLgrlBp4LcUs4wyOdzN8N9rb8j6A+K2sVXco+Al4ZHwkv8AWiNjvyxvjzUJGsb3c6yrgstubHFTt/VqGji3PLdu2efMnko/NcL46s46qaokwd2bgeQ2XWxGDZ1Ct1haaJgbUTNdN1ZCS/8AGfooxcfSY5oLaC3uznAdKcfTfP0UIr7ndYZW1QpCKYO4XF8excdwM887FZH7atdTC0zFsb+rXtJIPiBuFzbPGUiyMas4bJfatR1d2ppfXsB/EC0NBAA+asXvTNv1IGSy1Zp6ljeHtWtBBHcR181p6WtpWR5grKfh54Dxssr17iaC2eN4+Dgsr+SM9y4N0VU4bX0YkHoztjH5qb1JIzqI4Qw+ZJW6otKaSt5a4UpkeP3pXF2flnH0WplurQ7s3VUbT/CXgFesqQ/ft2n/ANgoydsvJkowpj0iXU8tmoBw0lLDEP5GgfZVyX2JgxG1oUSEoA2ePNUOlaN3SgeJUNjJ7kSGW/A59pYMl6LjsCVoZrlboRmSsjPwaeI/Ra2p1Tbodo455SdwcBoPzO/0U1S39EHfFdsmcdwaGl82/cFHdW1JqI6MD3C97iPiOHH3K0D9YTudiGkhYP8AyEuP4VpldVXWsD5njhYMBrRgD5LVpqdtibMWq1EZ1uKO1+im6U81ifbPdqYHdo4Y95ruR+mPJTxcw9EMJfPc6rgPC0MiDiOfP8ALp60W+bMVfigiIqyYREQBERAFZqKSnqoXQ1EEcsbxhzHtBBHgiICikt9HQQGCkpYYIiclkbA0E/JePtlBIcvo4HHvMYREO5ID6X2U9FpSnp6eBkYmqw88DQM4af7jyXEC08+9EW2nwMtj/IoOQrZJRF2TEUW3ZyvOHdEVD7LkOEjqvWtOURc+w+iqNzywtzsNlddD2sPcWjYoiRXBGTMVuVvbbH2bABzxnPxRFKjs5d0ju3oupBTaT7UHPrE7n+GAG/hTJEVVnkyyHij/2Q==""",

                    BirthDate = new DateTime(1998, 3, 12),

                    Gender = Gender.Male,
                    Password = new byte[64],
                    Salt = new byte[64],
                    RegisterDate = DateTime.Now,
                });

                context.Users.Add(new User()
                {
                    FirstName = "Роман",

                    SecondName = "Димитров",

                    Email = "dimitrov@sm.com",
                    Image = @"zxBlLTtaer3bvd4lZUrmt2yCsF1iseUj1KK3XHDZjPcXKE6qiNNfobhn9OWncx4/mG32cPJTGaUDYKIa3JfQ0pGxErm+Y/wrtE8XIq18c0N+jqHo3r5LhomidIcuhzDn4N5fTClKhforHDpItwMNqCBj+lqmi32eTPJh4oIiKBIKl7BIwtdyKqRAaK9VUNlpfWJy5zCcN4RuSolb9VSVV8EMz/0J2fpZGMHnj5j7LoNwoILnRSUlS3ijkGD3j4hQy7+jyR0UTbXU8IicCC84eN87Hkq5ZzwXw+NxafZmXaHtIRUsHtR8yAc4+W61OfaPTi97HLPiB9x4qunqL1FfDR1NFNPTtj4X8DOX8x5Aj/4Kq6XGitOJJ7bMwQ5LZGsk4cD94gAAdeRUlLgrlBp4LcUs4wyOdzN8N9rb8j6A+K2sVXco+Al4ZHwkv8AWiNjvyxvjzUJGsb3c6yrgstubHFTt/VqGji3PLdu2efMnko/NcL46s46qaokwd2bgeQ2XWxGDZ1Ct1haaJgbUTNdN1ZCS/8AGfooxcfSY5oLaC3uznAdKcfTfP0UIr7ndYZW1QpCKYO4XF8excdwM887FZH7atdTC0zFsb+rXtJIPiBuFzbPGUiyMas4bJfatR1d2ppfXsB/EC0NBAA+asXvTNv1IGSy1Zp6ljeHtWtBBHcR181p6WtpWR5grKfh54Dxssr17iaC2eN4+Dgsr+SM9y4N0VU4bX0YkHoztjH5qb1JIzqI4Qw+ZJW6otKaSt5a4UpkeP3pXF2flnH0WplurQ7s3VUbT/CXgFesqQ/ft2n/ANgoydsvJkowpj0iXU8tmoBw0lLDEP5GgfZVyX2JgxG1oUSEoA2ePNUOlaN3SgeJUNjJ7kSGW/A59pYMl6LjsCVoZrlboRmSsjPwaeI/Ra2p1Tbodo455SdwcBoPzO/0U1S39EHfFdsmcdwaGl82/cFHdW1JqI6MD3C97iPiOHH3K0D9YTudiGkhYP8AyEuP4VpldVXWsD5njhYMBrRgD5LVpqdtibMWq1EZ1uKO1+im6U81ifbPdqYHdo4Y95ruR+mPJTxcw9EMJfPc6rgPC0MiDiOfP8ALp60W+bMVfigiIqyYREQBERAFZqKSnqoXQ1EEcsbxhzHtBBHgiICikt9HQQGCkpYYIiclkbA0E/JePtlBIcvo4HHvMYREO5ID6X2U9FpSnp6eBkYmqw88DQM4af7jyXEC08+9EW2nwMtj/IoOQrZJRF2TEUW3ZyvOHdEVD7LkOEjqvWtOURc+w+iqNzywtzsNlddD2sPcWjYoiRXBGTMVuVvbbH2bABzxnPxRFKjs5d0ju3oupBTaT7UHPrE7n+GAG/hTJEVVnkyyHij/2Q==""",

                    Password = new byte[64],
                    Salt = new byte[64],

                    BirthDate = new DateTime(1999, 01, 11),

                    Gender = Gender.Male,

                    Description = "Студент 3 курса Самарского университета, факультет государственного и муниципального управления. Стипендиат программы Правительства Самарской области. "
                });

                context.Users.Add(new User()
                {
                    FirstName = "Ольга",

                    SecondName = "Петрова",
                    Image = @"zxBlLTtaer3bvd4lZUrmt2yCsF1iseUj1KK3XHDZjPcXKE6qiNNfobhn9OWncx4/mG32cPJTGaUDYKIa3JfQ0pGxErm+Y/wrtE8XIq18c0N+jqHo3r5LhomidIcuhzDn4N5fTClKhforHDpItwMNqCBj+lqmi32eTPJh4oIiKBIKl7BIwtdyKqRAaK9VUNlpfWJy5zCcN4RuSolb9VSVV8EMz/0J2fpZGMHnj5j7LoNwoILnRSUlS3ijkGD3j4hQy7+jyR0UTbXU8IicCC84eN87Hkq5ZzwXw+NxafZmXaHtIRUsHtR8yAc4+W61OfaPTi97HLPiB9x4qunqL1FfDR1NFNPTtj4X8DOX8x5Aj/4Kq6XGitOJJ7bMwQ5LZGsk4cD94gAAdeRUlLgrlBp4LcUs4wyOdzN8N9rb8j6A+K2sVXco+Al4ZHwkv8AWiNjvyxvjzUJGsb3c6yrgstubHFTt/VqGji3PLdu2efMnko/NcL46s46qaokwd2bgeQ2XWxGDZ1Ct1haaJgbUTNdN1ZCS/8AGfooxcfSY5oLaC3uznAdKcfTfP0UIr7ndYZW1QpCKYO4XF8excdwM887FZH7atdTC0zFsb+rXtJIPiBuFzbPGUiyMas4bJfatR1d2ppfXsB/EC0NBAA+asXvTNv1IGSy1Zp6ljeHtWtBBHcR181p6WtpWR5grKfh54Dxssr17iaC2eN4+Dgsr+SM9y4N0VU4bX0YkHoztjH5qb1JIzqI4Qw+ZJW6otKaSt5a4UpkeP3pXF2flnH0WplurQ7s3VUbT/CXgFesqQ/ft2n/ANgoydsvJkowpj0iXU8tmoBw0lLDEP5GgfZVyX2JgxG1oUSEoA2ePNUOlaN3SgeJUNjJ7kSGW/A59pYMl6LjsCVoZrlboRmSsjPwaeI/Ra2p1Tbodo455SdwcBoPzO/0U1S39EHfFdsmcdwaGl82/cFHdW1JqI6MD3C97iPiOHH3K0D9YTudiGkhYP8AyEuP4VpldVXWsD5njhYMBrRgD5LVpqdtibMWq1EZ1uKO1+im6U81ifbPdqYHdo4Y95ruR+mPJTxcw9EMJfPc6rgPC0MiDiOfP8ALp60W+bMVfigiIqyYREQBERAFZqKSnqoXQ1EEcsbxhzHtBBHgiICikt9HQQGCkpYYIiclkbA0E/JePtlBIcvo4HHvMYREO5ID6X2U9FpSnp6eBkYmqw88DQM4af7jyXEC08+9EW2nwMtj/IoOQrZJRF2TEUW3ZyvOHdEVD7LkOEjqvWtOURc+w+iqNzywtzsNlddD2sPcWjYoiRXBGTMVuVvbbH2bABzxnPxRFKjs5d0ju3oupBTaT7UHPrE7n+GAG/hTJEVVnkyyHij/2Q==""",
                    Email = "petrova-olga@sm.com",

                    Password = new byte[64],
                    Salt = new byte[64],

                    BirthDate = new DateTime(1996, 6, 15),

                    Gender = Gender.Female,

                    Description = "Выпускник Самарского экономического университета,специальность - Экология и природопользование.Все свое свободное время я посвящаю любимому хобби - рисованию. "
                });

                context.Users.Add(new User()
                {
                    FirstName = "Константин",

                    SecondName = "Григорьев",

                    Email = "grigoriev-ks@sm.com",
                    Image = @"zxBlLTtaer3bvd4lZUrmt2yCsF1iseUj1KK3XHDZjPcXKE6qiNNfobhn9OWncx4/mG32cPJTGaUDYKIa3JfQ0pGxErm+Y/wrtE8XIq18c0N+jqHo3r5LhomidIcuhzDn4N5fTClKhforHDpItwMNqCBj+lqmi32eTPJh4oIiKBIKl7BIwtdyKqRAaK9VUNlpfWJy5zCcN4RuSolb9VSVV8EMz/0J2fpZGMHnj5j7LoNwoILnRSUlS3ijkGD3j4hQy7+jyR0UTbXU8IicCC84eN87Hkq5ZzwXw+NxafZmXaHtIRUsHtR8yAc4+W61OfaPTi97HLPiB9x4qunqL1FfDR1NFNPTtj4X8DOX8x5Aj/4Kq6XGitOJJ7bMwQ5LZGsk4cD94gAAdeRUlLgrlBp4LcUs4wyOdzN8N9rb8j6A+K2sVXco+Al4ZHwkv8AWiNjvyxvjzUJGsb3c6yrgstubHFTt/VqGji3PLdu2efMnko/NcL46s46qaokwd2bgeQ2XWxGDZ1Ct1haaJgbUTNdN1ZCS/8AGfooxcfSY5oLaC3uznAdKcfTfP0UIr7ndYZW1QpCKYO4XF8excdwM887FZH7atdTC0zFsb+rXtJIPiBuFzbPGUiyMas4bJfatR1d2ppfXsB/EC0NBAA+asXvTNv1IGSy1Zp6ljeHtWtBBHcR181p6WtpWR5grKfh54Dxssr17iaC2eN4+Dgsr+SM9y4N0VU4bX0YkHoztjH5qb1JIzqI4Qw+ZJW6otKaSt5a4UpkeP3pXF2flnH0WplurQ7s3VUbT/CXgFesqQ/ft2n/ANgoydsvJkowpj0iXU8tmoBw0lLDEP5GgfZVyX2JgxG1oUSEoA2ePNUOlaN3SgeJUNjJ7kSGW/A59pYMl6LjsCVoZrlboRmSsjPwaeI/Ra2p1Tbodo455SdwcBoPzO/0U1S39EHfFdsmcdwaGl82/cFHdW1JqI6MD3C97iPiOHH3K0D9YTudiGkhYP8AyEuP4VpldVXWsD5njhYMBrRgD5LVpqdtibMWq1EZ1uKO1+im6U81ifbPdqYHdo4Y95ruR+mPJTxcw9EMJfPc6rgPC0MiDiOfP8ALp60W+bMVfigiIqyYREQBERAFZqKSnqoXQ1EEcsbxhzHtBBHgiICikt9HQQGCkpYYIiclkbA0E/JePtlBIcvo4HHvMYREO5ID6X2U9FpSnp6eBkYmqw88DQM4af7jyXEC08+9EW2nwMtj/IoOQrZJRF2TEUW3ZyvOHdEVD7LkOEjqvWtOURc+w+iqNzywtzsNlddD2sPcWjYoiRXBGTMVuVvbbH2bABzxnPxRFKjs5d0ju3oupBTaT7UHPrE7n+GAG/hTJEVVnkyyHij/2Q==""",
                    Password = new byte[64],
                    Salt = new byte[64],

                    BirthDate = new DateTime(1994, 7, 3),

                    Gender = Gender.Female,

                    Description = "Магистр по специальности информационная безопасность Самарского университета. Участник Летней школы Информационных технологий МФТО."
                });

                context.UserContacts.Add(new UserContacts()
                {
                    ContactId = 1,
                    UserId = 1,
                    Value = "+7-9277-77-88-99"
                });
                context.UserContacts.Add(new UserContacts()
                {
                    ContactId = 1,
                    UserId = 2,
                    Value = "+7-9277-77-88-00"
                });
                context.UserContacts.Add(new UserContacts()
                {
                    ContactId = 1,
                    UserId = 3,
                    Value = "+7-9266-77-88-99"
                });
                context.UserContacts.Add(new UserContacts()
                {
                    ContactId = 1,
                    UserId = 4,
                    Value = "+7-9277-77-00-99"
                });


                //***************************
                context.Specializations.Add(new Specialization()
                {
                    Name = "Backend" //1
                });

                context.Specializations.Add(new Specialization()
                {
                    Name = "Frontend" //2
                });
                context.Specializations.Add(new Specialization()
                {
                    Name = "Managment" //3
                });

                context.Specializations.Add(new Specialization()
                {
                    Name = "Illustration" //4
                });
                context.Specializations.Add(new Specialization()
                {
                    Name = "FirmStyle" //5
                });
                context.Specializations.Add(new Specialization()
                {
                    Name = "SMM" //6
                });

                //************************
                context.Spheres.Add(new Sphere()
                {
                    Name = "IT"//1
                });

                context.Spheres.Add(new Sphere()
                {
                    Name = "Design" //2
                });

                context.Spheres.Add(new Sphere()
                {
                    Name = "Marketings" //3
                });
                //***********************************


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
                    SpecializationId = 4
                });
                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 2,
                    SpecializationId = 5
                });

                //***************************
                context.SphereSpecializations.Add(new SphereSpecializations()
                {
                    SphereId = 3,
                    SpecializationId = 6
                });

                context.UserSpecializations.Add(new UserSpecializations()
                {
                    UserId = 1,
                    SphereId = 1,
                    SpecializationId = 1
                });
                context.UserSpecializations.Add(new UserSpecializations()
                {
                    UserId = 2,
                    SphereId = 1,
                    SpecializationId = 2
                });
                context.UserSpecializations.Add(new UserSpecializations()
                {
                    UserId = 3,
                    SphereId = 1,
                    SpecializationId = 3
                });
                context.UserSpecializations.Add(new UserSpecializations()
                {
                    UserId = 4,
                    SphereId = 1,
                    SpecializationId = 4
                });
                //*********************************************

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
