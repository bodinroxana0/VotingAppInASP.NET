using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.WindowsAzure.Storage;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;
using System.Text;

namespace Web.Models
{
    public class Result
    {
        [Required]
        [Display(Name = "Judet")]
        public string Judet { get; set; }
 
        public string GetResults()
        {
            List<ResultEntity> resultList=new List<ResultEntity>();
            string sURL;
            sURL = "https://alegeri.azurewebsites.net/api/rezultate/";

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
                    resultList= JsonConvert.DeserializeObject<List<ResultEntity>>(sLine);
                }
            }
            var result="<table style=\"width:300px\"><tr><th>Candidat Ales</th><th>Cnp Votant</th></tr>";
            foreach(ResultEntity r in resultList)
            {
                result+="<tr><td>"+r.CandidatAles+"</td><td>"+r.RowKey+"</td></tr>";
            }
            result+="</table>";
            return result;
        }
         public string GetResultsJudet(string _judet)
        {
           List<ResultEntity> resultList=new List<ResultEntity>();
           string sURL;
           sURL = "https://alegeri.azurewebsites.net/api/rezultate/"+_judet;

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
                    resultList= JsonConvert.DeserializeObject<List<ResultEntity>>(sLine);
                }
            }
            var result="<table style=\"width:300px\"><tr><th>Candidat Ales</th><th>Cnp Votant</th></tr>";
            foreach(ResultEntity r in resultList)
            {
                result+="<tr><td>"+r.CandidatAles+"</td><td>"+r.RowKey+"</td></tr>";
            }
            result+="</table>";
            return result;
        }

    }
}