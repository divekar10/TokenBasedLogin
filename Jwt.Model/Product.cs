using Jwt.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Model
{
    public class Product
    {
        [RequiredGreaterThanZero( ErrorMessage = "This field is required.. ")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name required.")]
        public string Name { get; set; }
    }
}
