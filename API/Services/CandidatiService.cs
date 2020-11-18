using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Services
{
    public class CandidatiService : IDisposable
    {
        private CloudTable Candidati;

        public CandidatiService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Candidati = tableClient.GetTableReference("Candidati");

        }

        public async Task Initialize()
        {
            await Candidati.CreateIfNotExistsAsync();
        }

        public async Task<List<CandidatiEntity>> GetCandidati()
        {
            if (Candidati == null)
            {
                throw new Exception();
            }
            
            var candidati = new List<CandidatiEntity>();
            TableQuery<CandidatiEntity> query = new TableQuery<CandidatiEntity>(); 
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<CandidatiEntity> resultSegment = await Candidati.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                candidati.AddRange(resultSegment.Results);
            } while (token != null);

            return candidati;
        }

        public async Task<TableResult> AddCandidat(CandidatiEntity candidat)
        {
            if (Candidati == null)
            {
                throw new Exception();
            }
	
            var insertOperation = TableOperation.Insert(candidat);
            return await Candidati.ExecuteAsync(insertOperation);
        }
           public async void DeleteCandidat(CandidatiEntity candidatiEntity)
        {
           // Create a retrieve operation that expects a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<CandidatiEntity>(candidatiEntity.PartitionKey,candidatiEntity.RowKey);

            // Execute the operation.
            TableResult retrievedResult = await Candidati.ExecuteAsync(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            RezultatEntity deleteEntity = (RezultatEntity)retrievedResult.Result;

            // Create the Delete TableOperation and then execute it.
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                Console.WriteLine(candidatiEntity.RowKey + "from  " + candidatiEntity.PartitionKey+ " was deleted.");
                // Execute the operation.
                 await Candidati.ExecuteAsync(deleteOperation);
                
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

