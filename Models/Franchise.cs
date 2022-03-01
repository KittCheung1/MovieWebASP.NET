﻿using System.Collections.Generic;

namespace TestWebASP.NET.Models
{
    public class Franchise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
