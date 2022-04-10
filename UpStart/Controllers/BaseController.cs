using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpStart.Controllers
{
    public class BaseController : Controller
    {
        /// <summary></summary>
        /// <param name="result"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        [NonAction]
        public new IActionResult Response(object result = null, IReadOnlyCollection<string> errors = null)
        {
            if (errors == null || !errors.Any())
            {
                try
                {
                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }
                catch
                {
                    return BadRequest(new
                    {
                        success = false,
                        errors = new[] { "Internal Server Error." }
                    });
                }
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = errors
                });
            }
        }

    }
}
