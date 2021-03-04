using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Azure.Storage.Blobs;
namespace PhotoSharingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("StorageAccount");
            string containerName = "photos";
            Console.WriteLine("ejecutar..");
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.CreateIfNotExists();

            string blobName = "img2.png";
            string fileName = "images/img.png";
            BlobClient blobClient = container.GetBlobClient(blobName);

            blobClient.Upload(fileName, true);
            Console.WriteLine("subir..");
            var blobs = container.GetBlobs();
            foreach (var blob in blobs)
            {
                Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
            }
        }
    }
}
