using DSHOP.DAL.DTO.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Validations
{
    public class ProductValidation : AbstractValidator<ProductRequest>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Rate)
           .LessThanOrEqualTo(5)
           .WithMessage("Rate must be between 0 and 5");
        }
    }
}
