using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Simulation_1Mpa201.Context;
using Simulation_1Mpa201.ViewModels.Employee;

namespace Simulation_1Mpa201.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {

            var employees = _context.Employees.Select(e => new EmployeeGetVM()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                BranchName = e.Branch.Name
            }).ToList();
            return View(employees);
        }

      
    }
}
