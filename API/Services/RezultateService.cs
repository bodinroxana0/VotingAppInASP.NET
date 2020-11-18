using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Services
{
    public class RezultateService : IDisposable
    {
        private CloudTable Rezultate;

        public RezultateService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Rezultate = tableClient.GetTableReference("Rezultate");

        }

        public async Task Initialize()
        {
            await Rezultate.CreateIfNotExistsAsync();
        }

        public async Task<List<RezultatEntity>> GetRezultat()
        {
            if (Rezultate == null)
            {
                throw new Exception();
            }
            
            var rezultat = new List<RezultatEntity>();
            TableQuery<RezultatEntity> query = new TableQuery<RezultatEntity>(); 
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<RezultatEntity> resultSegment = await Rezultate.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                rezultat.AddRange(resultSegment.Results);
            } while (token != null);

            return rezultat;
        }
        public async Task<List<RezultatEntity>> GetRezultatJudet(string judet)
        {
            if (Rezultate == null)
            {
                throw new Exception();
            }
            
            var rezultate = new List<RezultatEntity>();
            TableQuery<RezultatEntity> query = new TableQuery<RezultatEntity>().Where(TableQuery.GenerateFilterCondition("Judet", QueryComparisons.Equal, judet));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<RezultatEntity> resultSegment = await Rezultate.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                rezultate.AddRange(resultSegment.Results);
            } while (token != null);

            return rezultate;
        }
        public async Task<TableResult> AddRezultat(RezultatEntity rezultat)
        {
            if (Rezultate == null)
            {
                throw new Exception();
            }
            var insertOperation = TableOperation.Insert(rezultat);
            return await Rezultate.ExecuteAsync(insertOperation);
        }
        
        public async void DeleteRezultat(RezultatEntity rezultat)
        {
           // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<RezultatEntity>(rezultat.PartitionKey,rezultat.RowKey);

            // Execute the operation.
            TableResult retrievedResult = await Rezultate.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            RezultatEntity deleteEntity = (RezultatEntity)retrievedResult.Result;

            // Create the Delete TableOperation and then execute it.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                Console.WriteLine(rezultat.CandidatAles + "voted by CNP: " + rezultat.RowKey+ " was deleted.");
                // Execute the operation.
                 await Rezultate.ExecuteAsync(deleteOperation);
                
            }
            else
            {
                 Console.WriteLine("Couldn't delete the entity.");
            }
           
            
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

