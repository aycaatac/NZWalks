using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    //https://localhost:7284/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository RegionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        //what is the difference between field and property
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository
            , IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.RegionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL REGIONS
        // GET: https://localhost:7284/api/regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {

            logger.LogInformation("GetAllRegions action method was invoked");
            //get data from database - returned things are domain models

            //by adding await and async to this line we made
            //this call and the access to db asynchronous
            var regions = await RegionRepository.GetAllAsync(); //await DbContext.Regions.ToListAsync();

            //map domain models to DTOs
            //var regionsDto = new List<RegionDto>();

            /*foreach(var region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }*/
            logger.LogInformation($"Finished request: {JsonSerializer.Serialize(regions)}");
            //mapping model domains to dtos
            var regionsDto = mapper.Map<List<RegionDto>>(regions);
            //do not return domain models to client - return DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGION BY ID
        // GET: https://localhost:7284/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            //two ways to do this
            //find method only takes the primary key but first or default method can look for all variables

            //var region = DbContext.Regions.Find(id);

            var region = await RegionRepository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
         
            //OR
            /*var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };*/

            return Ok(mapper.Map<RegionDto>(region));
        }


        // POST TO CREATE NEW REGION
        // POST: https://localhost:7284/api/regions
        [HttpPost]
        //[Route("api/[controller]")]
        //[Authorize(Roles = "Writer")]
        //id gets created internally by the application,
        //it is not taken as an input from client
        public async Task<IActionResult> CreateRegion([FromBody] InputRegionDto inputRegion)
        {
            //create new dto with only 3 properties to take an input region from
            //clients

            //map or create dto to domain model in order to add
            //it into the database
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regionDomainModel = mapper.Map<Region>(inputRegion);

            //use domain model to create region using the dbcontext
            await RegionRepository.CreateRegionAsync(regionDomainModel);


            //map domain model to a DTO to send it back
            var returnDTO = mapper.Map<RegionDto>(regionDomainModel);

            //we return the name of the method that can be
            //used to retrieve the newly created entry
            //nameof is used bc if the name of the method is changed, it still returns that method
            //with its new name
            return CreatedAtAction(nameof(GetRegionById), new { id = returnDTO.Id }, returnDTO);
        }


        //sadece bi özellik değiştirmek isesek???? nullable yapsak olur mu dto yu
        // PUT TO UPDATE AN EXISTING REGION
        // PUT: https://localhost:7284/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")] //the id syntax should match the input parameter id below
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegion)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //map dto to domain model to pass to region repository
            var regionDomian = mapper.Map<Region>(updateRegion); //does this give the region a new id by default???????????????
            
            //Guid i = regionDomian.Id; //gives all 0's to id in both cases

            var regionDomainModel = await RegionRepository.UpdateRegionAsync(id, regionDomian);    
            
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //convert domain model to a dto to return the updated values to the client
            var returnDto = mapper.Map<RegionDto>(regionDomainModel);
                
               

            return Ok(returnDto); //always return DTOs
        }


        // DELETE AN EXISTING REGION
        // DELETE: https://localhost:7284/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await RegionRepository.DeleteRegionAsync(id);
            
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //remove does not have an async method so it stays as sync
            //delete region from db using dbcontext

            //can return empty or can return the deleted object as a DTO
            var returnDto = mapper.Map<RegionDto>(regionDomainModel);

            //never return domain models, map them into dtos and return those 

            return Ok(returnDto);
        }

    }
}
