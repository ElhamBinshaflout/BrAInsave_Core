using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BrAInsaveWebMain.Models
{
    public class BlobService
    {
        public static async Task<string> Upload2blob(string blobContainer, string filePath, string fileName)
        {
            BlobServiceConfig blobConfig = ConfigService.BlobServiceConfig;
            CloudStorageAccount storageAccount;

            if (CloudStorageAccount.TryParse(blobConfig.connectionString, out storageAccount))
                try
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainer);

                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    await cloudBlockBlob.UploadFromFileAsync(filePath);
                }
                catch (StorageException ex)
                {
                    return $"Error returned from the service: {ex.Message}";
                }
            else
                return
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.";
            return "Successfully uploaded";
        }

        public static string GetBlobFile(string blobFileName)
        {
            StorageCredentials creds = new StorageCredentials(
                ConfigService.BlobServiceConfig.storageAccount, 
                ConfigService.BlobServiceConfig.subscriptionKey);
            CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(
                ConfigService.BlobServiceConfig.blobContainer);
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
