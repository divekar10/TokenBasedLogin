using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Model
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
