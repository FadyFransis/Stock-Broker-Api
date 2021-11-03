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
             new Stock{Id=1,Name="Vianet"},
             new Stock{Id=2,Name="Agritek"},
             new Stock{Id=3,Name="Akamai"},
             new Stock{Id=4,Name="Baidu"},
             new Stock{Id=5,Name="Blinkx"},
             new Stock{Id=6,Name="Blucora"},
             new Stock{Id=7,Name="Boingo"},
             new Stock{Id=8,Name="Brainybrawn"},
             new Stock{Id=9,Name="Carbonite"},
             new Stock{Id=10,Name="China Finance"},
             new Stock{Id=11,Name="ChinaCache"},
             new Stock{Id=12,Name="ADR"},
             new Stock{Id=13,Name="ChitrChatr"},
             new Stock{Id=14,Name="Cnova"},
             new Stock{Id=15,Name="Cogent"},
             new Stock{Id=16,Name="Crexendo"},
             new Stock{Id=17,Name="CrowdGather"},
             new Stock{Id=18,Name="EarthLink"},
             new Stock{Id=19,Name="Eastern"},
             new Stock{Id=20,Name="ENDEXX"},
             new Stock{Id=21,Name="Envestnet"},
             new Stock{Id=22,Name="Epazz"},
             new Stock{Id=23,Name="FlashZero"},
             new Stock{Id=24,Name="Genesis"},
             new Stock{Id=25,Name="InterNAP"},
             new Stock{Id=26,Name="MeetMe"},
             new Stock{Id=27,Name="Netease"},
             new Stock{Id=28,Name="Qihoo"},



            };
            if (context.Stock.FirstOrDefault() == null)
            {
                foreach(var item in stocks)
                {
                    Stock stock = new Stock
                    {
                        CreationDate = DateTime.Now,
                        LastUpdatedDate = DateTime.Now
                    };
                    stocks.Add(stock);
                }
                context.Stock.AddRange(stocks);
                context.SaveChanges();
            }
        }


    }
}
