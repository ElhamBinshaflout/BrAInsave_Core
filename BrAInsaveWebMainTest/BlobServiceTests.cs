using BrAInsaveWebMain;
using BrAInsaveWebMain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BrAInsaveWebMainTest
{
    [TestClass]
    public class BlobServiceTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task Upload2BlobTestAsync()
        {
            string resultMsg = await BlobService.Upload2blob(ConfigService.BlobServiceConfig.blobContainer,
                Utility.getRootPath() + "/Resources/Test.json", "test1.json");
            Assert.AreEqual("Successfully uploaded", resultMsg);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DownloadBlobFileTestAsync()
        {
            string filename = "test1.json";

            //upload a file to test container
            await BlobService.Upload2blob(ConfigService.BlobServiceConfig.blobContainer,
                Utility.getRootPath() + "/Resources/Test.json", filename);

            //download it
            string downloadedString = await BlobService.DownloadBlobFile(ConfigService.BlobServiceConfig.blobContainer,
                filename);
            Assert.AreEqual("Successfully downloaded", downloadedString);
            File.Delete(Utility.getRootPath() +
                Constants.DOWNLOADS_FOLDER_PATH +
                Constants.DOWNLOADED_FILE_NAME_PREFIX +
                filename);
        }
    }
}
