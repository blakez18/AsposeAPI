using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aspose.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsposeController : ControllerBase
    {
        // Get api/aspose
        [HttpGet]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            if (file == null || file.Length == 0)
                return Content('File not selected')

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), 'wwwroot', file.GetFilename());

                using (var stream = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(stream);
            
            // do something with the stream

            return "File Uploaded"
        }

        [HttpGet]

        // [HttpPost]
        // public asyn Task<I
    }
}
