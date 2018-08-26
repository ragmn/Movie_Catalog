using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MovieCatalogAPI.Helper
{
    public class AppSettings
    {
        public static IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string JsonDataSource = _configuration.GetSection("test").GetSection("key").Value;
    }
}