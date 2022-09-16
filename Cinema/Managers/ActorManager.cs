using Cinema.Controllers;
using Cinema.Entities;
using Cinema.Models;
using Cinema.Repositories;
using AutoMapper;
using System.Collections.Generic;
using Cinema.Repositories.Impl;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Caching.Memory;
using Cinema.Caching;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Cinema.Exceptions;

namespace Cinema.Managers
{
    public class ActorManager
    {
        private readonly IActorRepository actorRepository;
        private readonly IMapper mapper;
        private readonly ICaching inMemoryCache;
        private readonly string ALL_ACTORS = "ALL_ACTORS";
        private readonly string ACTOR = "ACTOR";
        public ActorManager(IActorRepository actorRepository, IMapper mapper, ICaching memoryCache)
        {
            this.actorRepository = actorRepository;
            this.mapper = mapper;
            this.inMemoryCache = memoryCache;
        }

        public List<ActorModel> GetAll()
        {
            string? ALL = inMemoryCache.Get(ALL_ACTORS);
            if (ALL == null)
            {
                List<ActorEntity> actorEntity = actorRepository.GetAll();
                inMemoryCache.Set(ALL_ACTORS, actorEntity);
                return mapper.Map<List<ActorModel>>(actorEntity);
            }
            List<ActorModel>? Actors = JsonConvert.DeserializeObject<List<ActorModel>>(ALL);
            return Actors;
        }

        public ActorModel GetActorById(Guid id)
        {
            string? ACTOR_BY_ID = inMemoryCache.Get(ACTOR + id.ToString());
            if (ACTOR_BY_ID == null)
            {
                ActorEntity actorEntity = actorRepository.GetActorById(id);
                if (actorEntity == null) {
                    throw new BadRequestException("Acotr.not.found", string.Format("Did not find any Actor with id {0}", id.ToString()));
                }
                inMemoryCache.Set(ACTOR + id.ToString(), actorEntity);
                return mapper.Map<ActorModel>(actorEntity);
            }
            ActorModel? Actor = JsonConvert.DeserializeObject<ActorModel>(ACTOR_BY_ID);
            return Actor;
        }

        public List<ActorModel> GetActorsByWealth()
        {
            return mapper.Map<List<ActorModel>>(actorRepository.GetActorsByWealth());
        }

        public ActorModel DeleteActorById(Guid id)
        {
            inMemoryCache.Remove(ACTOR+id.ToString());
            ActorModel actorModel = mapper.Map<ActorModel>(actorRepository.DeleteActorById(id));
            if (actorModel == null)
            {
                throw new BadRequestException("Acotr.not.found", string.Format("Did not find any Actor with id {0}", id.ToString()));
            }
            return actorModel;

        }

        public ActorModel AddActor(ActorModel actorModel)
        {
            inMemoryCache.Remove(ALL_ACTORS);
            ActorEntity actorEntity = mapper.Map<ActorEntity>(actorModel);
            return mapper.Map<ActorModel>(actorRepository.AddActor(actorEntity));
        }
    }
}
