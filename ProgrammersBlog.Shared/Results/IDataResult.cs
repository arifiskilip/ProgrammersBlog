using System;

namespace ProgrammersBlog.Shared.Results
{
    public interface IDataResult<T> 
    {
        public T Data { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
