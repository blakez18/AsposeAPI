using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Positions.Models;
using System.Linq;
using TempJson.Models;
using Companys.Models;
using Candidates.Models;

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
            tempJson tempjson = new tempJson();
            tempjson = GetAndConvJson(tempjson);
            if (file == null || file.Length == 0)
                return Content("File not selected");
            // Get CSV File
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);
            return null;
        }

        public tempJson GetAndConvJson(tempJson tj)
        {
            // Set Path
            const string posPath = "../Json/Position.json";
            const string compPath = "../Json/Company.json";
            const string canPath = "../Json/Candiate.json";
            List<string> paths = new List<string> { posPath, compPath, canPath };
            paths.ForEach(x =>
            {
                using (StreamReader r = new StreamReader(x))
                {
                    switch (x)
                    {
                        case posPath:
                            tj.position = JsonConvert.DeserializeObject<List<Position>>(r.ReadToEnd());
                            break;
                        case compPath:
                            tj.company = JsonConvert.DeserializeObject<List<Company>>(r.ReadToEnd());
                            break;
                        case canPath:
                            tj.candidate = JsonConvert.DeserializeObject<List<Candidate>>(r.ReadToEnd());
                            break;
                    }
                }
            });
            return tj;
        }

    }
}
