using Microsoft.Extensions.Configuration;
using Movie_Catalog.Entity;
using Movie_Catalog.Services;
using MovieCatalogAPI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Catalog.Repo
{
    public class MovieRepo : IMovie
    {
        IConfiguration _configuration;
        public MovieRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Movie> GetAll()
        {
            List<Movie> movieCatalog = new List<Movie>();
            var helper = new JSonHelper(_configuration.GetSection("JsonDataSource").GetSection("Path").Value);
            var jsonObject = helper.GetJSonString();
            movieCatalog = jsonObject.First.First.ToObject<List<Movie>>();
            return movieCatalog;
        }
    }
}
