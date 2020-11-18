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
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if(user.IsValid(user.UserName, user.Password))
                {
                    return RedirectToAction("AddVotant", "Votant");
                }
                else
                {
                    ModelState.AddModelError("", "Autentificare esuata!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
        }
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
