using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace MovieCatalogAPI.Helper
{
    public static class FileLogger
    {
        static Assembly assembly = Assembly.GetEntryAssembly();
        static string ExeDirectory = assembly == null ? null : Path.GetDirectoryName(assembly.Location);
        static StringBuilder sbWriteLog = new StringBuilder();

        public static string CurrentDirectoryPath
        {
            get
            {
                return ExeDirectory;
            }
        }

        /// <summary>
        /// Set the log path for logging exception and message.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static string LogPath(string fileName, bool isWindows = false)
        {
            string todaysDate = DateTime.Now.ToString("dd-MM-yyyy");
            string directoryPath = string.Empty;
            if (!isWindows)
            {
                directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", todaysDate);
            }
            else
            {
                directoryPath = Path.Combine(ExeDirectory, "Logs", todaysDate);
            }
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string path = directoryPath;
            path = Path.Combine(path, (string.IsNullOrEmpty(fileName) ? "ErrorLog.txt" : fileName.IndexOf(".txt") == -1 ? (fileName + ".txt") : fileName));
            return path;
        }

        public static bool IsFileExist(string fileNameWithExtension)
        {
            string filePath = LogPath(fileNameWithExtension);
            return File.Exists(filePath);
        }


        /// <summary>
        /// Log exception by passing file name with extension.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="fileNameWithExtension"></param>
        public static void LogExceptionToFile(this Exception exception, string fileNameWithExtension, bool isWindows = false)
        {
            try
            {
                string filePath = LogPath(fileNameWithExtension, isWindows);
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine("********** {0} **********", DateTime.Now);
                    if (exception.InnerException != null)
                    {
                        sw.Write("Inner Exception Type: ");
                        sw.WriteLine(exception.InnerException.GetType().ToString());
                        sw.Write("Inner Exception: ");
                        sw.WriteLine(exception.InnerException.Message);
                        sw.Write("Inner Source: ");
                        sw.WriteLine(exception.InnerException.Source);
                        if (exception.InnerException.StackTrace != null)
                        {
                            sw.WriteLine("Inner Stack Trace: ");
                            sw.WriteLine(exception.InnerException.StackTrace);
                        }
                    }
                    sw.Write("Exception Type: ");
                    sw.WriteLine(exception.GetType().ToString());
                    sw.WriteLine("Exception: " + exception.Message);
                    sw.WriteLine("Stack Trace: ");
                    if (exception.StackTrace != null)
                    {
                        sw.WriteLine(exception.StackTrace);
                        sw.WriteLine();
                    }
                }
            }
            catch
            {
            }
        }
    }
}