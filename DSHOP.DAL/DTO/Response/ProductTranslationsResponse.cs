using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.DTO.Response
{
    public class ProductTranslationsResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Language { get; set; } = "en";
    }
}
