using AutoMapper;
using Cinema.Authentication;
using Cinema.DataContext;
using Cinema.DTOs;
using Cinema.Entities;
using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class MoviesController
    {
        private readonly MovieService movieService;
        private readonly CinemaContext cinemaContext;
        private readonly IMapper mapper;

        public MoviesController(MovieService movieService, CinemaContext cinemaContext, IMapper mapper)
        {
            this.cinemaContext = cinemaContext;
            this.movieService = movieService;
            this.mapper = mapper;
        }

        [HttpGet("/movies")]
        public List<MovieDTO> getAllMovies()
        {
            return movieService.GetAll();
        }

        [HttpGet("/movie/q/{search}")]
        public List<MovieDTO> GetMovieByName(string search)
        {
            List<MovieEntity> result = cinemaContext.Movies.Where(movie => movie.MovieName.ToLower().Contains(search.ToLower())).ToList();
            List<MovieModel> resultModel = mapper.Map<List<MovieModel>>(result);
            return mapper.Map<List<MovieDTO>>(resultModel);
        }


        [HttpGet("/movie/{id}")]
        public MovieDTO GetMovieById(Guid id)
        {
            MovieDTO result = movieService.GetMovieById(id);
            return result;
        }

        [HttpPost("/movie")]
        public MovieDTO AddMovie([FromBody] MovieDTO movieDTO)
        {
            return movieService.AddMovie(movieDTO);
        }

        [HttpGet("/mostViews")]
        public List<MovieDTO> GetMoviesByViews()
        {
            return movieService.GetMoviesByViews();
        }

        //[Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("/movie/{id}")]
        public MovieDTO DeleteMovieById(Guid id)
        {
            return movieService.DeleteMovieById(id);
        }

        [HttpDelete("/movie/{movieId}/actor/{actorId}")]
        public void RemoveActorFromMovie(Guid movieId, Guid actorId)
        {
            movieService.RemoveActorFromMovie( movieId,  actorId);
        }

        [HttpPost("/movie/{movieId}/actor/{actorId}")]
        public void AddActorToMovie(Guid movieId, Guid actorId)
        {
            movieService.AddActorToMovie(movieId, actorId);
        }

    }
}
