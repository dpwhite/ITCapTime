using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITCapTime.API
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public List<ProjectViewModel> EmployeeProjects { get; set; }
    }
}
