using AutoMapper;
using Cinema.DTOs;
using Cinema.Managers;
using Cinema.Models;

namespace Cinema.Services
{
    public class ActorService
    {
        private readonly ActorManager actorManager;
        private readonly IMapper mapper;

        public ActorService(ActorManager actorManager, IMapper mapper)
        {
            this.actorManager = actorManager;
            this.mapper = mapper;
        }

        public List<ActorDTO> GetAll()
        {
            List<ActorModel> actorEntity = actorManager.GetAll();
            return mapper.Map<List<ActorDTO>>(actorEntity);
        }

        public ActorDTO GetActorById(Guid id) {
            ActorModel actorModel = actorManager.GetActorById(id);
            return mapper.Map<ActorDTO>(actorModel);
        }

        public List<ActorDTO> GetActorsByWealth()
        {
            return mapper.Map<List<ActorDTO>>(actorManager.GetActorsByWealth());
        }

        public ActorDTO DeleteActorById(Guid id)
        {
            ActorModel actorModel = actorManager.DeleteActorById(id);
            return mapper.Map<ActorDTO>(actorModel);
        }

        public ActorDTO AddActor(ActorDTO actorDTO)
        {
            ActorModel actorModel = mapper.Map<ActorModel>(actorDTO);
            return mapper.Map<ActorDTO>(actorManager.AddActor(actorModel));
        }
    }
}
