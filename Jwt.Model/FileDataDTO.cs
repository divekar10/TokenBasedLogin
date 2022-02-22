using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Model
{
    public class FileDataDTO
    {
        public IFormFile file { get; set; }
        public Users User { get; set; }
    }
}
