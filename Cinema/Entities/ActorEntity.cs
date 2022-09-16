namespace Cinema.Entities
{
    public class ActorEntity : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int wealth { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<ActorMovieEntity>? ActorMovies { get; set; }
    }
}
