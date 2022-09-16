using Cinema.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataContext
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ActorEntity> Actors { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<ActorMovieEntity>? ActorsOfMovie { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<ActorMovieEntity>().HasOne<ActorEntity>(p => p.ActorEntity).WithMany(b => b.ActorMovies)
                                                                            .HasForeignKey(p => p.RelatedActorId);


            modelBuilder.Entity<ActorMovieEntity>().HasOne<MovieEntity>(p => p.MovieEntity).WithMany(b => b.ActorMovies)
                                                                            .HasForeignKey(p => p.RelatedMovieId)
                                                                                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ActorEntity>().HasIndex(actor => actor.wealth);
            modelBuilder.Entity<MovieEntity>().HasIndex(movie => movie.TotalViews);

    }
    }

}
