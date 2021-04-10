using System;

namespace TimesheetManager.Api.Models
{
    public class Project
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public string ScopeDocument {get;set;}
        public DateTime StartDate {get;set;}
        public int CustomerId {get;set;}
        public Customer Customer {get;set;}
    }
}