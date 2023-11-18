using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CB.EmbeddedResourceHelper
{
    public static class ResourceAssemblyExtensions
    {
        public static string ReadAllTextFromEmbeddedFile(this Assembly assembly, string resourceName)
        {
            using (var resourceStream = GetStreamFromEmbeddedFile(assembly, resourceName))
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var template = reader.ReadToEnd();
                return template;
            }
        }

        public static Stream GetStreamFromEmbeddedFile(Assembly assembly, string resourceName)
        {
            var resourcename = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(resourceName));
            
            if (resourcename == null)
                throw new Exception($"Embeded resource name '{resourceName}' not found in assembly '{assembly.FullName}'.");

            var resourceStream = assembly.GetManifestResourceStream(resourcename);
            
            return resourceStream;
        }
    }
}