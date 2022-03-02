using System.Collections.Generic;
using TestWebASP.NET.Models;

namespace TestWebASP.NET.DTO.Requests
{
    public class UpdateCharacterDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public ICollection<Movie> Movie { get; set; }
    }
}
