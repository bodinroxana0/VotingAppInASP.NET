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
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;
using System.IO;
using Json.Net;
using Newtonsoft.Json;
using System.Text;


namespace Web.Controllers
{
    public class StatisticiController : Controller
    {
        private readonly ILogger<StatisticiController> _logger;

        public StatisticiController(ILogger<StatisticiController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Statistici(Models.Result result)
        {
            ViewBag.HtmlStr=result.GetResults();
            return View();
        }
        [HttpGet]
        public IActionResult StatisticiJudet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StatisticiJudet(Models.Result result)
        {
            ViewBag.HtmlStr=result.GetResultsJudet(result.Judet);
            return View();
            
        }
        [HttpGet]
        public IActionResult Chart(Models.Raport raport)
        {
            return View(raport.GetRaport());
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
