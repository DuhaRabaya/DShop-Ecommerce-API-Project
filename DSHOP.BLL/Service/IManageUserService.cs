using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.BLL.Service
{
    public interface IManageUserService
    {
        Task<List<UserResponse>> GetUsers();  
        Task<UserDetailsResponse> GetUserDetails(string userId);
        Task<BaseResponse> BlockUser(string userId);
        Task<BaseResponse> UnBLockUser(string userId);
        Task<BaseResponse> ChangeUserRoLe(ChangeUserRoleRequest request);
    }
}
