using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Models;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        const string ServiceBusConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";
      //  const string QueueName = "queue-1";
        //static IQueueClient queueClient;

      // GET https://localhost:5001/api/admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminEntity>>> Get()
        {
            using (var service = new AdminService())
            {
                await service.Initialize();
                return await service.GetAdmin();
            }
        }

        // GET api/vot/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // Must be implemented
            return null;
        
        }
         // POST api/vot
        [HttpPost]
        public async Task Post([FromBody]AdminModel admin)
        {
            if (string.IsNullOrEmpty(admin.Nume) || string.IsNullOrEmpty(admin.Parola))
            {
                throw new Exception("Adminul nu are date suficiente!");
            }
            var adminEntity = new AdminEntity(admin.Nume, admin.Parola);

            using (var service = new AdminService())
            {
                await service.Initialize();
                await service.AddAdmin(adminEntity);
            }
        }
          
    }
}
        