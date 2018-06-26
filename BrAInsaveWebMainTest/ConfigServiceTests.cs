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
        [TestMethod]
        public void GetConfigTest()
        {
            //get config with default path to config file
            var config = ConfigService.getConfig();
            Assert.IsNotNull(config);
            Assert.AreEqual("brainsavemain", config.blobServiceConfig.storageAccount);
            Assert.AreEqual("1617d8c5cf1145fcabe716e600b6b6ae", config.cognitiveServiceConfig.subscriptionKey);

            //get config with customized path to config file
            config = ConfigService.getConfig(Directory.GetCurrentDirectory() + "/../../../Resources/TestingConfig.json");
            Assert.IsNotNull(config);
            Assert.AreEqual("c", config.blobServiceConfig.storageAccount);
            Assert.AreEqual("b", config.cognitiveServiceConfig.subscriptionKey);
        }
    }
}
