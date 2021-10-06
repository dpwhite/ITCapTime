using ITCapTime.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using AgileObjects.AgileMapper;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;

namespace ITCapTime.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        readonly ITCapTimeContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;

        public AuthController(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, ITCapTimeContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByNameAsync(request.UserName);
                if (existingUser == null)
                {
                    var user = Mapper.Map(request).ToANew<ApplicationUser>();
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        var claimesPrincipal = await userClaimsPrincipalFactory.CreateAsync(user);
                        user.Claims = claimesPrincipal.Claims.Select(c => new UserClaim { ClaimType = c.Type, ClaimValue = c.Value }).ToList();
                        var mapped = Mapper.Map(user).ToANew<UserResponse>();
                        return Ok(mapped);
                    }
                    else
                    {
                        var badRequests = result.Errors.ToList();
                        return BadRequest(badRequests.FirstOrDefault().Code);
                    }
                }
                return BadRequest();
            }
            return BadRequest(ModelState);
        }        

        private async Task<int> AddNewEmployee(string employeeName)
        {
            var name = employeeName.Split(' ');
            Employee employee = new Employee();
            employee.FirstName = name[0];
            employee.LastName = name[1];
            this.context.Employees.Add(employee);
            var retval = await this.context.SaveChangesAsync();
            return retval;
        }

        [AllowAnonymous]
        [HttpPost("login/{id}")]
        public async Task<IActionResult> Login(string id)
        {
            Guid employeeId;
            if (!Guid.TryParse(id, out employeeId))
            {
                return NotFound();
            }
            var employee = await GetEmployeeInfo(employeeId);
            var retval = JsonConvert.SerializeObject(employee);
            return Ok(employee);
        }

        private Task<Employee> GetEmployeeInfo(Guid id)
        {
            var employee = this.context.Employees
                .Where(e => e.Id == id).Single();

            var projectIds = this.context.ProjectEmployees
                .Where(p => p.EmployeeId == employee.Id)
                .Select(p => p.ProjectId);

            employee.Projects = this.context.Projects.Where(p => projectIds.Contains(p.Id)).ToList();
            return Task.Run(() => employee);
        }

        [AllowAnonymous]
        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            var message = await GetInfo();
            return Ok(message);
        }

        private Task<string> GetInfo()
        {
            var message = Task.Run(() => "test works");
            return message;
        }

    }
}
