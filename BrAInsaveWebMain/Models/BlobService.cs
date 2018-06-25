﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

//UNTESTED!
namespace BrAInsaveWebMain.Models
{
    public class BlobService
    {
        public static string GetBlobFile(string blobFileName)
        {
            StorageCredentials creds = new StorageCredentials(
                ConfigService.getConfig().blobServiceConfig.storageAccount, 
                ConfigService.getConfig().blobServiceConfig.subscriptionKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(
                ConfigService.getConfig().blobServiceConfig.blobContainer);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobFileName);

            string fileString;
            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStreamAsync(memoryStream);
                var length = memoryStream.Length;
                fileString = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return fileString;
        }
    }
}
