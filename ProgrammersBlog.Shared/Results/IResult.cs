using System;

namespace ProgrammersBlog.Shared.Results
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }

    public enum ResultStatus
    {
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
}
