using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;

namespace WebUI.Areas.Admin.Models
{
    public class UserAddAjaxModel
    {
        public string UserAddPartial { get; set; } // PARTİAL VİEW
        public User User { get; set; }  // DATA 
        public string Message { get; set; } // Message
    }
}
