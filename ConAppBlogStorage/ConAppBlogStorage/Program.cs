using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConAppBlogStorage
{
    internal class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Azure Blob Storage v12 - .NET quickstart sample\n");
            const string connectionString = @"DefaultEndpointsProtocol=https;AccountName=AcountName;AccountKey=AccoutKey";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            //string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
            //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("cmmcontainer");
            // Create a local file in the ./data/ directory for uploading and downloading
            string localPath = "./data/";
            string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            string localFilePath = Path.Combine(localPath, fileName);
            // Write text to the file
            await File.WriteAllTextAsync(localFilePath, "Hello, World!");
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
            // Upload data from the local file
            await blobClient.UploadAsync(localFilePath, true);
        }
    }
}
