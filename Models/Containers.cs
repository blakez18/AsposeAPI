using System.Collections.Generic;


namespace Containers.Models
{
    public class SqlLists
    {
        public List<Positions.Models.Position> position {get; set;}
        public List<Candidates.Models.Candidate> candidate {get; set;}
        public List<Companys.Models.Company> company {get; set;}
    }
    public class FileorJson
    {
        public System.IO.FileInfo File { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile FileDetails { get; set; }
        public SqlLists PCCList { get; set; }   
         }


}