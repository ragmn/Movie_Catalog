using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Entity;
using Newtonsoft.Json;

namespace Movie_Catalog.Web.Controllers
{
    public class MovieController : Controller
    {
        //private string uri = "http://ragmnmoviecatalogapi.azurewebsites.net/";
        private string uri = "http://localhost:63063/";
        [HttpGet]
        public async Task <ActionResult> GetData()
        {
            using (var client = new HttpClient())
            {
                List<Movie> movieList;
                try
                {
                    client.BaseAddress = new Uri(uri);
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


        [HttpGet]
        public async Task<IActionResult> GetMovie(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(uri);
                    var response = await client.GetAsync($"api/MovieCatalog/{id}");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var movie = JsonConvert.DeserializeObject<Movie>(stringResult);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(movie);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (HttpRequestException ex)
                {
                    return NotFound();
                }

            }
        }

        [HttpPost]
        // GET: Movie/Delete/5
        public async Task<bool> UpdateMovie(string data,int movieId)
        {
            bool blnSucess = false;
            using (var client = new HttpClient())
            {
                try
                {
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(uri);
                    var response = await client.PutAsync($"api/MovieCatalog/{movieId}", httpContent);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        blnSucess = true;
                    }
                    else
                    {
                        blnSucess = false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    return blnSucess;
                }

            }
            return blnSucess;
        }


        [HttpPost]
        // GET: Movie/Delete/5
        public async Task<bool> AddMovie(string data)
        {
            bool blnSucess = false;
            using (var client = new HttpClient())
            {
                try
                {
                    Random rnd = new Random();
                    int random = rnd.Next(100, 1000); // creates a number between 1 and 12
                    data= data.Replace("{id}", random.ToString());
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(uri);
                    var response = await client.PostAsync($"api/MovieCatalog/", httpContent);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        blnSucess = true;
                    }
                    else
                    {
                        blnSucess = false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    return blnSucess;
                }

            }
            return blnSucess;
        }


        [HttpPost]
        // GET: Movie/Delete/5
        public async Task<bool> Delete(string id)
        {
            bool blnSucess = false;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(uri);
                    var response = await client.DeleteAsync($"api/MovieCatalog/{id}");
                    response.EnsureSuccessStatusCode();
                    if(response.StatusCode== HttpStatusCode.OK)
                    {
                        blnSucess = true;
                    }
                    else
                    {
                        blnSucess = false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    return blnSucess;
                }
                
            }
            return blnSucess;
        }
    }
}