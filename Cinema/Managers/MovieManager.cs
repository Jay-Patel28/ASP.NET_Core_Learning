using AutoMapper;
using Cinema.Caching;
using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Exceptions;
using Cinema.Models;
using Cinema.Repositories;
using Cinema.Repositories.Impl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Cinema.Managers
{
    public class MovieManager
    {

        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;
        private readonly ICaching inMemoryCache;
        private readonly string MOVIE = "MOVIE_";
        private readonly string ALL_MOVIES = "ALL_MOVIES";
        public MovieManager(IMovieRepository movieRepository, IMapper mapper, ICaching inMemoryCache)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
            this.inMemoryCache = inMemoryCache;
        }

        public List<MovieModel> GetAll()
        {
            string? ALL = inMemoryCache.Get(ALL_MOVIES);

            if (ALL == null) {
                List<MovieEntity> movieEntity = movieRepository.GetAll();
                inMemoryCache.Set(ALL_MOVIES, movieEntity);
                return mapper.Map<List<MovieModel>>(movieEntity);
            }
            List<MovieModel>? Movies = JsonConvert.DeserializeObject<List<MovieModel>>(ALL);
            return Movies;    
        }

        public MovieModel GetMovieById(Guid id)
        {
            string? MOVIE_BY_ID = inMemoryCache.Get(MOVIE + id.ToString());
            if (MOVIE_BY_ID == null)
            {
                MovieEntity movieEntity = movieRepository.GetMovieById(id);
                if (movieEntity == null)
                {
                    throw new BadRequestException("Movie.not.found", string.Format("Did not find any Movie with id {0}", id.ToString()));
                }
                MovieModel movieModel = mapper.Map<MovieModel>(movieEntity);
                movieModel.ActorModels = new List<ActorModel>();

                foreach(ActorMovieEntity actorMovieEntity in movieEntity.ActorMovies)
                {
                    movieModel.ActorModels.Add(mapper.Map<ActorModel>(actorMovieEntity.ActorEntity));
                }

                inMemoryCache.Set(MOVIE + id.ToString(), movieModel);
                return (movieModel);
            }
            MovieModel? Movie = JsonConvert.DeserializeObject<MovieModel>(MOVIE_BY_ID);
            return Movie;
        }


        public MovieModel DeleteMovieById(Guid id)
        {
            inMemoryCache.Remove(MOVIE + id.ToString());
            MovieModel movieModel = mapper.Map<MovieModel>(movieRepository.DeleteMovieById(id));
            if (movieModel == null)
            {
                throw new BadRequestException("Movie.not.found", string.Format("Did not find any Movie with id {0}", id.ToString()));
            }
            return movieModel;
        }
        //public List<ActorEntity> GetActorsByMovieId(Guid movieId)
        //{
        //    List<ActorMovie> actors = movieRepository.GetaActorsByMovieId(Guid movieId);
        //}

        public List<MovieModel> GetMoviesByViews()
        {
            return mapper.Map<List<MovieModel>>(movieRepository.GetMoviesByViews());
        }

        public MovieModel AddMovie(MovieModel movieModel)
        {
            inMemoryCache.Remove(ALL_MOVIES);
            MovieEntity movieEntity = mapper.Map<MovieEntity>(movieModel);
            movieEntity.CreatedDate = DateTime.Now;
            MovieEntity movie = movieRepository.AddMovie(movieEntity);
            Guid NewMovieid = movie.Id;

            foreach(ActorModel actorModel in movieModel.ActorModels)
            {
                ActorMovieEntity ame = new ActorMovieEntity();
                (ame.RelatedMovieId, ame.RelatedActorId) = (NewMovieid, actorModel.ActorId);
                movieRepository.AddActorToMovie(ame);
            }

            movieRepository.Save();
            return mapper.Map<MovieModel>(movie);
        }
    }
}
