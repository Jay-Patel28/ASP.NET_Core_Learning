using AutoMapper;
using Cinema.Authentication;
using Cinema.Caching;
using Cinema.Controllers;
using Cinema.Cosmos;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CinemaContext>
    (options => options.UseSqlServer(dbConnectionString));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CinemaContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
{
    var databaseName = configurationSection["DatabaseName"];
    var containerName = configurationSection["ContainerName"];
    var account = configurationSection["Account"];
    var key = configurationSection["Key"];
    var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
    var cosmosDbService = new CosmosDbService(client, databaseName, containerName);
    return cosmosDbService;
}

//builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());


MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<ActorModel, ActorEntity>();
    mc.CreateMap<ActorEntity, ActorModel>()
    .ForMember(des => des.ActorId, opt => opt.MapFrom(sr=> sr.Id));

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

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICaching, InMemoryCache>();
builder.Services.AddCors(); // Make sure you call this previous to AddMvc
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("CinemaRedisConStr");
//    options.InstanceName = "DB0";
//});
//builder.Services.AddSingleton<ICaching, RedisCache>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(
       options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
   );
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler("/api/error");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
