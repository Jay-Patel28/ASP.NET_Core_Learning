using AutoMapper;
using Cinema.DTOs;
using Cinema.Entities;
using Cinema.Managers;
using Cinema.Models;
using Cinema.Repositories;

namespace Cinema.Services
{
    public class MovieService
    {
        private readonly MovieManager movieManager;
        private readonly IMapper mapper;

        public MovieService(MovieManager movieManager, IMapper mapper)
        {
            this.movieManager = movieManager;
            this.mapper = mapper;
        }

        public List<MovieDTO> GetAll()
        {

            List<MovieModel> movieEntity = movieManager.GetAll();
            return mapper.Map<List<MovieDTO>>(movieEntity);
        }

        public MovieDTO GetMovieById(Guid id)
        {
            MovieModel movieModel = movieManager.GetMovieById(id);
            List <ActorModel> actorModels = movieModel.ActorModels;

            List<ActorDTO> actorDTOs = mapper.Map<List<ActorDTO>>(actorModels);

            MovieDTO movieDTO = mapper.Map<MovieDTO>(movieModel);
            movieDTO.ActorDTOs = actorDTOs;
            return movieDTO;
        }

        public List<MovieDTO> GetMoviesByViews()
        {
            return mapper.Map<List<MovieDTO>>(movieManager.GetMoviesByViews());
        }

        public MovieDTO AddMovie(MovieDTO movieDTO)
        {
            MovieModel movieModel = mapper.Map<MovieModel>(movieDTO);
            //if (movieModel.ReleaseDate == null) { movieModel.ReleaseDate = DateTime.Now; }
            List<ActorModel> actorModel = mapper.Map<List<ActorModel>>(movieDTO.ActorDTOs);
            movieModel.ActorModels = actorModel;
            return mapper.Map<MovieDTO>(movieManager.AddMovie(movieModel));
        }
        public MovieDTO DeleteMovieById(Guid id)
        {
            MovieModel movieModel = movieManager.DeleteMovieById(id);
            return mapper.Map<MovieDTO>(movieModel);
        }
    }
}
