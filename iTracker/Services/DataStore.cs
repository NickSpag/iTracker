using System;
using Microsoft.Azure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.Storage.Auth;
namespace iTracker
{
    public static class DataStore
    {

        public static CloudTableClient TableClient()
        {
            var credentials = new StorageCredentials("accountName", "keyValue", "keyName");

            var tableClient = new CloudTableClient(new Uri(""), credentials);


            var storageAccount = CloudStorageAccount.Parse(PrivateKeys.StorageConnection);
            var tableClient = storageAccount.c
        }


        static CloudBlobContainer GetContainer(ContainerType containerType)
        {
            var account = CloudStorageAccount.Parse(Constants.StorageConnection);
            var client = account.CreateCloudBlobClient();
            return client.GetContainerReference(containerType.ToString().ToLower());
        }
    }
}
