using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class VotantiModel
    {
        public string Nume { get; set; }

        public string Prenume { get; set; }

        public string Cnp { get; set; }

        public string Serie { get; set; }

        public string Judet { get; set; }

        public string Localitate { get; set; }
        public bool Votat { get; set; }
    }
}