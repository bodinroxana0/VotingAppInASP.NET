using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class RezultatEntity : TableEntity
    {
        public RezultatEntity(string serie, string cnp)
        {
            this.PartitionKey = serie;
            this.RowKey = cnp;
        }
        
        public string CandidatAles { get; set; }
        public string Judet { get; set; }
        public RezultatEntity() { }

    }
}