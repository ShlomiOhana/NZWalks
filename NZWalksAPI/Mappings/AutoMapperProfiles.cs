using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Difficulty;
using NZWalks.API.Models.DTO.Region;
using NZWalks.API.Models.DTO.Walk;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Region
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionsRequestDTO>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();

            // Walk
            CreateMap<Walk, AddWalkRequestDTO>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDTO>().ReverseMap();

            // Difficulty
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        }
    }
}
