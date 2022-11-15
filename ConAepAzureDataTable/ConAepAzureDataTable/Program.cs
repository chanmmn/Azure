using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace ConAepAzureDataTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Construct a new "TableServiceClient using a TableSharedKeyCredential.

            string storageUri = @"https://storageaccountnewcmm.table.core.windows.net/";
            string accountName = @"storageaccountnewcmm";
            string storageAccountKey = @"KlZYuWWok5z8G5GHkDZ0FDD0bEKJiBDc4ZXGZlTmuLUFCr11dcIv2Uo14pUb7P4xgcCO5/CHe3L7+AStrOYIHA==";

            var serviceClient = new TableServiceClient(
                new Uri(storageUri),
                new TableSharedKeyCredential(accountName, storageAccountKey));

            // Create a new table. The TableItem class stores properties of the created table.
            string tableName = "OfficeSupplies1p1";
            TableItem table = serviceClient.CreateTableIfNotExists(tableName);
            Console.WriteLine($"The created table's name is {table.Name}.");

            // Construct a new <see cref="TableClient" /> using a <see cref="TableSharedKeyCredential" />.
            var tableClient = new TableClient(
                new Uri(storageUri),
                tableName,
                new TableSharedKeyCredential(accountName, storageAccountKey));

            // Create the table in the service.
            //tableClient.Create();

            // Make a dictionary entity by defining a <see cref="TableEntity">.
            string partitionKey = "PK";
            string rowKey = "RK";

            var entity = new TableEntity(partitionKey, rowKey)
{
    { "Product", "Marker Set" },
    { "Price", 5.00 },
    { "Quantity", 21 }
};

            Console.WriteLine($"{entity.RowKey}: {entity["Product"]} costs ${entity.GetDouble("Price")}.");

            // Add the newly created entity.
            tableClient.AddEntity(entity);

            Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

            // Iterate the <see cref="Pageable"> to access all queried entities.
            foreach (TableEntity qEntity in queryResultsFilter)
            {
                Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
            }

            Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");

        }
    }
}