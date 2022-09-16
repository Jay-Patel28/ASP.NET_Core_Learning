using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories.Impl
{
    public class ActorRepository:IActorRepository
    {

        private readonly CinemaContext cinemaContext;

        public ActorRepository(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public List<ActorEntity> GetAll()
        {
            return cinemaContext.Actors.ToList();
        }

        public ActorEntity GetActorById(Guid id)
        {
            return cinemaContext.Actors.Where(actor => actor.Id == id).FirstOrDefault();
        }

        public List<ActorEntity> GetActorsByWealth()
        {
            return cinemaContext.Actors.OrderByDescending(actor => actor.wealth).ToList();
        }


        public ActorEntity DeleteActorById(Guid id)
        {
            ActorEntity actor = cinemaContext.Actors.Find(id);

            //Add an Exception Here
            if (actor == null) return null;
            
            
            cinemaContext.Actors.Remove(actor);
            cinemaContext.SaveChanges();
            return actor;
        }

        public ActorEntity AddActor(ActorEntity actorEntity)
        {
            if(actorEntity.ActorMovies != null)
            {
                //actorEntity.ActorMovies.
            }
            actorEntity.CreatedDate = DateTime.Now;
            cinemaContext.Actors.Add(actorEntity);
            cinemaContext.SaveChanges();
            return actorEntity;
        }

    }
}
