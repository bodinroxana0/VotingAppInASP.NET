using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class CandidatiEntity : TableEntity
    {
        public CandidatiEntity(string Partid, string NumePrenume)
        {
            this.PartitionKey = Partid;
            this.RowKey = NumePrenume;
        }
        public string PartidSigla { get; set; }
        public CandidatiEntity() { }

    }
}