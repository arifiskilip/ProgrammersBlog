using ProgrammersBlog.Entities.Concrete;

namespace WebUI.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public string CategoryAddPartial { get; set; } // PARTİAL VİEW
        public Category Category { get; set; }  // DATA 
        public string Message { get; set; } // Message
    }
}
