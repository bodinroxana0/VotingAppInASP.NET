using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using System.Web;

namespace Web.Controllers
{
    public class CandidatController : Controller
    {
        private readonly ILogger<CandidatController> _logger;

        public CandidatController(ILogger<CandidatController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult AddCandidat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCandidat(Models.Candidat candidat)
        {
           
            if (ModelState.IsValid)
            {
                candidat.IsValid(candidat.Partid,candidat.NumePrenume,candidat.PartidSigla);
    
            }
            return View(candidat);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
