using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Cinema.Cosmos
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(Movie movie)
        {
            await _container.CreateItemAsync(movie, new PartitionKey(movie.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Movie>(id, new PartitionKey(id));
        }

        public async Task<Movie> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Movie>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<Movie>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Movie>(new QueryDefinition(queryString));

            var results = new List<Movie>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, Movie movie)
        {
            await _container.UpsertItemAsync(movie, new PartitionKey(id));
        }
    }
}
