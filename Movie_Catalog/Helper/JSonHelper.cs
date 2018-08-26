using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace MovieCatalogAPI.Helper
{
    public class JSonHelper
    {
        private readonly string FilePath;
        public JSonHelper(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// Gets JSON object
        /// </summary>
        /// <returns>JObject</returns>
        public JObject GetJSonString()
        {
            JObject jsonString;
            using (StreamReader file = File.OpenText(FilePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonString = (JObject)JToken.ReadFrom(reader);
            }
            return jsonString;
        }
    }
}