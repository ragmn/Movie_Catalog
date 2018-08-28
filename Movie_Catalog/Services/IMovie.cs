using Movie_Catalog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Catalog.Services
{
    public interface IMovie
    {
        List<Movie> GetAll();

        Movie GetMovie(int id);

        bool InsertMovie(Movie movie);

        bool DeleteMovie(int id);

        bool UpdateMovie(int id, Movie movie);

    }
}
