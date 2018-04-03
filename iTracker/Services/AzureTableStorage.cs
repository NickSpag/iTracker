using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTracker
{
    public static class AzureTableStorage
    {
        private const string snapShotTable = "Snapshots";

        private static CloudTable table;


        private static CloudTableClient TableClient()
        {
            var storageAccount1 = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(PrivateKeys.StorageConnection);
            return storageAccount1.CreateCloudTableClient();
        }

        private static void EstablishTable()
        {
            if (table == null)
            {
                table = TableClient().GetTableReference(snapShotTable);
            }
        }

        private static void RecordResults(IList<TableResult> results)
        {
            foreach (var result in results)
            {
                if (result.HttpStatusCode < 200 && result.HttpStatusCode > 300)
                {
                    System.Console.WriteLine($"Non-successful result:{result.HttpStatusCode}");
                }
            }
        }

        public static async Task UploadSnapshots(List<GazeTrainingSnapshot> snapshots)
        {
            EstablishTable();

            var batchOperation = new TableBatchOperation();

            foreach (var snapshot in snapshots)
            {
                batchOperation.Insert(snapshot);
            }

            var results = await table.ExecuteBatchAsync(batchOperation);

            RecordResults(results);
        }

    }
}
