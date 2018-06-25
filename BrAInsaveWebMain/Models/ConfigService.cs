using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace BrAInsaveWebMain.Models
{
    public class ConfigService
    {
        public static Config getConfig(string configJsonPath = null)
        {
            string jsonPath;
            if (configJsonPath == null)
                jsonPath = Directory.GetCurrentDirectory() + "/../../../" + Constants.DEFAULT_CONFIG_FILE_PATH;
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
    }
}
