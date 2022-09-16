using Cinema.Models;

namespace Cinema.DTOs
{
    public class ActorDTO
    {
        public Guid ActorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? wealth { get; set; }

        public List<MovieDTO>? MovieDTOs { get; set; }
    }
}
