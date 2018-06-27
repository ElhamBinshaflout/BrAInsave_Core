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
            Assert.AreEqual("brainsavemain", ConfigService.BlobServiceConfig.storageAccount);
            Assert.AreEqual("1617d8c5cf1145fcabe716e600b6b6ae", ConfigService.CognitiveServiceConfig.subscriptionKey);
        }
    }
}
