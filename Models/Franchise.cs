using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebASP.NET.Models
{
    public class Franchise
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
