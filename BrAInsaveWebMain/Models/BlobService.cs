using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

//UNTESTED!
namespace BrAInsaveWebMain.Models
{

    public class BlobService
    {
                // this is a exampls of call "upload2blob"
        /*public static void Main()
        {
            string container = "upload2blob";
            string localPath = @"C:\Users\19332\Documents";
            string localFileName = "pdf2" + ".pdf";
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=brainsavemain;AccountKey=g7jAnP7SQ+7BO0va9ILro5R/Z38ocDtZDxoKk+Ta9lhwKtjuv8BkX8ZM13dnNpGscGMdOlZ/KzbornAfbq36XA==;EndpointSuffix=core.windows.net";

            Console.WriteLine("Azure Blob storage - .NET Quickstart sample");
            Console.WriteLine();
            Upload2blob(storageConnectionString, container, localPath, localFileName).GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit the sample application.");
            Console.ReadLine();
        }*/

        private static async Task Upload2blob(string storageConnectionString, string blobContainer, string localPath, string localFileName)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;

            string sourceFile = null;

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    // connect to a specific container
                    cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainer);
                    //Console.WriteLine("Current container: '{0}'", cloudBlobContainer.Name);
                    //Console.WriteLine();

                    // Set the permissions so the blobs are public. 
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);

                    sourceFile = Path.Combine(localPath, localFileName);

                    // Write text to the file.

                    //Console.WriteLine("Temp file = {0}", sourceFile);
                    //Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);
                    //Console.WriteLine();

                    // Get a reference to the blob address, then upload the file to the blob.
                    // Use the value of localFileName for the blob name.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                    await cloudBlockBlob.UploadFromFileAsync(sourceFile);

                    // List the blobs in the container.
                    Console.WriteLine("Listing blobs in container.");
                    BlobContinuationToken blobContinuationToken = null;
                    do
                    {
                        var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                        // Get the value of the continuation token returned by the listing call.
                        blobContinuationToken = results.ContinuationToken;
                        foreach (IListBlobItem item in results.Results)
                        {
                            Console.WriteLine(item.Uri);
                        }
                    } while (blobContinuationToken != null); // Loop while the continuation token is not null.
                    Console.WriteLine();
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
            }
        }
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
