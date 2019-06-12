using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Positions.Models;
using Microsoft.EntityFrameworkCore;

namespace PositionCont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        // GET api/PositionCont
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/PositionCont/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int posID)
        {
            return "Get Not Working";
        }

        // POST api/PositionCont
        [HttpPost]
        public ActionResult<string> Post([FromBody] Position pos)
        {
            return "Post Not Working";
        }

        // PUT api/PositionCont/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/PositionCont/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int posId)
        {
            return "Delete Not Working";
        }
    }
}
