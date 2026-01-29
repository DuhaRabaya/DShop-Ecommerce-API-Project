using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Validations
{
    public class MinValue : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValue(int minValue)
        {
            _minValue = minValue;
        }
        public override bool IsValid(object? value)
        {
            if(value is decimal val)
            {
                if (val > _minValue)
                {
                    return true;
                }
            }
            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{name} invalid!";
        }
    }
}
