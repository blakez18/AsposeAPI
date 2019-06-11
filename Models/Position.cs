using System;

namespace Position.Models
{
    public class Position
    {
        public int PositionId {get; set;}
        public string PositionName {get;set;}
        public string Discription {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
    }
}