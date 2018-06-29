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
                    CloudBlockBlob cloudBlockBlob = await getInitializedCloudBlockBlobAsync(storageAccount, blobContainer, fileName);
                    await cloudBlockBlob.UploadFromFileAsync(filePath);
                    return "Successfully uploaded";
                }
                catch (StorageException ex)
                {
                    throw ex;
                }
            else
                return
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.";
        }

        public static async Task<string> DownloadBlobFile(string blobContainer, string blobFileName)
        {
            BlobServiceConfig blobConfig = ConfigService.BlobServiceConfig;
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(blobConfig.connectionString, out storageAccount))
                try
                {
                    CloudBlockBlob cloudBlockBlob = await getInitializedCloudBlockBlobAsync(storageAccount, blobContainer, blobFileName);
                    //MemoryStream downloadedStream = new MemoryStream();
                    //await cloudBlockBlob.DownloadToStreamAsync(downloadedStream);
                    //Console.WriteLine(downloadedStream == null);
                    //StreamReader reader = new StreamReader(downloadedStream);
                    //string downloadedFileString = reader.ReadToEnd();
                    //Console.WriteLine(downloadedFileString == null);

                    await cloudBlockBlob.DownloadToFileAsync(
                        Utility.getRootPath() + 
                        Constants.DOWNLOADS_FOLDER_PATH + 
                        Constants.DOWNLOADED_FILE_NAME_PREFIX + 
                        blobFileName, FileMode.Create);
                    return "Successfully downloaded";
                }
                catch (StorageException ex)
                {
                    throw ex;
                }
            else
                return
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.";
        }

        private static async Task<CloudBlockBlob> getInitializedCloudBlockBlobAsync(CloudStorageAccount storageAccount, string blobContainer, string fileName)
        {
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainer);

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            return cloudBlockBlob;
        }
    }
}
