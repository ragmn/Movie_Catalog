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
        public string GetJSonString()
        {
            string jsonString = string.Empty;
            try
            {
                using (StreamReader file = File.OpenText(FilePath))
                {
                    jsonString = file.ReadToEnd();
                }
            }
            catch(IOException ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return jsonString;
        }

        public bool SaveJSonString(string updatedJson)
        {
            try
            {
                File.WriteAllText(FilePath, updatedJson.Replace(@"\", string.Empty));
            }
            catch (IOException ex)
            {
                ex.LogExceptionToFile("Error.txt");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}