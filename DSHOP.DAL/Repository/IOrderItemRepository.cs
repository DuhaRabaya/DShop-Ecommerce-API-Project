using DSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public interface IOrderItemRepository
    {
        Task AddAsync(List<OrderItem> request);
    }
}
