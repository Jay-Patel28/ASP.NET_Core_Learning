using Cinema.Entities;

namespace Cinema.Models
{
    public class ActorModel
    {
        public Guid ActorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int wealth { get; set; }

        public List<MovieModel>? MovieModels { get; set; }
    }
}
