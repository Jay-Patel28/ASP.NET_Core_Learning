using Cinema.Models;

namespace Cinema.DTOs
{
    public class MovieDTO
    {
        public Guid Id { get; set; }
        public string? MovieName { get; set; }

        public int? TotalViews { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<ActorDTO> ActorDTOs { get; set; }
    }
}
