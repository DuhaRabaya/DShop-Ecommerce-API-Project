using DSHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Utils
{
    public class UserSeedData : ISeedData
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSeedData(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task DataSeed()
        {
            if(!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "DRabaya",
                    Email = "duharabaya4@gmail.com",
                    FullName = "DuhaRabaya",
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(user1,"Pass#@1du");
                await _userManager.AddToRoleAsync(user1, "Admin");
            }
        }
    }
}
