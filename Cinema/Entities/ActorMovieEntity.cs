namespace Cinema.Entities
{
    public class ActorMovieEntity : BaseEntity
    {
       public Guid RelatedActorId { get; set; }

        public ActorEntity ActorEntity { get; set; }

        public Guid RelatedMovieId { set; get; }

        public MovieEntity MovieEntity { get; set; }
    }
}
