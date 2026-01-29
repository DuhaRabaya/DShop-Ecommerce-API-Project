using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Models
{
    public class Product: BaseModel
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public double Rate { get; set; }
        public string MainImage { get; set; }
        public int CategoryId { get; set; }
        public Category Category {  get; set; }

        public List<ProductTranslations> Translations { get; set; } 
        public List<ProductImage> SubImages { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
