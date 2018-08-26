using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Catalog.Model
{
    public class Movie
    {
        [JsonProperty("MovieID")]
        public int MovieID { get; set; }
        [JsonProperty("MovieName")]
        public string MovieName { get; set; }
    }
}
