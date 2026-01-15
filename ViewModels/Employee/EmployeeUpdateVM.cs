using System.ComponentModel.DataAnnotations;

namespace Simulation_1Mpa201.ViewModels.Employee
{
    public class EmployeeUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        
        public IFormFile? ImageFile { get; set; }

        [Required]
        public int BranchId { get; set; }
    }
}
