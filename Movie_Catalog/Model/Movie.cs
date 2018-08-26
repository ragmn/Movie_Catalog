using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Catalog.Model
{
    public class Movie
    {
        [JsonProperty("ID")]
        public int MovieID { get; set; }
        [JsonProperty("MovieName")]
        public string MovieTitle { get; set; }
        [JsonProperty("Description")]
        public string MovieDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
    }

}
