using System.Collections.Generic;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.DTO.Franchise
{
    public class CreateFranchiseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
