using System.Linq.Expressions;
using AutoMapper;
using Cinema.DTOs;
using Cinema.Models;

namespace Cinema.Mapper
{
    public class CinemaMapper

    {
        public CinemaMapper()
        {
        MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
        {
            mc.CreateMap<ActorDTO, ActorModel>();
        });
        }
       
    }
}
