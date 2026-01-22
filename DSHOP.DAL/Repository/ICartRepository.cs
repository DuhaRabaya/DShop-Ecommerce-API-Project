using DSHOP.DAL.DTO.Request;
using DSHOP.DAL.DTO.Response;
using DSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public interface ICartRepository
    {
        Task<Cart> AddAsync(Cart request);
        Task<List<Cart>> getItems(string userId);
        Task<Cart?> getItem(string userId , int productId);
        Task<Cart> updateAsync(Cart cart);
        Task clearCart(string userId);
        Task DeleteAsync(Cart cart);
    }
}
