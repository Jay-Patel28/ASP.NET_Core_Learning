using Cinema.Entities;

namespace Cinema.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string? MovieName { get; set; }

        public int TotalViews { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<ActorModel> ActorModels { get; set; }
    }
}
