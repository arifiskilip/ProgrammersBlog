using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Results;

namespace WebUI.Areas.Admin.Models
{
    public class CategoryAddAjaxModel
    {
        public CategoryAddDto CategoryAddDto { get; set; }
        public string CategoryAddPartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}
