using Cinema.Entities;
using Cinema.Managers;
using Cinema.Models;

namespace Cinema.Repositories
{
    public interface IMovieRepository
    {
        public List<MovieEntity> GetAll();

        public MovieEntity GetMovieById(Guid id);
        public MovieEntity AddMovie(MovieEntity movieEntity);
        //public List<ActorEntity> GetaActorsByMovieId(Guid movieId);

        public void AddActorToMovie(ActorMovieEntity actorMovieEntity);
        public List<MovieEntity> GetMoviesByViews();
        public MovieEntity DeleteMovieById(Guid id);
        public int GetActorsCount(Guid id);

        public void RemoveActorFromMovie(Guid movieId, Guid actorId);

        public void Save();
        //public ActorEntity DeleteActorById(Guid id);
    }
}
