using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simulation_1Mpa201.Context;
using Simulation_1Mpa201.Helpers;
using Simulation_1Mpa201.Models;
using Simulation_1Mpa201.ViewModels.Employee;

namespace Simulation_1Mpa201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
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

        public IActionResult Create()
        {
            SendBranchesWithViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(EmployeeCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existBranch = _context.Branches.Any(x => x.Id == vm.BranchId);
            if (!existBranch)
            {
                ModelState.AddModelError("BranchId", "Invalid branch");
            }
            if (!vm.ImageFile.CheckSize(2))
            {
                ModelState.AddModelError("ImageFile", "Image file must be less than 2mb");
            }

            if (!vm.ImageFile.CheckType("image/"))
            {
                ModelState.AddModelError("ImageFile", "Image file must be in image format");
            }

            string folderpath = Path.Combine(_environment.WebRootPath, "assets", "img");

            string imageurl = await vm.ImageFile.SaveFile(folderpath);

            Employee employee = new Employee()
            {
                Name = vm.Name,
                Description = vm.Description,
                BranchId = vm.BranchId,
                ImageUrl = imageurl
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existEmployee = _context.Employees.Find(id);
            if (existEmployee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(existEmployee);
            _context.SaveChanges();


            string folderpath = Path.Combine(_environment.WebRootPath, "assets", "img");
            string oldPath = Path.Combine(folderpath, existEmployee.ImageUrl);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var existEmployee = _context.Employees.Find(id);
            if (existEmployee == null)
            {
                return NotFound();
            }

            EmployeeUpdateVM vm = new()
            {
                Id = existEmployee.Id,
                Name = existEmployee.Name,
                Description = existEmployee.Description,
                BranchId = existEmployee.BranchId
            };

            SendBranchesWithViewBag();

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(EmployeeUpdateVM vm)
        {
            SendBranchesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            var existEmployee = await _context.Employees.FindAsync(vm.Id);
            if (existEmployee is null)
                return NotFound();

            var isExistBranch = await _context.Branches.AnyAsync(x => x.Id == vm.BranchId);
            if (!isExistBranch)
            {
                ModelState.AddModelError("BranchId", "Invalid branch");
                return View(vm);
            }

            if (vm.ImageFile is not null)
            {
                if (!vm.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image must be less than 2MB");
                    return View(vm);
                }

                if (!vm.ImageFile.CheckType("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Invalid image format");
                    return View(vm);
                }
            }

            existEmployee.Name = vm.Name;
            existEmployee.Description = vm.Description;
            existEmployee.BranchId = vm.BranchId;

            string folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");

            if (vm.ImageFile is not null)
            {
                string newImagePath = await vm.ImageFile.SaveFile(folderPath);
                string oldImagePath = Path.Combine(folderPath, existEmployee.ImageUrl);

                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);

                existEmployee.ImageUrl = newImagePath;
            }

            _context.Employees.Update(existEmployee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        private void SendBranchesWithViewBag()
        {
            var branches = _context.Branches.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            ViewBag.Branches = branches;
        }



    }
}
