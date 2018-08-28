using Microsoft.Extensions.Configuration;
using Movie_Catalog.Entity;
using Movie_Catalog.Services;
using MovieCatalogAPI.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Catalog.Repo
{
    public class MovieRepo : IMovie
    {
        IConfiguration _configuration;
        readonly JSonHelper _jsonHelper;
        public MovieRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _jsonHelper = new JSonHelper(_configuration.GetSection("JsonDataSource").GetSection("Path").Value);
        }

        public List<Movie> GetAll()
        {
            List<Movie> movieCatalog = new List<Movie>();
            try
            {
                var jsonObject = _jsonHelper.GetJSonString();
                movieCatalog = JsonConvert.DeserializeObject<List<Movie>>(jsonObject);
            }
            catch (Exception ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            return movieCatalog;
        }

        public Movie GetMovie(int id)
        {
            Movie movie;
            try
            {
                var jsonObject = _jsonHelper.GetJSonString();
                movie = JsonConvert.DeserializeObject<List<Movie>>(jsonObject).Find(a => a.MovieID == id);
            }
            catch (Exception ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            return movie;
        }

        public bool InsertMovie(Movie movie)
        {
            List<Movie> movieCatalog = new List<Movie>();

            try
            {
                var jsonObject = _jsonHelper.GetJSonString();
                movieCatalog = JsonConvert.DeserializeObject<List<Movie>>(jsonObject);
                movieCatalog.Add(movie);
                var asdf = JsonConvert.SerializeObject(movieCatalog);
                _jsonHelper.SaveJSonString(JsonConvert.SerializeObject(movieCatalog));
            }
            catch (Exception ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            return true;
        }

        public bool UpdateMovie(int id, Movie movie)
        {
            List<Movie> movieCatalog = new List<Movie>();
            try
            {
                var jsonObject = _jsonHelper.GetJSonString();
                movieCatalog = JsonConvert.DeserializeObject<List<Movie>>(jsonObject);
                if (movieCatalog.Any(a => a.MovieID == id))
                {
                    var found = movieCatalog.Find(a => a.MovieID == id);
                    found.MovieTitle = movie.MovieTitle;
                    found.Description = movie.Description;
                    found.ReleaseYear = movie.ReleaseYear;
                    found.ImageURL = movie.ImageURL;
                    found.Genre = movie.Genre;
                    var jsonString = JsonConvert.SerializeObject(movieCatalog);
                    _jsonHelper.SaveJSonString(jsonString);
                }
                else return false;
            }
            catch (Exception ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            return true;
        }

        public bool DeleteMovie(int id)
        {
            try
            {
                var movies = GetAll();
                if (movies.Any(a => a.MovieID == id))
                {
                    var movie = movies.Find(a => a.MovieID == id);
                    movies.Remove(movie);
                    var asdf = JsonConvert.SerializeObject(movies);
                    _jsonHelper.SaveJSonString(asdf);
                }
                else return false;
            }
            catch (Exception ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            return true;
        }
    }
}
