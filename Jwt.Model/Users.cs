using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter DOB")]
        public DateTime DOB { get; set; }

        public string PhotoPath { get; set; }
    }
}
