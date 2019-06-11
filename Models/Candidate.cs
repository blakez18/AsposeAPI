using System.Collections.Generic;

namespace Candidates.Models
{
    public class Candidates
    {
        public int CandidateID {get; set;}
        public string FName {get; set;}
        public string LName {get; set;}
        public string JobTitle {get; set;}
        public string Email {get; set;}
        public long mobileNumb {get; set;}
        public List<int> CompanyID {get; set;}
        public List<int> PositionID {get; set;}
    }
}