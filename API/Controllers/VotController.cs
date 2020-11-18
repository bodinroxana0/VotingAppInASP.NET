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
    public class VotController : ControllerBase
    {
        const string ServiceBusConnectionString = "DefaultEndpointsProtocol=https;AccountName=aplicatievotare;AccountKey=PuNXi3cZVkDqLqMhgAZMTmGs5jaF82HLSv2Me/47TkcJwqjvVL7A+WyzqPIDemIQekJGkcwDo11WN8VosMBDag==;EndpointSuffix=core.windows.net";
      //  const string QueueName = "queue-1";
        //static IQueueClient queueClient;

      // GET https://localhost:5001/api/vot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VotantiEntity>>> Get()
        {
            using (var service = new VotService())
            {
                await service.Initialize();
                return await service.GetVotanti();
            }
        }

        // GET api/vot/id
        [HttpGet("{serie}")]
        public async Task<ActionResult<IEnumerable<VotantiEntity>>> Get(string serie)
        {
            using (var service = new VotService())
            {
                await service.Initialize();
                return await service.GetVotantiSerie(serie);
            }
        }

        // POST api/vot
        [HttpPost]
        public async Task Post([FromBody] VotantiModel votant)
        {
            if (string.IsNullOrEmpty(votant.Serie) || string.IsNullOrEmpty(votant.Cnp) || string.IsNullOrEmpty(votant.Prenume) || string.IsNullOrEmpty(votant.Nume) || string.IsNullOrEmpty(votant.Localitate) || string.IsNullOrEmpty(votant.Judet))
            {
                throw new Exception("Votantul nu are date suficiente!");
            }
            var votantiEntity = new VotantiEntity(votant.Serie, votant.Cnp)
            {
                Nume = votant.Nume,
                Prenume=votant.Prenume,
                Judet=votant.Judet,
                Localitate=votant.Localitate,
                Votat=votant.Votat
            };
            
            //queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            // string json = JsonConvert.SerializeObject(student);
            // var message = new Message(Encoding.UTF8.GetBytes(json));
            // Console.WriteLine($"Sending message: {json}");
            //  // Send the message to the queue.
            // await queueClient.SendAsync(message);

            using (var service = new VotService())
            {
                await service.Initialize();
                await service.AddVotant(votantiEntity);
            }
        }
        
        // DELETE api/students/5
        [HttpDelete]
        public async Task Delete([FromBody]VotantiEntity votantiEntity)
        {
            using (var service = new VotService())
            {
                service.DeleteVotant(votantiEntity);
            }
        }        
    }
}
        // // PUT api/students/5
        // [HttpPut("{id}")]
        // public async Task Put(int id, [FromBody] string value)
        // {
            
        // }