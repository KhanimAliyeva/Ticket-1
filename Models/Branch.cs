using Simulation_1Mpa201.Models.Common;

namespace Simulation_1Mpa201.Models
{
    public class Branch:BaseEntity
    {
        public string  Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
