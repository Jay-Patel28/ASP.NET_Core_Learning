using AutoMapper;
using Cinema.Caching;
using Cinema.Controllers;
using Cinema.DataContext;
using Cinema.DTOs;
using Cinema.Entities;
using Cinema.Exceptions;
using Cinema.Managers;
using Cinema.Mapper;
using Cinema.Models;
using Cinema.Repositories;
using Cinema.Repositories.Impl;
using Cinema.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CinemaContext>(options => options.UseSqlServer(dbConnectionString));

MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<ActorModel, ActorEntity>();
    mc.CreateMap<ActorEntity, ActorModel>();

    mc.CreateMap<ActorModel, ActorDTO>();
    mc.CreateMap<ActorDTO, ActorModel>();

    mc.CreateMap<MovieModel, MovieEntity>();
    mc.CreateMap<MovieEntity, MovieModel>();

    mc.CreateMap<MovieDTO, MovieModel>();
    mc.CreateMap<MovieModel, MovieDTO>();


    mc.CreateMap<MovieDTO, MovieModel>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IActorRepository,ActorRepository>();
builder.Services.AddScoped<IMovieRepository,MovieRepository>();

builder.Services.AddScoped<ActorManager>();
builder.Services.AddScoped<MovieManager>();

builder.Services.AddScoped<ActorService>();
builder.Services.AddScoped<MovieService>();

//builder.Services.AddMemoryCache();
//builder.Services.AddSingleton<ICaching, InMemoryCache>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("CinemaRedisConStr");
    options.InstanceName = "DB0";
});
builder.Services.AddSingleton<ICaching, RedisCache>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/api/error");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
