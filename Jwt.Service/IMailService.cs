using Jwt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Service
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
