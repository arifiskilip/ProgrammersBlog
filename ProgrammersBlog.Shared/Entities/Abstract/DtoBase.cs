using ProgrammersBlog.Shared.Results;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public class DtoBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }
}
