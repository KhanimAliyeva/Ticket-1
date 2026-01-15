using Microsoft.AspNetCore.Identity;

namespace Simulation_1Mpa201.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
