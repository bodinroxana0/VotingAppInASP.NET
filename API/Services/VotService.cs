using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Services
{
    public class VotService : IDisposable
    {
        private CloudTable Votanti;
        

        public VotService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Votanti = tableClient.GetTableReference("Votanti");

        }

        public async Task Initialize()
        {
            await Votanti.CreateIfNotExistsAsync();
        }

        public async Task<List<VotantiEntity>> GetVotanti()
        {
            if (Votanti == null)
            {
                throw new Exception();
            }
            
            var votanti = new List<VotantiEntity>();
            TableQuery<VotantiEntity> query = new TableQuery<VotantiEntity>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<VotantiEntity> resultSegment = await Votanti.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                votanti.AddRange(resultSegment.Results);
            } while (token != null);

            return votanti;
        }
        public async Task<List<VotantiEntity>> GetVotantiSerie(string serie)
        {
            if (Votanti == null)
            {
                throw new Exception();
            }
            
            var votanti = new List<VotantiEntity>();
            TableQuery<VotantiEntity> query = new TableQuery<VotantiEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, serie));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<VotantiEntity> resultSegment = await Votanti.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                votanti.AddRange(resultSegment.Results);
            } while (token != null);

            return votanti;
        }

        public async Task<TableResult> AddVotant(VotantiEntity votant)
        {
            if (Votanti == null)
            {
                throw new Exception();
            }
	
            var insertOperation = TableOperation.Insert(votant);
            return await Votanti.ExecuteAsync(insertOperation);
        }

        public async Task<TableResult> UpdateVotant(string cnp)
        {
    
        var votant=new List<VotantiEntity>();
        TableQuery<VotantiEntity> query = new TableQuery<VotantiEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, cnp));
        TableContinuationToken token = null;
            do
            {
                TableQuerySegment<VotantiEntity> resultSegment = await Votanti.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                votant.AddRange(resultSegment.Results);
            } while (token != null);

            var item=new VotantiEntity();
            item.PartitionKey = votant[0].PartitionKey;
            item.RowKey = cnp;
            item.Nume=votant[0].Nume;
            item.Prenume=votant[0].Prenume;
            item.Judet=votant[0].Judet;
            item.Localitate=votant[0].Localitate;
            item.Votat=true;
            item.ETag = "*";

        var operation = TableOperation.Replace(item);
        return await Votanti.ExecuteAsync(operation);

        }
        
        public async void DeleteVotant(VotantiEntity votant)
        {
           // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<RezultatEntity>(votant.PartitionKey,votant.RowKey);

            // Execute the operation.
            TableResult retrievedResult = await Votanti.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            RezultatEntity deleteEntity = (RezultatEntity)retrievedResult.Result;

            // Create the Delete TableOperation and then execute it.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                Console.WriteLine(votant.Nume + " with CNP: " + votant.RowKey+ " was deleted.");
                // Execute the operation.
                 await Votanti.ExecuteAsync(deleteOperation);
                
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
