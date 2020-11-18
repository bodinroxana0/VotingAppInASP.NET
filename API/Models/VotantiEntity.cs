using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class VotantiEntity : TableEntity
    {
        public VotantiEntity(string serie, string cnp)
        {
            this.PartitionKey = serie;
            this.RowKey = cnp;
        }

        public VotantiEntity() { }

        public string Nume { get; set; }
        

        public string Prenume { get; set; }

        public string Judet { get; set; }

        public string Localitate { get; set; }

        public bool Votat  { get; set; }
        
    }
}