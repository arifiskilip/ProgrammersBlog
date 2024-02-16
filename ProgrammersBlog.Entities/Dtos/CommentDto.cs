using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CommentDto : DtoBase
    {
        public Comment Comment { get; set; }
    }
}
