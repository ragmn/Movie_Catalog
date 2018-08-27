using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_Catalog.Entity
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string ImageURL { get; set; }
        public string ReleaseYear { get; set; }
        public string Genre { get; set; }
    }
}
