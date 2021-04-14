namespace TimesheetManager.Api.Models
{
    public class Issue
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public int PrijectId {get;set;}
        public int StageId {get;set;}
    }
}