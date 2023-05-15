using Core.entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentitySeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "john",
                    Email = "john@gmail.com",
                    UserName = "john@gmail.com",
                    Address = new Address
                    {
                        FirstName = "john",
                        LastName = "Doe",
                        street = "10 st",
                        city = "New York",
                        state = "NY",
                        zipcode = "12345"
                    }
                };
                await userManager.CreateAsync(user, "Password123$!");
            }
        }
    }
}
