using Simulation_1Mpa201.Models.Common;

namespace Simulation_1Mpa201.Models
{
    public class Employee:BaseEntity
    {
        public string Name{ get; set; }
        public string Description{ get; set; }
        public string ImageUrl { get; set; }
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
    }
}
