using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class AdminEntity : TableEntity
    {
        public AdminEntity(string Nume, string Parola)
        {
            this.PartitionKey = Nume;
            this.RowKey = Parola;
        }

        public AdminEntity() { }
        

    }
}