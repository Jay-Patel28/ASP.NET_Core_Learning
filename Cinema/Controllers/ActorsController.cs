using Cinema.DTOs;
using Cinema.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ActorService actorService;
        public ActorsController(ActorService actorService)
        {
            this.actorService = actorService;
        }


        [HttpGet("/actors")]
        public List<ActorDTO> GetAllActors()
        {
            return actorService.GetAll();
        }

        [HttpGet("/actor/{id}")]
        public ActorDTO GetActorById(Guid id)
        {
            return actorService.GetActorById(id);
        }


        [HttpPost("/actor")]
        public ActorDTO AddActor([FromBody] ActorDTO actorDTO)
        {
            return actorService.AddActor(actorDTO);
        }

        [HttpGet("/wealthiest")]
        public List<ActorDTO> GetActorsByWealth()
        {
            return actorService.GetActorsByWealth();
        }

        [HttpDelete("/actor/{id}")]
        public ActorDTO DeleteActorById(Guid id)
        {
            return actorService.DeleteActorById(id);
        }
    }
}
