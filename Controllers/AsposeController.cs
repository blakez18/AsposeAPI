using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspose.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsposeController : ControllerBase
    {
        // Get api/aspose
        [HttpGet]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);
            
            // do something with the stream

            return null;
        }
    }
}
