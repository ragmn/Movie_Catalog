using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Catalog.Model;
using Movie_Catalog.Services;
using MovieCatalogAPI.Helper;

namespace Movie_Catalog.Controllers
{
    [Produces("application/json")]
    [Route("api/MovieCatalog")]
    public class MovieCatalogController : Controller
    {

        private readonly IMovie _movieRepository;
        public MovieCatalogController(IMovie movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [Produces(typeof(List<Movie>))]
     
        public IActionResult GetMovies()
        {
            List<Movie> movieCatalog = new List<Movie>();
            try
            {
                var results = new ObjectResult(_movieRepository.GetAll())
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
                return results;
            }
            catch (Exception ex)
            {
                ex.LogExceptionToFile("Error.txt");
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public string GetMovie(int id)
        {
            return "value";
        }
        
        [HttpPost]
        public void PostMovie([FromBody]string value)
        {
        }
        
        [HttpPut("{id}")]
        public void PutMovie(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void DeleteMovie(int id)
        {
        }
    }
}
