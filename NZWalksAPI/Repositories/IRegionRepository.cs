using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Region;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, UpdateRegionsRequestDTO updateRegionsRequestDTO);
        Task<Region?> DeleteAsync(Guid id);
    }
}
