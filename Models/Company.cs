using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Companys.Models
{
    public class Company
    {
        public int CompanyId {get; set;}
        public string CompanyName {get; set;}
        public string Address_1 {get; set;}
        public string Address_2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Zip {get; set;}
        public int Placement_Fee {get; set;}
        public int Fee_Collected {get;set;}
        public string Email {get; set;}
        public long mainNumb {get; set;}
        public List<int> PositionID {get; set;}
        
    }
}