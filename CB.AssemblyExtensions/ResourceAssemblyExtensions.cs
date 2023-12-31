using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CB.AssemblyExtensions
{
    public static class ResourceAssemblyExtensions
    {
        public static string ReadAllTextFromEmbeddedFile(this Assembly assembly, string resourceName)
        {
            using (var resourceStream = assembly.GetStreamFromEmbeddedFile(resourceName))
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var template = reader.ReadToEnd();
                return template;
            }
        }

        public static Stream GetStreamFromEmbeddedFile(this Assembly assembly, string resourceName)
        {
            var resourcename = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(resourceName));

            if (resourcename == null)
                throw new Exception($"Embeded resource name '{resourceName}' not found in assembly '{assembly.FullName}'.");

            var resourceStream = assembly.GetManifestResourceStream(resourcename);

            return resourceStream;
        }
    }
}