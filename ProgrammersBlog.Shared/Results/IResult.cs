using ProgrammersBlog.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ProgrammersBlog.Shared.Results
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }

    public enum ResultStatus
    {
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4,
        Authentication = 5,
        Authorization = 6
    }
}
