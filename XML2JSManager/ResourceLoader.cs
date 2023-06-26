using System;
using System.IO;
using System.Reflection;

namespace XML2JSManager
{
    public static class ResourceLoader
    {
        public static string LoadResource(string resourceName)
        {
            string content = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullResourceName = $"{assembly.GetName().Name}.Resources.{resourceName}";

            using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    content = reader.ReadToEnd();
                }
            }

            return content;
        }
    }
}

