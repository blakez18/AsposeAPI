using System.Collections.Generic;

namespace Company.Models
{
    public class Company
    {
        public int CompanyId {get; set;}
        public string CompanyName {get; set;}
        public string Email {get; set;}
        public long mainNumb {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public List<int> PositionID {get; set;}
        
    }
}