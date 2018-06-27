using BrAInsaveWebMain;
using BrAInsaveWebMain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BrAInsaveWebMainTest
{
    [TestClass]
    public class BlobServiceTests
    {
        private BlobService _blobService = new BlobService();

        [TestMethod]
        public async System.Threading.Tasks.Task Upload2BlobTestAsync()
        {
            string resultMsg = await BlobService.Upload2blob(ConfigService.BlobServiceConfig.blobContainer,
                ConfigService.getRootPath() + "/Resources/Test.json", "test1.json");
            Assert.AreEqual("Successfully uploaded", resultMsg);
        }


    }
}
