using ClosedXML.Excel;
using Jwt.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        protected FileContentResult Export()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Colors");
                var currentCell = 1;
                #region Header

                worksheet.Cell(currentCell, 1).Value = "Colors";

                #endregion

                #region Body

                worksheet.Cell(currentCell, 1).Value = _userService.Colors();

                #endregion

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var result = stream.ToArray();

                    return File(
                        result,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Colors.xlsx"
                        );
                }
            }
        }
    }
}
