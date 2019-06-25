using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Positions.Models;
using Companys.Models;
using Containers.Models;
using EPPService.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Master.Controllers
{
    public class MasterController : ControllerBase
    {
        #region Namespace_Details
        // Example route: https://localhost:5001/Master/FiletoEPPlus
        // Under this controller you will create routes to the service for various package examples
        #endregion Namespace_Details

        public async Task<IActionResult> EPPlusExample(IFormFile file)
        {        
            //extention = "DB_Data" + extention;
            String extention = "DB_Data.xlsx";
            EPlusPlus service = new EPlusPlus();
            FileorJson foj = new FileorJson();

            if (file == null || file.Length == 0)
            {
                SqlLists tempjson = new SqlLists();
                foj.PCCList = GetAndConvJson(tempjson);
                    ReturnStreamAsFile(service.EPPlusDatatoFormat(foj), extention);
            } else {
               foj.FileDetails = file;
                    ReturnStreamAsFile(service.EPPlusDatatoFormat(foj), extention);
                // added a comment
            }
            return null;
        }

        public static HttpResponseMessage ReturnStreamAsFile(byte[] data, string fileName)
        {
            // Set Http Status Code
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            
            // Reset Stream Position
            result.Content = new ByteArrayContent(data);

            // Generic Content Header
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("applicattion/octet-stream");
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

            // Set file name sent to client
            result.Content.Headers.ContentDisposition.FileName = "Test.xlsx";
            return result;
        }

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
