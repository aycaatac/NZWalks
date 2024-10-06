using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //https://localhost:7284/api/difficulties
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DifficultiesController : Controller
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultiesController(NZWalksDbContext dbContext, IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            DbContext = dbContext;
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }

        public NZWalksDbContext DbContext { get; }

        //GET ALL DIFFICULTIES
        // GET: https://localhost:7284/api/difficulties
        [HttpGet]
        //[Route("api/[controller]")]

        //can this be null?????????????????
        public async Task<IActionResult> GetAllDifficulties()
        {
            var diff = await difficultyRepository.GetAllAsync();
            return Ok(mapper.Map<List<DifficultyDto>>(diff));
        }


        //GET ONE DIFFICULTY FROM ID
        // GET: https://localhost:7284/api/difficulties/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDifficultyById([FromRoute] Guid id)
        {
            var difficulty = await difficultyRepository.GetDifficultyByIdAsync(id);

            if(difficulty == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DifficultyDto>(difficulty));
        }


        //CREATE NEW DIFFICULTY
        // POST: https://localhost:7284/api/difficulties/
        [HttpPost]
        //[Route("api/[controller]")]
        public async Task<IActionResult> CreateDifficulty([FromBody] InputDifficultyDto inputDifficulty)
        {
            var difficultyDto = mapper.Map<DifficultyDto>(difficultyRepository.CreateDifficultyAsync(mapper.Map<Difficulty>(inputDifficulty)));

            

            return CreatedAtAction(nameof(GetDifficultyById), new {id = difficultyDto.Id }, difficultyDto);
        }


        //UPDATE EXISTING DIFFICULTY
        // PUT: https://localhost:7284/api/difficulties/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateDifficulty([FromRoute] Guid id, [FromBody] UpdateDifficultyDto updateDifficulty)
        {
            var difficultyDomainModel = await difficultyRepository.UpdateDifficultyAsync(id, mapper.Map<Difficulty>(updateDifficulty)); //what if i dont write await. does it work but not become async only???????????

            if(difficultyDomainModel == null)
            {
                return NotFound();
            }

            var returnDto = mapper.Map<DifficultyDto>(difficultyDomainModel);

            return Ok(returnDto);
        }


        //DELETE EXISTING DIFFICULTY
        // DELETE: https://localhost:7284/api/difficulties/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteDifficulty([FromRoute] Guid id)
        {
            var difficultyDomainModel = await difficultyRepository.DeleteDifficultyAsync(id);

            if(difficultyDomainModel == null)
            {
                return NotFound();
            }


            var returnDto = mapper.Map<DifficultyDto>(difficultyDomainModel);

            return Ok(returnDto);
        }
    }
}
