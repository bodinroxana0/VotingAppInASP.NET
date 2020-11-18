using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using Models;


namespace WebJob2
{
    class Program
    {
        private static CloudTable Rezultate;
        private static CloudTable Raport;
        static string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";

        static void Main(string[] args)
        {
            var account = CloudStorageAccount.Parse(storageConnectionString);
            var tableClient = account.CreateCloudTableClient();
            
            Rezultate = tableClient.GetTableReference("Rezultate");
            //creare tabel raport 
            Raport=tableClient.GetTableReference("RaportVoturi");
             Task.Run(async() =>
                {
                    await Initialize();
                    if (Raport== null)
            {
                throw new Exception();
            }
            ///sterge toate inregistrarile din raport
            List<RaportEntity> raport=await GetRaport();
            foreach(RaportEntity r in raport){
            TableOperation retrieveOperation = TableOperation.Retrieve<RaportEntity>(r.PartitionKey,r.RowKey);
            TableResult retrievedResult = await Raport.ExecuteAsync(retrieveOperation);
            RaportEntity deleteEntity = (RaportEntity)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                 await Raport.ExecuteAsync(deleteOperation);
            }
            }

            //construieste lista de inregistrari cu fiecare candidat+nr voturi
            List<RaportEntity> raport2=await GetVoturi(GetCandidati());
            
            //insereaza inregistrarile in tabela raport
            foreach(RaportEntity r in raport2) 
            {    
                var insertOperation = TableOperation.Insert(r);
            
                await Raport.ExecuteAsync(insertOperation);
                
            }
            await GetRaport(); 
                })
                .GetAwaiter()
                .GetResult();
            
          
            
        }
        

        public static async Task Initialize()
        {
            await Raport.CreateIfNotExistsAsync();
        }
        public static async Task<List<RaportEntity>> GetRaport()
        {
            if (Raport == null)
            {
                throw new Exception();
            }
            var raportlist = new List<RaportEntity>();
            TableQuery<RaportEntity> query = new TableQuery<RaportEntity>();//.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, candidat.RowKey));
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<RaportEntity> resultSegment = await Raport.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                raportlist.AddRange(resultSegment.Results);
            } while (token != null);
            return raportlist;
        }
  
        public static async Task<List<RaportEntity>> GetVoturi(List<CandidatiEntity> candiati)
        {
            if (Rezultate == null)
            {
                throw new Exception();
            }
            //////preluam totalul de voturi
            List<RezultatEntity> voturitotal = new List<RezultatEntity>();
            TableQuery<RezultatEntity> query1 = new TableQuery<RezultatEntity>();//.Where(TableQuery.GenerateFilterCondition("Votat", QueryComparisons.Equal,"true"));
            TableContinuationToken token1 = null;
                do
                {
                    TableQuerySegment<RezultatEntity> resultSegment = await Rezultate.ExecuteQuerySegmentedAsync(query1, token1);
                    token1 = resultSegment.ContinuationToken;
                    voturitotal.AddRange(resultSegment.Results);
                } while (token1 != null);
            
            int total= voturitotal.Count;


            List<RaportEntity> raport = new List<RaportEntity>();

            //select pe tabela rezultat pentru fiecare candidat=> nr voturi /candidat
            foreach(CandidatiEntity candidat in candiati)
            {
                
                TableQuery<RezultatEntity> query = new TableQuery<RezultatEntity>().Where(TableQuery.GenerateFilterCondition("CandidatAles", QueryComparisons.Equal, candidat.RowKey));

                TableContinuationToken token = null;
                do
                {
                    TableQuerySegment<RezultatEntity> resultSegment = await Rezultate.ExecuteQuerySegmentedAsync(query, token);
                    token = resultSegment.ContinuationToken;
                    //se declara o inregistrare de tip raport pt candidatul gasit si se initializeaza
                    RaportEntity r=new RaportEntity(candidat.RowKey,""+resultSegment.Results.Count);
                    r.Procent=((float)resultSegment.Results.Count/total)*100+"%";
                    raport.Add(r);
                } while (token != null);
            }
            return raport;
        }
        public static List<CandidatiEntity> GetCandidati()
        {
             List<CandidatiEntity> candidatiList=new List<CandidatiEntity>();
            string sURL;
            sURL = "https://alegeri.azurewebsites.net/api/candidat";

            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            while (sLine!=null)
            {
            i++;
            sLine = objReader.ReadLine();
                if (sLine!=null)
                {
                    candidatiList= JsonConvert.DeserializeObject<List<CandidatiEntity>>(sLine);
                }
            }
            return candidatiList;
        }
        // public static async void AddRaport()//numar voturi
        // {
            
        // }
        // public static async void DeleteRaport()
        // {
        //     List<RaportEntity> raport=await GetRaport();
        //     foreach(RaportEntity r in raport){
        //    // Create a retrieve operation that expects a customer entity.
        //     TableOperation retrieveOperation = TableOperation.Retrieve<RaportEntity>(r.PartitionKey,r.RowKey);

        //     // Execute the operation.
        //     TableResult retrievedResult = await Raport.ExecuteAsync(retrieveOperation);

        //     // Assign the result to a CustomerEntity object.
        //     RaportEntity deleteEntity = (RaportEntity)retrievedResult.Result;

        //     // Create the Delete TableOperation and then execute it.
        //     if (deleteEntity != null)
        //     {
        //         TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
        //          await Raport.ExecuteAsync(deleteOperation);
        //     }
        //     }
        // }
        // public async void UpdateRaport(string candidatAles)
        // {
        //    // List<CandidatiEntity> candidati=GetCandidati();
        //    // foreach(CandidatiEntity c in candidati) {
        //     var raport=new List<RaportEntity>();
        //     TableQuery<RaportEntity> query = new TableQuery<RaportEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, candidatAles));
        //     TableContinuationToken token = null;
        //         do
        //         {
        //             TableQuerySegment<RaportEntity> resultSegment = await Raport.ExecuteQuerySegmentedAsync(query, token);
        //             token = resultSegment.ContinuationToken;
        //             raport.AddRange(resultSegment.Results);
        //         } while (token != null);

        //     var item=new RaportEntity();
        //     item.PartitionKey = raport[0].PartitionKey;
        //     int nr=Convert.ToInt16(raport[0].RowKey);
        //     item.RowKey =""+nr;
        //     item.ETag = "*";

        //     var operation = TableOperation.Replace(item);
        //     await Raport.ExecuteAsync(operation);
            
        // }

    }
}
