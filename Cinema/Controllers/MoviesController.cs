using Cinema.DTOs;
using Cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class MoviesController
    {
        private readonly MovieService movieService;

        public MoviesController(MovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet("/movies")]
        public List<MovieDTO> getAllMovies()
        {
            return movieService.GetAll();
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

    }
}
