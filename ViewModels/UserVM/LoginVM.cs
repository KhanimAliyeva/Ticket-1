using System.ComponentModel.DataAnnotations;

namespace Simulation_1Mpa201.ViewModels.UserVM
{
    public class LoginVM
    {
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRemember{ get; set; }
    }
}
