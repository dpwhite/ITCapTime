using ITCapTime.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITCapTime.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        readonly ITCapTimeContext _context;

        public EmployeesController(ITCapTimeContext context)
        {
            this._context = context;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var employees = this._context.Employees.ToList();
            return employees;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
            this._context.Employees.Add(value);
            var retval = this._context.SaveChanges();
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("projects/{id}")]
        public Task<List<ProjectEmployees>> GetEmployeeProjects(string id)
        {
            Guid employeeId;
            if (!Guid.TryParse(id, out employeeId))
            {
                return (Task<List<ProjectEmployees>>)Task.Run(() => new List<ProjectEmployees>());
            }
            var employeeProjects = this._context.ProjectEmployees.Where(pe => pe.EmployeeId == employeeId).ToList();
            return Task.Run(() => employeeProjects);
        }
    }
}
