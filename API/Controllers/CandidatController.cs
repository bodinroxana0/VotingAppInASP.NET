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
    public class CandidatController : ControllerBase
    {
        const string ServiceBusConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";
      //  const string QueueName = "queue-1";
        //static IQueueClient queueClient;

      // GET https://localhost:5001/api/candidat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidatiEntity>>> Get()
        {
            using (var service = new CandidatiService())
            {
                await service.Initialize();
                return await service.GetCandidati();
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
        public async Task Post([FromBody] CandidatiModel candidat)
        {
            if (string.IsNullOrEmpty(candidat.PartidSigla) || string.IsNullOrEmpty(candidat.Partid) || string.IsNullOrEmpty(candidat.NumePrenume))
            {
                throw new Exception("Candidatul nu are date suficiente!");
            }
            var candidatEntity = new CandidatiEntity(candidat.Partid, candidat.NumePrenume)
            {
                PartidSigla = candidat.PartidSigla
            };

            using (var service = new CandidatiService())
            {
                await service.Initialize();
                await service.AddCandidat(candidatEntity);
            }
        }
        
        // DELETE api/students/5
        [HttpDelete]
        public async Task Delete([FromBody]CandidatiEntity candidatiEntity)
        {
            using (var service = new CandidatiService())
            {
                service.DeleteCandidat(candidatiEntity);
            }
        }        
    }
}