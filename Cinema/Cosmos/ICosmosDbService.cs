namespace Cinema.Cosmos
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Movie>> GetMultipleAsync(string query);
        Task<Movie> GetAsync(string id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(string id, Movie movie);
        Task DeleteAsync(string id);
    }
}
