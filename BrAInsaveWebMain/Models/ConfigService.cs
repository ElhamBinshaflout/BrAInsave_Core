using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace BrAInsaveWebMain.Models
{
    public class ConfigService
    {
        public static Config getConfig(string configJsonPath = null)
        {
            string jsonPath;
            if (configJsonPath == null)
                jsonPath = getRootPath() + "/" + Constants.DEFAULT_CONFIG_FILE_PATH;
            else
                jsonPath = configJsonPath;

            var config = new Config();
            IConfiguration iConfig = getIConfig(jsonPath);

            config.cognitiveServiceConfig = iConfig.GetSection("CognitiveService").Get<CognitiveServiceConfig>();
            config.blobServiceConfig = iConfig.GetSection("BlobService").Get<BlobServiceConfig>();

            return config;
        }

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
