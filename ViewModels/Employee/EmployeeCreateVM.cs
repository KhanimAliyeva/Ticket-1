using System.ComponentModel.DataAnnotations;

namespace Simulation_1Mpa201.ViewModels.Employee
{
    public class EmployeeCreateVM
    {
        [Required,MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public int BranchId { get; set; }
    }
}
