using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.DTO.Response
{
    public class ProductUserDetails : BaseResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public string MainImage { get; set; }
        public string Description { get; set; }
        public List<ReviewResponse> Reviews { get; set; }
        public List<string> SubImages { get; set; }
    }
}
