using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebASP.NET.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int FranchiseId { get; set; }

        [MaxLength(50)]
        public string MovieTitle { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        [MaxLength(50)]
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
        //public Franchise Franchise { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
