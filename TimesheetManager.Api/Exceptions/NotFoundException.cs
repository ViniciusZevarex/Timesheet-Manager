using System;


namespace TimesheetManager.Api.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string msg) : base(msg) { }
    }
}