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
    public class RezultateController : ControllerBase
    {
        const string ServiceBusConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";
      //  const string QueueName = "queue-1";
        //static IQueueClient queueClient;

      // GET https://localhost:5001/api/rezultate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RezultatEntity>>> Get()
        {
            using (var service = new RezultateService())
            {
                await service.Initialize();
                return await service.GetRezultat();
            }
        }
         // GET api/rezultate/BH
        [HttpGet("{judet}")]
        public async Task<ActionResult<IEnumerable<RezultatEntity>>> Get(string judet)
        {
             using (var service = new RezultateService())
            {
                await service.Initialize();
                return await service.GetRezultatJudet(judet);
            }
        }
        // POST api/vot
        [HttpPost]
        public async Task Post([FromBody]RezultatModel rezultat)
        {
            if (string.IsNullOrEmpty(rezultat.Serie) || string.IsNullOrEmpty(rezultat.CandidatAles) || string.IsNullOrEmpty(rezultat.Cnp) || string.IsNullOrEmpty(rezultat.Judet))
            {
                throw new Exception("Rezultatul nu e valid!");
            }
            var rezultatEntity = new RezultatEntity(rezultat.Serie, rezultat.Cnp)
            {
                CandidatAles=rezultat.CandidatAles,
                Judet = rezultat.Judet,
            };

            using (var service = new RezultateService())
            {
                await service.Initialize();
                await service.AddRezultat(rezultatEntity); 
            }
            using (var service = new VotService())
            {
                await service.UpdateVotant(rezultat.Cnp);
            }
        }
        
        // DELETE api/students/5
        [HttpDelete]
        public async Task Delete([FromBody]RezultatEntity rezultatEntity)
        {
            using (var service = new RezultateService())
            {
                service.DeleteRezultat(rezultatEntity);
            }
        }        
    }
}