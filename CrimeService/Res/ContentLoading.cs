using System.IO;
using System.Reflection;
//Custom class to load embedded JSON agency resource
namespace CrimeService.Res
{
    static class ContentLoading
    {
        public static string GetJsonContent()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string NAME = "CrimeService.Res.agency.json";

            using (Stream stream = assembly.GetManifestResourceStream(NAME))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}