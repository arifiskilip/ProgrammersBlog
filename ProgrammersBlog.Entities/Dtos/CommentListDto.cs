using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CommentListDto : DtoBase
    {
        public IList<Comment> Comments { get; set; }
    }
}
