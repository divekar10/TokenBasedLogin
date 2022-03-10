using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database
{
    public class RequiredGreaterThanZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int i;
            return value != null && int.TryParse(value.ToString(), out i) && i > 0;
        }
    }
}
