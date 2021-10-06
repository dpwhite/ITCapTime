using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace ITCapTime.Data.Seeding
{

    public static class ITCapTimeSeeding
    {
        private const int minProjects = 3;
        private const int minEmployees = 5;
        private const int minProjectsPerEmployee = 1;
        private const int maxProjectsPerEmployee = 5;
        private const int maxProjects = 8;
        private const int maxEmployees = 15;

        //enum ProjectTypesEnum : int { Capital = 1, Expense = 2 };
        public static async Task Seed(ITCapTimeContext context)
        {
            if (await context.Employees.IgnoreQueryFilters().AnyAsync()) return;
            await AddEmployees(context);
            await AddProjects(context);
            await AddEmployeesToProjects(context);
        }

        private static async Task AddEmployeesToProjects(ITCapTimeContext context)
        {
            var employees = context.Employees.ToList();
            foreach (var employee in employees)
            {
                var projectCount = Randomizer.Seed.Next(minProjectsPerEmployee, maxProjectsPerEmployee);
                var projects = context.Projects.Take(projectCount).ToList();
                foreach (var project in projects)
                {
                    var monthNumber = Randomizer.Seed.Next(1, 12);
                    var projectEmployee = new ProjectEmployees();
                    projectEmployee.ProjectId = project.Id;
                    projectEmployee.EmployeeId = employee.Id;
                    projectEmployee.DateAdded = project.StartDate.AddMonths(monthNumber);
                    context.ProjectEmployees.Add(projectEmployee);
                }
            }
            await context.SaveChangesAsync();
        }

        private static async Task AddEmployees(ITCapTimeContext context)
        {
            var employeeTitles = GetEmployeeTitles();
            var employeeCount = Randomizer.Seed.Next(minEmployees, maxEmployees);
            for (int i = 0; i < employeeCount; i++)
            {
                var fakeEmployee = new Faker<Employee>()
                    .RuleFor(l => l.FirstName, f => f.Name.FirstName())
                    .RuleFor(l => l.LastName, f => f.Name.LastName())
                    .RuleFor(l => l.Title, f => f.PickRandom(employeeTitles));
                var employee = fakeEmployee.Generate();
                employee.Email = $"{employee.FirstName}.{employee.LastName}@test.com";
                context.Employees.Add(employee);
            }

            await context.SaveChangesAsync();
        }

        private static async Task AddProjects(ITCapTimeContext context)
        {
            var projects = GetProjects();
            var projectCount = Randomizer.Seed.Next(minProjects, maxProjects);
            for (int i = 0; i < projectCount; i++)
            {
                var projectType = new List<ProjectType> { ProjectType.Capital, ProjectType.Expense };
                var fakeProject = new Faker<Project>()
                    .RuleFor(p => p.Name, f => f.PickRandom(projects))
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.StartDate, f => f.Date.Between(new DateTime(2017, 1, 1), DateTime.Today))
                    .RuleFor(p => p.ProjectType, f => f.PickRandom(projectType));
                var project = fakeProject.Generate();
                context.Projects.Add(project);
            }
            await context.SaveChangesAsync();
        }

        private static List<string> GetEmployeeTitles()
        {
            var employeeTitles = new List<string>
            {
                "Lead Developer",
                "Project Manager",
                "Business Analyst",
                "Developer",
                "Architect",
                "Database Admin",
            };
            return employeeTitles;
        }

        private static List<string> GetProjects()
        {
            var projects = new List<string>
            {
                "Brine Time",
                "Chase",
                "Compensation Workbench",
                "Conroe - New Legal Entity",
                "Data Analytics",
                "Disability ESS US Self Identification",
                "E-Contracts",
                "FA Project NDF-20-54",
                "Field Computer Standards",
                "Fleet Management",
                "HRIS Transformation",
                "Hyperion",
                "iExpense Enhancement",
                "India",
                "ITSM Implementation - FootPrints Software & Labour AFE 19-25",
                "MFA / Secure Auth",
                "NMIS Fleet Management",
                "OKC Office IT Equipment",
                "Oracle Algeria",
                "Project SCRUM",
                "Prudential Demographic & Port and Convert Integrations",
                "Rimini Street",
                "Third Party Management"
            };
            return projects;
        }
    }
}
