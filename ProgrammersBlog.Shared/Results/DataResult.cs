using System;

namespace ProgrammersBlog.Shared.Results
{
    public class DataResult<T> : IDataResult<T> 
    {
        public T Data { get; set; }

        public ResultStatus ResultStatus { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }


        public DataResult(ResultStatus resultStatus, T data)
        {
            this.ResultStatus = resultStatus;
            this.Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
            this.Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {
            this.ResultStatus = resultStatus;
            this.Message = message;
            this.Data = data;
            this.Exception = exception;
        }
    }
}
