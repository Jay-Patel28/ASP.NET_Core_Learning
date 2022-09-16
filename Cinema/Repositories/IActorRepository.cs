using Cinema.Entities;
using Cinema.Models;

namespace Cinema.Repositories
{
    public interface IActorRepository
    {
        public List<ActorEntity> GetAll();

        public ActorEntity GetActorById(Guid id);
        public ActorEntity AddActor(ActorEntity actorEntity);
        public ActorEntity DeleteActorById(Guid id);
        public List<ActorEntity> GetActorsByWealth();

    }
}
