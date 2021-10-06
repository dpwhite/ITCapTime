using AgileObjects.AgileMapper;
using ITCapTime.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ITCapTime.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        readonly ITCapTimeContext context;

        public ProjectsController(ITCapTimeContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await context.Projects                
                .AsNoTracking()
                .ToListAsync();

            var mapper = new AutoMapper.Mapper(ConfigurationMapper.config);
            var projectView = mapper.Map<ProjectViewModel[]>(projects);

            return Ok(projectView);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProject(string id)
        {
            Guid projectId;
            if (!Guid.TryParse(id, out projectId))
            {
                return NotFound();
            }

            var project = await context.Projects
                .Where(p => p.Id == projectId)
                .AsNoTracking()
                .SingleAsync();

            return Ok(project);
        }

        [HttpGet("employee/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjectsForEmployee(string id)
        {
            Guid employeeId; 
            if (!Guid.TryParse(id, out employeeId))
            {
                return NotFound();
            }

            var projectIds = context.ProjectEmployees
                .Where(pe => pe.EmployeeId == employeeId)
                .Select(pe => pe.ProjectId)
                .ToList();

            var employeeProjects = await context.Projects
                .Where(p => projectIds.Contains(p.Id))
                .ToListAsync();

            var mapper = new AutoMapper.Mapper(ConfigurationMapper.config);
            var projectView = mapper.Map<ProjectViewModel[]>(employeeProjects);
            return Ok(projectView);
        }

        [HttpPut("project/{id}")]
        public void UpdateProject(string id, [FromBody] Project project)
        {
            var p = context.Projects.Update(project);
            context.SaveChanges();
        }


        //[AllowAnonymous]
        //[HttpGet("test")]
        //public async Task<IActionResult> Test()
        //{
        //    var message = await GetInfo();
        //    return Ok(message);
        //}

        //private Task<string> GetInfo()
        //{
        //    return Task.Run(() => "login works");
        //}
    }
}
