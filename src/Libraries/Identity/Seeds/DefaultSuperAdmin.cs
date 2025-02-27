﻿using System;
using System.Collections.Generic;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Models.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = Guid.NewGuid().ToString(),
                Email = "prueba@prueba.com",
                FirstName = "Sinan",
                LastName = "Tok",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Preferences =
                [
                    new Preference
                    {
                        Type = "Theme",
                        Value = "Light"
                    }
                ]
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Shopper.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}