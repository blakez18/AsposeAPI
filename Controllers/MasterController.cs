using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Positions.Models;
using System.Linq;
using MasterModel.Models;
using Companys.Models;
using Candidates.Models;
using Containers.Models;
using EPPService.Service;
using AsposeService.Service;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public async Task<IActionResult> FiletoAspose(IFormFile file) // GET /Master/FiletoAspose
        {
            AsposeExcel service = new AsposeExcel();

            if (file == null || file.Length == 0)
                return Content("File not selected");
            FileInfo fi = new FileInfo(file.FileName);
            var data = service.AsposeFiletoWB(fi, file);
            return null;
        }

        //=== Json  ===/
        public tempJson JsonToAspose() // GET /Master/JsonToAspose
        {
            // Declarations
            tempJson tempjson = new tempJson();
            AsposeExcel service = new AsposeExcel();

            tempjson = GetAndConvJson(tempjson); // Call tempJson and convert
            return service.AsposeJsontoWB(tempjson); // Create workbook
        }
        #endregion Aspose

        #region EPPlus
        //=== File Uploader ===//
        public async Task<IActionResult> FiletoEPPlus(IFormFile file) // GET /Master/FiletoEPPlus
        {
            EPlusPlus service = new EPlusPlus();


            FileInfo fi = new FileInfo(file.FileName);
            var data = service.EPPFiletoWS(fi, file);
            return null;
        }

        //=== Json ==//
        public HttpResponseMessage JsonToEPPlus() // GET Master/JsonToEPPlus
        {
            // Declarations
            masterModel tempjson = new masterModel();
            EPlusPlus service = new EPlusPlus();


            tempjson = GetAndConvJson(tempjson); // Call tempJson and convert
            return service.EPPJsontoWS(tempjson); // Create workbook
        }

        public HttpResponseMessage EPPlusExample(IFormFile file)
        {
            EPlusPlus service = new EPlusPlus();

            if (file == null || file.Length == 0)
            {
                SqlLists tempjson = new SqlLists();

                return ReturnStreamAsFile(service.EPPlusDatatoFormat(null));
            }






            return null;
        }

        public static HttpResponseMessage ReturnStreamAsFile(MemoryStream stream, string fileName)
        {
            // Set Http Status Code
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            // Reset Stream Position
            stream.Position = 0;
            result.Content = new StreamContent(stream);

            // Generic Content Header
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("applicattion/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

            // Set file name sent to client
            result.Content.Headers.ContentDisposition.FileName = "";

            return result;
        }

        #endregion EPPlus

        #region Functions
        //=== Json Converter ===//
        public SqlLists GetAndConvJson(SqlLists tj) // Converts json file to list<>
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
