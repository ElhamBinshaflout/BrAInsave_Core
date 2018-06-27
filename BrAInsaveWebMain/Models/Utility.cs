using System.IO;
using System.Text.RegularExpressions;

namespace BrAInsaveWebMain.Models
{
    public class Utility
    {
        public static string getRootPath()
        {
            var currentDirectory = Path.GetDirectoryName(System.Reflection
                   .Assembly.GetExecutingAssembly().CodeBase);
            Regex re = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var rootPath = re.Match(currentDirectory).Value;
            return rootPath;
        }
    }
}
