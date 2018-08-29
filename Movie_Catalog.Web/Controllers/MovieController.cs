using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Entity;
using Newtonsoft.Json;

namespace Movie_Catalog.Web.Controllers
{
    public class MovieController : Controller
    {
        
        [HttpGet]
        public async Task <ActionResult> GetData()
        {
            using (var client = new HttpClient())
            {
                List<Movie> movieList;
                try
                {
                    //client.BaseAddress = new Uri($"http://localhost:63063/");
                    client.BaseAddress = new Uri("http://ragmnmoviecatalogapi.azurewebsites.net/");
                    var response = await client.GetAsync($"api/MovieCatalog");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    movieList = JsonConvert.DeserializeObject<List<Movie>>(stringResult);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
                return Json(new { data = movieList });
            }
        }

        // GET: Movie/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

       
    }
}