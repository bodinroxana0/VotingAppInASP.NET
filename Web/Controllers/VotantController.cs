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
    public class VotantController : Controller
    {
        private readonly ILogger<VotantController> _logger;

        public VotantController(ILogger<VotantController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult AddVotant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddVotant(Models.Votant votant)
        {
           
              if (ModelState.IsValid)
            {
                votant.IsValid(votant.Nume,votant.Prenume,votant.CNP,votant.Serie,votant.Judet,votant.Localitate);
    
            }
            return View(votant);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
