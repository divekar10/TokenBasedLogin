using Jwt.Database;
using Jwt.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Exceptions
{

    
    public class ExceptionLogging
    {
        //public static HttpRequest request;
        //public static string Path()
        //{
        //    var path = "";
        //    path = request.Path.Value.ToString();
        //    return path;
        //}

        public static void LogExceptionToDb(Exception ex)
        {
            var connectionstring = "Data Source=DESKTOP-M6QJC04;Initial Catalog=Demo;Integrated Security=True";

            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder.UseSqlServer(connectionstring);


           // UserContext dbContext = new UserContext(optionsBuilder.Options);
            using (UserContext db = new UserContext(optionsBuilder.Options))
            {
                string message = "";
                if (ex.InnerException != null)
                {
                    message = ex.InnerException.ToString();
                }
                
                //Url fuctionality not working...
                string url = "";
                
                //if (HttpContext.Current != null)
                //    url = HttpContext.Current.Request.Url.ToString();
                ExceptionLog l = new ExceptionLog();
                l.Msg = ex.Message.ToString() + message;
                l.Type = ex.GetType().Name.ToString();
                l.Url = url;
                l.Source = ex.StackTrace.ToString();
                l.CreatedDate = DateTime.Now;

                db.ExceptionLog.Add(l);
                db.SaveChanges();
            }
        }
    }
}
