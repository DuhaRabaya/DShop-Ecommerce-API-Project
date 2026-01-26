using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.DTO.Request
{
    public class ReviewRequest
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Comment { get; set; }
        [Required]
        [Range(1,5)]
        public int Rate { get; set; }
    }
}
