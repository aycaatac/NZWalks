using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            //just write the names if the properties
            //are the same(name and type, will nullable matter?) but if they are diff you
            //have to write mapping
          
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<InputRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<Difficulty, InputDifficultyDto>().ReverseMap();
            CreateMap<Difficulty, UpdateDifficultyDto>().ReverseMap();
            CreateMap<Walk, InputWalkDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkDto>().ReverseMap();
            // EXAMPLE _____ CreateMap<>.ForMember(x => x.Name, opt => opt.MapFrom(source.name));
        }
    }
}
