using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories.Impl
{
    public class MovieRepository : IMovieRepository
    {

        private readonly CinemaContext cinemaContext;

        public MovieRepository(CinemaContext cinemaContext)
        {
            this.cinemaContext = cinemaContext;
        }

        public List<MovieEntity> GetAll()
        {
            return cinemaContext.Movies.ToList();
        }

        public MovieEntity GetMovieById(Guid id)
        {
            //return cinemaContext.Movies.Where(movie => movie.Id == id).FirstOrDefault();
            return cinemaContext.Movies.Where(movie => movie.Id == id)
                    .Include(movie => movie.ActorMovies)
                    .ThenInclude(movieActor => movieActor.ActorEntity)
                    .FirstOrDefault();
        }

        //public List<ActorEntity> GetaActorsByMovieId(Guid movieId)
        //{
        //    List<ActorMovieEntity> as = cinemaContext.ActorsOfMovie.Where(acm => movieId == acm.RelatedMovieId);
        //    return List<ActorEntity>;
        //}
        public List<MovieEntity> GetMoviesByViews()
        {
            return cinemaContext.Movies.OrderByDescending(movie => movie.TotalViews).ToList();
        }


        public MovieEntity AddMovie(MovieEntity movieEntity)
        {
            cinemaContext.Movies.Add(movieEntity);
            return movieEntity;
        }

        public MovieEntity DeleteMovieById(Guid id)
        {
            MovieEntity movie = cinemaContext.Movies.Find(id);

            //Add an Exception Here
            if (movie == null) return null;


            cinemaContext.Movies.Remove(movie);
            cinemaContext.SaveChanges();
            return movie;
        }


        public void AddActorToMovie(ActorMovieEntity actorMovieEntity)
        {
            cinemaContext.ActorsOfMovie.Add(actorMovieEntity);
        }

        public void Save()
        {
            cinemaContext.SaveChanges();
        }

    }
}
