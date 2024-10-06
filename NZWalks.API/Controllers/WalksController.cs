using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE A NEW WALK / POST
        //difficulty ve region buna  bagli oldugu icin onların guid idleri random yaratılamaz tabledakilerden biri olmalı???? değilse bu metodda yeni difficulty mi yaratılmalı mesela???
        [HttpPost]
        //[Route("api/[controller]")]
        public async Task<IActionResult> CreateWalk([FromBody] InputWalkDto inputWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(inputWalkDto);
            walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel); //will this change anything ever, is it needed?
            var returnDTO = mapper.Map<WalkDto>(walkDomainModel);
            //return CreatedAtAction(nameof(GetWalkById), new { id = returnDTO.Id }, returnDTO);
            return Ok(returnDTO);
        }

        //GET ALL WALKS
        //get query strings for filtering. they are query strings inside the url
        
        // GET: https://localhost:portno/api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10   
        //track is like john. it is what we are searching for in the name
        [HttpGet]
        //[Route("api/[controller]")]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)//why are ints not nullable
        {
            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize); //if isascending is null turn it to true

           
            return Ok(mapper.Map<List<WalkDto>>(walks));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetWalkByIdAsync(id);
            if(walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteWalkAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto updateDto)
        {
            var walk = await walkRepository.UpdateWalkAsync(id,mapper.Map<Walk>(updateDto));
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

    }
}