using ProgrammersBlog.Entities.Concrete;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class RightSideBarViewModel
    {
        public IList<Category> Categories { get; set; }
        public IList<Article> Articles { get; set; }
    }
}
