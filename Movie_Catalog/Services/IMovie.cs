﻿using Movie_Catalog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Catalog.Services
{
    public interface IMovie
    {
        IEnumerable<Movie> GetAll();
    }
}
