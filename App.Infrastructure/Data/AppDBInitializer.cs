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
             new Stock{Id=0,Name="Vianet"},
             new Stock{Id=0,Name="Agritek"},
             new Stock{Id=0,Name="Akamai"},
             new Stock{Id=0,Name="Baidu"},
             new Stock{Id=0,Name="Blinkx"},
             new Stock{Id=0,Name="Blucora"},
             new Stock{Id=0,Name="Boingo"},
             new Stock{Id=0,Name="Brainybrawn"},
             new Stock{Id=0,Name="Carbonite"},
             new Stock{Id=0,Name="China Finance"},
             new Stock{Id=0,Name="ChinaCache"},
             new Stock{Id=0,Name="ADR"},
             new Stock{Id=0,Name="ChitrChatr"},
             new Stock{Id=0,Name="Cnova"},
             new Stock{Id=0,Name="Cogent"},
             new Stock{Id=0,Name="Crexendo"},
             new Stock{Id=0,Name="CrowdGather"},
             new Stock{Id=0,Name="EarthLink"},
             new Stock{Id=0,Name="Eastern"},
             new Stock{Id=0,Name="ENDEXX"},
             new Stock{Id=0,Name="Envestnet"},
             new Stock{Id=0,Name="Epazz"},
             new Stock{Id=0,Name="FlashZero"},
             new Stock{Id=0,Name="Genesis"},
             new Stock{Id=0,Name="InterNAP"},
             new Stock{Id=0,Name="MeetMe"},
             new Stock{Id=0,Name="Netease"},
             new Stock{Id=0,Name="Qihoo"},



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
