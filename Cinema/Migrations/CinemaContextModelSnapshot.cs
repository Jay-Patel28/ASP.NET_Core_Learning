﻿// <auto-generated />
using System;
using Cinema.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.Migrations
{
    [DbContext(typeof(CinemaContext))]
    partial class CinemaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cinema.Entities.ActorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("wealth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("wealth");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Cinema.Entities.ActorMovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RelatedMovieId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RelatedActorId");

                    b.HasIndex("RelatedMovieId");

                    b.ToTable("ActorsOfMovie");
                });

            modelBuilder.Entity("Cinema.Entities.MovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalViews")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TotalViews");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Cinema.Entities.ActorMovieEntity", b =>
                {
                    b.HasOne("Cinema.Entities.ActorEntity", "ActorEntity")
                        .WithMany("ActorMovies")
                        .HasForeignKey("RelatedActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cinema.Entities.MovieEntity", "MovieEntity")
                        .WithMany("ActorMovies")
                        .HasForeignKey("RelatedMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActorEntity");

                    b.Navigation("MovieEntity");
                });

            modelBuilder.Entity("Cinema.Entities.ActorEntity", b =>
                {
                    b.Navigation("ActorMovies");
                });

            modelBuilder.Entity("Cinema.Entities.MovieEntity", b =>
                {
                    b.Navigation("ActorMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
