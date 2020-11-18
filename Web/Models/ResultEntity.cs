using Microsoft.WindowsAzure.Storage.Table;

namespace Web.Models
{
    public class ResultEntity : TableEntity
    {
        
        public string CandidatAles{ get; set; }
        
        public string Cnp { get; set; }
        
        public ResultEntity() { }
        

    }
}