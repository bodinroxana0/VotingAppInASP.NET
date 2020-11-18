using Microsoft.WindowsAzure.Storage.Table;

namespace Web.Models
{
    public class RaportEntity : TableEntity
    {
        public RaportEntity(string Nume, string Nr)
        {
            this.PartitionKey=Nume;
            this.RowKey=Nr;
        }
        public string Procent {get;set;}
        public RaportEntity(){}
        
    }
}