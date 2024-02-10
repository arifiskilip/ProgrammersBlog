using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace ProgrammersBlog.Entities.Dtos
{
    public class ArticleListDto : DtoBase
    {
        public IList<Article> Articles { get; set; }
    }
}
