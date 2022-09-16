using Cinema.Entities;
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

        public void Save();
        //public ActorEntity DeleteActorById(Guid id);
    }
}
