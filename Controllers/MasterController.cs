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

namespace Master.Controllers
{
    public class MasterController : ControllerBase
    {
        #region Namespace_Details
        // Example route: https://localhost:5001/Master/FiletoEPPlus
        // Under this controller you will create routes to the service for various package examples
        #endregion Namespace_Details
        
        #region Aspose
        //=== File Uploader ===//
        public string FiletoAspose(IFormFile file)// GET /Master/FiletoAspose
        {
            // if (file == null || file.Length == 0)
            //     return Content("File not selected");
            // // Get CSV File
            // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            // using (var stream = new FileStream(path, FileMode.Create))
            //     await file.CopyToAsync(stream);

            return "working";
        }

        //=== Json Converter ===//
        public string JsonToAspose() // GET /Master/JsonToAspose
        {
            // Declarations
            tempJson tempjson = new tempJson();
            EPlusPlus epp = new EPlusPlus();
            return "working";
            tempjson = GetAndConvJson(tempjson); // Call tempJson and convert
            return epp.EPPSetup(tempjson); // Create workbook
        }
        #endregion Aspose
        
        #region EPPlus
        //=== File Uploader ===//
        public string FiletoEPPlus(IFormFile file)
        {
            // if (file == null || file.Length == 0)
            //     return Content("File not selected");
            // // Get CSV File
            // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

            // using (var stream = new FileStream(path, FileMode.Create))
            //     await file.CopyToAsync(stream);

            return "working";
        }

        //=== Json Converter ===//

        public string JsonToEPPlus()
        {
            // Declarations
            tempJson tempjson = new tempJson();
            EPlusPlus epp = new EPlusPlus();
            return "working";
            tempjson = GetAndConvJson(tempjson); // Call tempJson and convert
            return epp.EPPSetup(tempjson); // Create workbook
        }
        #endregion EPPlus

        #region Functions
        //=============== JSON Function ================//
        public tempJson GetAndConvJson(tempJson tj) // Converts json file to list<>
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

            return tj;
        }
        #endregion Functions
    }
}
