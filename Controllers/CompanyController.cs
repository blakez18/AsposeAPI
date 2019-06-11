using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Companys.Models;

namespace CompanyCont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int posID)
        {
            return "Get Not Working";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] Company comp)
        {
            return "Post Not Working";
        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int compId)
        {
            return "Delete Not Working";
        }
    }
}
