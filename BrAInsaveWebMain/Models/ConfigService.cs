using Microsoft.Extensions.Configuration;

namespace BrAInsaveWebMain.Models
{
    public class ConfigService
    {
        private static string configFilePath = Utility.getRootPath() + Constants.CONFIG_FILE_PATH;
        private static IConfiguration iConfig = getIConfig(configFilePath);

        public static CognitiveServiceConfig CognitiveServiceConfig = iConfig.GetSection("CognitiveService").Get<CognitiveServiceConfig>();
        public static BlobServiceConfig BlobServiceConfig = iConfig.GetSection("BlobService").Get<BlobServiceConfig>();

        private static IConfiguration getIConfig(string jsonPath)
        {
            var builder = new ConfigurationBuilder().AddJsonFile(jsonPath);
            IConfiguration Config = builder.Build();
            return Config;
        }
    }
}
