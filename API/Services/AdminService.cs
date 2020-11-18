using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Services
{
    public class AdminService : IDisposable
    {
        private CloudTable Admin;
        

        public AdminService()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();

            Admin = tableClient.GetTableReference("Admin");

        }

        public async Task Initialize()
        {
            await Admin.CreateIfNotExistsAsync();
        }

        public async Task<List<AdminEntity>> GetAdmin()
        {
            if (Admin == null)
            {
                throw new Exception();
            }
            
            var admin = new List<AdminEntity>();
            TableQuery<AdminEntity> query = new TableQuery<AdminEntity>();//Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, cnp));

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<AdminEntity> resultSegment = await Admin.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                admin.AddRange(resultSegment.Results);
            } while (token != null);

            return admin;
        }
        public async Task<TableResult> AddAdmin(AdminEntity admin)
        {
            if (Admin == null)
            {
                throw new Exception();
            }
	
            var insertOperation = TableOperation.Insert(admin);
            return await Admin.ExecuteAsync(insertOperation);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
