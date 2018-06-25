using BrAInsaveWebMain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BrAInsaveWebMainTest
{
    [TestClass]
    public class ConfigServiceTests
    {
        private readonly ConfigService _configService = new ConfigService();

        [TestMethod]
        public void GetConfigTest()
        {
            var config = ConfigService.getConfig();
            Assert.IsNotNull(config);
            Assert.AreEqual("brainsavemain", config.blobServiceConfig.storageAccount);
            Assert.AreEqual("1617d8c5cf1145fcabe716e600b6b6ae", config.cognitiveServiceConfig.subscriptionKey);

            config = ConfigService.getConfig(Directory.GetCurrentDirectory() + "/../../../Resources/TestingConfig.json");
            Assert.IsNotNull(config);
            Assert.AreEqual("c", config.blobServiceConfig.storageAccount);
            Assert.AreEqual("b", config.cognitiveServiceConfig.subscriptionKey);
        }
    }
}
