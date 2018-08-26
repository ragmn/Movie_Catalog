using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        public IEnumerable<string> GetMoviesMovie()
        {
            string value1 = _configuration.GetSection("test").GetSection("key").Value;

            return new string[] { "value1", "value2" };
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
