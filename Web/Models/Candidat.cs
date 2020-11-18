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
    public class Candidat
    {
        //Atentie: partidsigla trebuie sa fie un nume cu extensie, nu o cale intreaga , altfel va da 400 bad request
        [Required]
        [Display(Name = "Partid")]
        public string Partid { get; set; }

        [Required]
        [Display(Name = "NumePrenume")]
        public string NumePrenume { get; set; }

         [Required]
        [Display(Name = "PartidSigla")]
        public string PartidSigla { get; set; }

       
        public void IsValid(string _partid,string _numeprenume,string _partidsigla)
        {
           
           string sURL;
           sURL = "https://alegeri.azurewebsites.net/api/candidat";

            WebRequest wrPOSTURL;
            wrPOSTURL = WebRequest.Create(sURL);
            wrPOSTURL.ContentType = "application/json";
            wrPOSTURL.Method = "POST";

            using (var streamWriter = new StreamWriter(wrPOSTURL.GetRequestStream()))
            {
                string json = "{\"Partid\": \""+ _partid + "\","+"\"NumePrenume\": \""+_numeprenume+ "\","+"\"PartidSigla\": \""+ _partidsigla + "\"}"; 
                wrPOSTURL.ContentLength = json.Length;
                streamWriter.Write(json);
            }
            
            try
            {
                var httpResponse = (HttpWebResponse)wrPOSTURL.GetResponse();
            
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            }
            catch{
                Console.WriteLine("Error");
            }
        }
    }
}
        


