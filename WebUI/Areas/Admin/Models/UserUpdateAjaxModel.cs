using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Results;

namespace WebUI.Areas.Admin.Models
{
    public class UserUpdateAjaxModel
    {
        public string UserUpdatePartial { get; set; } // PARTİAL VİEW
        public User User { get; set; }  // DATA 
        public string Message { get; set; } // Message
        public ResultStatus ResultStatus { get; set; }
    }
}
