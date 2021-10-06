using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCapTime.Data
{
    public class Project: Entity
    {       
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
