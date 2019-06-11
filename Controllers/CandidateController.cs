using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Candidates.Models;

namespace CandidateCont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int canID)
        {
            return "Get Not Working";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] Candidate can)
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
        public ActionResult<string> Delete(int posId)
        {
            return "Delete Not Working";
        }
    }
}
