using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace BrAInsaveWebMain.Models
{
    public class ConfigService
    {
        private static string configFilePath = getRootPath() + "/" + Constants.CONFIG_FILE_PATH;
        private static IConfiguration iConfig = getIConfig(configFilePath);

        public static CognitiveServiceConfig CognitiveServiceConfig = iConfig.GetSection("CognitiveService").Get<CognitiveServiceConfig>();
        public static BlobServiceConfig BlobServiceConfig = iConfig.GetSection("BlobService").Get<BlobServiceConfig>();

        private static IConfiguration getIConfig(string jsonPath)
        {
            var builder = new ConfigurationBuilder().AddJsonFile(jsonPath);
            IConfiguration Config = builder.Build();
            return Config;
        }

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
