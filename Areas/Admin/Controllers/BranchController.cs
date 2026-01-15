using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation_1Mpa201.Context;
using Simulation_1Mpa201.Models;
using Simulation_1Mpa201.ViewModels.Employee;

namespace Simulation_1Mpa201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            var branches = _context.Branches.Select(x => new Branch()
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            return View(branches);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Branch branch)
        {
            if(!ModelState.IsValid)
            {
                return View(branch);
            }

            _context.Branches.Add(branch);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var existbranch = _context.Branches.Find(id);
            if(existbranch is not null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(  Branch vm)
        {

            if (!ModelState.IsValid)
                return View(vm);

            var existEmployee = await _context.Employees.FindAsync(vm.Id);
            if (existEmployee is null)
                return NotFound();


            existEmployee.Name = vm.Name;
           
            _context.Employees.Update(existEmployee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
