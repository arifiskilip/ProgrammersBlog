using System;

namespace ProgrammersBlog.Shared.Results
{
    public class Result : IResult
    {
        public ResultStatus ResultStatus { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public Result(ResultStatus resultStatus) 
        {
            this.ResultStatus = resultStatus;
        }
        public Result(ResultStatus resultStatus, String message)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
        }
        public Result(ResultStatus resultStatus, String message,Exception exception)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
            this.Exception = exception;
        }

    }
}
