using Cinema.Cosmos;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
 


    [Route("api/[controller]")]
    [ApiController]
    public class MovieCosmosController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public MovieCosmosController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }

        // GET api/items
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM c"));
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetAsync(id));
        }

        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie movie)
        {
            movie.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        // PUT api/items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] Movie movie)
        {
            await _cosmosDbService.UpdateAsync(movie.Id, movie);
            return NoContent();
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
            return NoContent();
        }
    }
}
