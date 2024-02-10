using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryDto : DtoBase
    {
        public Category Category { get; set; }
    }
}
