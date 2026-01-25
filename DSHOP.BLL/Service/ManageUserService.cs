using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public class ManageUserService : IManageUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<UserResponse>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = users.Adapt<List<UserResponse>>();
            for(int i=0;i<users.Count; i++)
            {
                var roles=await _userManager.GetRolesAsync(users[i]);
                result[i].Roles=roles.ToList();
            }
            return result;
        }
        public async Task<BaseResponse> BlockUser(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "user not found"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user , DateTimeOffset.MaxValue);
            await _userManager.UpdateAsync(user);

            return new BaseResponse()
            {
                Success = true,
                Message = "user blocked"
            };
        }
        public async Task<BaseResponse> UnBLockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "user not found"
                };
            }
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.UpdateAsync(user);

            return new BaseResponse()
            {
                Success = true,
                Message = "user unblocked"
            };
        }

        public async Task<BaseResponse> ChangeUserRoLe(ChangeUserRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                return new BaseResponse()
                {
                    Success = false,
                    Message = "user not found"
                };
            }
            var roles= await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, request.Role);
            return new BaseResponse()
            {
                Success = true,
                Message = "role updated successfully"
            };
        }

        public async Task<UserDetailsResponse> GetUserDetails(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new UserDetailsResponse()
                {
                    Success = false,
                    Message = "user not found"
                };
            }
            var result = user.Adapt<UserDetailsResponse>();
            var roles = await _userManager.GetRolesAsync(user);
            result.Roles = roles.ToList();
            result.Success = true;

            return result;
        }
    }
}
