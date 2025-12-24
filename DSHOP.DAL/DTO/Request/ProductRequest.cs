using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.DTO.Request
{
    public class ProductRequest
    {
        List<ProductTranslationsRequest> translations { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public double Rate { get; set; }
        public IFormFile MainImage { get; set; }
        public int CategoryId { get; set; }
    }
}
