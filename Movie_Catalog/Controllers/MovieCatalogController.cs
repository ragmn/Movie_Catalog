using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Catalog.Model;
using MovieCatalogAPI.Helper;

namespace Movie_Catalog.Controllers
{
    [Produces("application/json")]
    [Route("api/MovieCatalog")]
    public class MovieCatalogController : Controller
    {
        IConfiguration _configuration;
        public MovieCatalogController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetMoviesMovie()
        {
            //string value1 = _configuration.GetSection("test").GetSection("key").Value;
            List<Movie> movieCatalog = new List<Movie>();
            try
            {
                var helper = new JSonHelper(_configuration.GetSection("JsonDataSource").GetSection("Path").Value);
                var jsonObject = helper.GetJSonString();
                movieCatalog = jsonObject.First.First.ToObject<List<Movie>>();

                var results = new ObjectResult(movieCatalog)
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
