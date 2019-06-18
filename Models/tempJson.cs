using System.Collections.Generic;
using Candidates.Models;
using Companys.Models;
using Positions.Models;

namespace TempJson.Models
{
    public class tempJson
    {
        public List<Position> position {get; set;}
        public List<Candidate> candidate {get; set;}
        public List<Company> company {get; set;}
    }
}