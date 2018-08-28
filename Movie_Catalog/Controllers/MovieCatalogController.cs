using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Catalog.Entity;
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
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}", Name = "Get")]
        [Produces(typeof(List<Movie>))]
        public IActionResult GetMovie([FromRoute]int id)
        {
            var movie = _movieRepository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult PostMovie([FromBody]Movie movie)
        {
            var blnSucess = _movieRepository.InsertMovie(movie);
            if (blnSucess)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        
        [HttpPut("{id}")]
        public IActionResult PutMovie([FromRoute] int id, [FromBody]Movie movie)
        {
            var blnSucess = _movieRepository.UpdateMovie(id,movie);
            if (blnSucess)
            {
                return NotFound();
            }
            return Ok(movie);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var blnSucess = _movieRepository.DeleteMovie(id);
            if (blnSucess)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
