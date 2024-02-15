using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Results;

namespace WebUI.Areas.Admin.Models
{
    public class CategoryUpdateViewModel
    {
        public string CategoryUpdatePartial { get; set; } // PARTİAL VİEW
        public Category Category { get; set; }  // DATA 
        public string Message { get; set; } // Message
        public ResultStatus ResultStatus { get; set; }
    }
}
