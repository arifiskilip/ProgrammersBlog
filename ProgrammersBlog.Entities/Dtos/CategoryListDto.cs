using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryListDto : DtoBase
    {
        public IList<Category> Categories { get; set; }
    }
}
