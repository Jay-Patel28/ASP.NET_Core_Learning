namespace Cinema.Entities
{
    public class MovieEntity : BaseEntity
    {
        public string? MovieName { get; set; }

        public int TotalViews { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<ActorMovieEntity>? ActorMovies { get; set; }
    }
}
