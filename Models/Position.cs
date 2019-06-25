using System.Collections.Generic;
using System;
namespace Positions.Models
{
    public class Position
    {
        public int PositionId {get; set;}
        public int CompanyId {get; set;}
        public List<int> CandidateId {get; set;}
        public string PositionName {get;set;}
        public string Description {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public char Status {get; set;}
        public double MinSalary {get; set;}
        public double MaxSalary {get; set;}
    }
}