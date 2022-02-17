using System;
using System.ComponentModel.DataAnnotations;

namespace Jwt.Model
{
    public class Register
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
    }
}
