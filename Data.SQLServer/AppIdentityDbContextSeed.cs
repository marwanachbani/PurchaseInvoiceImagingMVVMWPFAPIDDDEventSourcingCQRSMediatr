using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedIdentityData(UserManager<UserApp> userManager)
        {

            if (!userManager.Users.Any()) {

                var user = new UserApp
                {
                    UserName = "Administrator" , Email ="Administrator@gmail.com"
                    
                };
                await userManager.CreateAsync(user, "Administrator");

            }
            
        }
        
    }
}
