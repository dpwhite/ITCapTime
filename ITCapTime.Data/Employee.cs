using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCapTime.Data
{
    public class Employee : Entity
    {       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
