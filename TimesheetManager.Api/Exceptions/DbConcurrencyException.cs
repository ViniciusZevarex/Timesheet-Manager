using System;

namespace TimesheetManager.Api.Exceptions
{
    public class DbConcurrencyException :  ApplicationException
    {
        public DbConcurrencyException(string msg) : base(msg) {}

    }
}