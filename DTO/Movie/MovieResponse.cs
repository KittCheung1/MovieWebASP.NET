using System.Collections.Generic;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.DTO.Responses
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public int FranchiseId { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Picture { get; set; }
        public string Trailer { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
