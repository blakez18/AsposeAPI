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
using EPPService.Service;

namespace eppcont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EPPController : ControllerBase
    {
        // Get api/aspose
        [HttpGet]
        public async Task<IActionResult> FileUploader(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");
            // Get CSV File
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);

            
            return null;
        }

        [HttpPost]
        public tempJson JsonToWB()
        {
            tempJson tempjson = new tempJson();
            EPlusPlus epp = new EPlusPlus();

            tempjson = GetAndConvJson(tempjson);

            return tempjson;
            //epp.EPPSetup(tempjson);
            //return "working";
        }

        public tempJson GetAndConvJson(tempJson tj)
        {
            // Set Path
            string posPath = Path.Combine(Directory.GetCurrentDirectory(), "Json", "Position.json");
            string compPath = Path.Combine(Directory.GetCurrentDirectory(), "Json", "Company.json");
            string canPath = Path.Combine(Directory.GetCurrentDirectory(), "Json", "Candidate.json");
            List<string> paths = new List<string> { posPath, compPath, canPath };
            
            using (StreamReader r = new StreamReader(posPath))            
                tj.position = JsonConvert.DeserializeObject<List<Position>>(r.ReadToEnd());
            
            using (StreamReader r = new StreamReader(compPath))            
                tj.company = JsonConvert.DeserializeObject<List<Company>>(r.ReadToEnd());

            using (StreamReader r = new StreamReader(posPath))            
                tj.position = JsonConvert.DeserializeObject<List<Position>>(r.ReadToEnd());
            // paths.ForEach(x =>
            // {
            //     using (StreamReader r = new StreamReader(x))
            //     {
            //         if (x == posPath) {
            //                 tj.position = JsonConvert.DeserializeObject<List<Position>>(r.ReadToEnd());
            //         } else if (x == compPath) {
            //                 tj.company = JsonConvert.DeserializeObject<List<Company>>(r.ReadToEnd());
            //         } else if (x == canPath) {
            //                 tj.candidate = JsonConvert.DeserializeObject<List<Candidate>>(r.ReadToEnd());
            //         }
            //     }
            // });            
            return tj;
        }

    }
}
