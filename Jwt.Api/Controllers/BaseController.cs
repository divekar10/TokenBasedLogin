using ClosedXML.Excel;
using Jwt.Database.Utility;
using Jwt.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //protected readonly IUserService _userService;
        public BaseController()
        {
            //_userService = userService;
        }

        protected IActionResult JsonResponse(object obj) => (obj != null) ? NSResponse(obj) : NSNotFound;

        protected OkObjectResult NSResponse(object obj) => Ok(new { Status = APIDefaultMessages.Success, Code = 200, ResponseData = obj });
        protected NotFoundObjectResult NSNotFound => NotFound(new { Status = APIDefaultMessages.RecordNotFound, Code = 401, ReposponseData = new object() });

        protected FileStreamResult Export<T>(IEnumerable<T> list)
        {
  
                var stream = new MemoryStream();

                using (var excle = new ExcelPackage(stream))
                {
                var workSheet = excle.Workbook.Worksheets.Add("User List");
                workSheet.Cells.LoadFromCollection(list, true);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                excle.SaveAs(stream);
                }
                stream.Position = 0;

                 return File(
                        stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Users.xlsx"
                        ); 
        }
    }
}
