using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Companys.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyCont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        // GET api/CompanyCont
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/CompanyCont/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int posID)
        {
            return "Get Not Working";
        }

        // POST api/CompanyCont
        [HttpPost]
        public ActionResult<string> Post([FromBody] Company comp)
        {
            return "Post Not Working";
        }

        // PUT api/CompanyCont/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/CompanyCont/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int compId)
        {
            return "Delete Not Working";
        }
    }
}
