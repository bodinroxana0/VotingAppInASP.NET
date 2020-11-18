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
using System.Web.Helpers;


namespace Web.Models
{
    public class Raport
    {
        public List<RaportEntity> GetRaport()
        {
            List<RaportEntity> raportList=new List<RaportEntity>();
           string sURL;
           sURL = "https://alegeri.azurewebsites.net/api/raportvoturi";

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
                    raportList= JsonConvert.DeserializeObject<List<RaportEntity>>(sLine);
                }
            }
            return raportList;
        }
    }
}