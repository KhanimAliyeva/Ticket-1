using Microsoft.AspNetCore.Mvc;

namespace Simulation_1Mpa201.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
