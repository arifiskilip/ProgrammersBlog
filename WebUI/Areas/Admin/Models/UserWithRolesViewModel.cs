using ProgrammersBlog.Entities.Concrete;
using System.Collections.Generic;

namespace WebUI.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
