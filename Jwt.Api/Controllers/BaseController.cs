using ClosedXML.Excel;
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
        protected readonly IUserService _userService;
        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        protected FileStreamResult Export<T>(IEnumerable<T> list)
        {
            //using (var workbook = new XLWorkbook())
            //{
            //    var worksheet = workbook.Worksheets.Add("Colors");
            //    var currentCell = 1;
            //    #region Header

            //    worksheet.Cell(currentCell, 1).Value = "Colors";

            //    #endregion

            //    #region Body

            //var result = _userService.GetUsers();
                

                //#endregion

                var stream = new MemoryStream();

                using (var excle = new ExcelPackage(stream))
                {
                var workSheet = excle.Workbook.Worksheets.Add("User List");
                workSheet.Cells.LoadFromCollection(list, true);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                excle.SaveAs(stream);
                //workbook.SaveAs(stream);
                //var result = stream.;
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
