using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;
using System.Text;

namespace Web.Models
{
    public class Votant
    {
        [Required]
        [Display(Name = "Nume")]
        public string Nume { get; set; }

        [Required]
        [Display(Name = "Prenume")]
        public string Prenume { get; set; }

        [Required]
        [Display(Name = "CNP")]
        public string CNP { get; set; }

        [Required]
        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [Required]
        [Display(Name = "Judet")]
        public string Judet { get; set; }

        [Required]
        [Display(Name = "Localiate")]
        public string Localitate { get; set; }
        public void IsValid(string _nume,string _prenume,string _cnp,string _serie,string _judet,string _localitate)
        {
           
           string sURL;
           sURL = "https://alegeri.azurewebsites.net/api/vot";

            WebRequest wrPOSTURL;
            wrPOSTURL = WebRequest.Create(sURL);
            wrPOSTURL.ContentType = "application/json";
            wrPOSTURL.Method = "POST";

            using (var streamWriter = new StreamWriter(wrPOSTURL.GetRequestStream()))
            {
                string json = "{\"Nume\": \""+ _nume + "\","+"\"Prenume\": \""+_prenume+"\","+"\"Cnp\": \""+ _cnp + "\","+"\"Serie\": \""+ _serie+ "\","+"\"Judet\": \""+ _judet + "\","+"\"Localitate\": \""+_localitate+ "\"}"; 
                streamWriter.Write(json);
            }
            
            var httpResponse = (HttpWebResponse)wrPOSTURL.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
        


