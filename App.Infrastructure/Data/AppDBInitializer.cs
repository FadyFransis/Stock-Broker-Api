using App.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace App.Infrastructure.Data
{
    public static class AppDBInitializer
    {
        public static void SeedSuperAdminUser(UserManager<AppUser> userManager, AppDBContext context)
        {
            if (context.Users.FirstOrDefault(u => u.Email == "admin@app.com") == null)
            {
                var defaultUser = new AppUser
                {
                    UserName = "admin@App.com",
                    Email = "admin@App.com",
                    FirstName = "Admin",
                    LastName = "App",
                    PhoneNumber = "01234567890",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(defaultUser, "Qwe_123456").Wait();
                userManager.AddToRoleAsync(defaultUser, "SuperAdmin").Wait();
                var res = userManager.AddClaimsAsync(defaultUser, new[]
                              {
                                new Claim("UserFullName", "Admin App")
                            }).Result;
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "SuperAdmin"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Client").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Client"
                };
                _ = roleManager.CreateAsync(role).Result;
            }

        }

        public static void SeedAppSetting(AppDBContext context)
        {
            List<AppSetting> AppSettings = new List<AppSetting>();
            if (context.AppSetting.FirstOrDefault() == null)
            {

                for (int i = 1; i <= 14; i++)
                {
                    AppSetting appSetting = new AppSetting
                    {
                        Key = (Core.Entities.Base.AppSettingKey)i,
                        CreationDate = DateTime.Now,
                        LastUpdatedDate = DateTime.Now
                    };
                    AppSettings.Add(appSetting);

                }
                context.AppSetting.AddRange(AppSettings);
                context.SaveChanges();
            }
        }

        public static void SeedStock(AppDBContext context)
        {
            List<Stock> stocks = new List<Stock> { 
             new Stock{Id=0,Name="Vianet",Price=1},
             new Stock{Id=0,Name="Agritek",Price=20},
             new Stock{Id=0,Name="Akamai",Price=22},
             new Stock{Id=0,Name="Baidu",Price=23},
             new Stock{Id=0,Name="Blinkx",Price=77},
             new Stock{Id=0,Name="Blucora",Price=24},
             new Stock{Id=0,Name="Boingo",Price=25},
             new Stock{Id=0,Name="Brainybrawn",Price=26},
             new Stock{Id=0,Name="Carbonite",Price=27},
             new Stock{Id=0,Name="China Finance",Price=28},
             new Stock{Id=0,Name="ChinaCache",Price=29},
             new Stock{Id=0,Name="ADR",Price=23},
             new Stock{Id=0,Name="ChitrChatr",Price=28},
             new Stock{Id=0,Name="Cnova",Price=12},
             new Stock{Id=0,Name="Cogent",Price=52},
             new Stock{Id=0,Name="Crexendo",Price=68},
             new Stock{Id=0,Name="CrowdGather",Price=62},
             new Stock{Id=0,Name="EarthLink",Price=72},
             new Stock{Id=0,Name="Eastern",Price=82},
             new Stock{Id=0,Name="ENDEXX",Price=92},
             new Stock{Id=0,Name="Envestnet",Price=54},
             new Stock{Id=0,Name="Epazz",Price=58},
             new Stock{Id=0,Name="FlashZero",Price=56},
             new Stock{Id=0,Name="Genesis",Price=53},
             new Stock{Id=0,Name="InterNAP",Price=82},
             new Stock{Id=0,Name="MeetMe",Price=88},
             new Stock{Id=0,Name="Netease",Price=2},
             new Stock{Id=0,Name="Qihoo",Price=2},



            };
            if (context.Stock.FirstOrDefault() == null)
            {
                foreach(var item in stocks)
                {
                       item.CreationDate = DateTime.Now;
                       item.LastUpdatedDate = DateTime.Now;
                }
                context.Stock.AddRange(stocks);
                context.SaveChanges();
            }
        }


    }
}
