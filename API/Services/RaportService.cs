using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Services
{
    public class RaportService : IDisposable
    {
        private  CloudTable Raport;
        private   CloudTable Rezultate;

        public RaportService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";
            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Rezultate = tableClient.GetTableReference("Rezultate");
            //creare tabel raport 
            Raport=tableClient.GetTableReference("RaportVoturi");
        }
        
        public async Task Initialize()
        {
            await Raport.CreateIfNotExistsAsync();
        }
        public async Task<List<RaportEntity>> GetRaport()
        {
            if (Raport == null)
            {
                throw new Exception();
            }
            
            var raport = new List<RaportEntity>();
            TableQuery<RaportEntity> query = new TableQuery<RaportEntity>(); //.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<RaportEntity> resultSegment = await Raport.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                raport.AddRange(resultSegment.Results);
            } while (token != null);

            return raport;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
