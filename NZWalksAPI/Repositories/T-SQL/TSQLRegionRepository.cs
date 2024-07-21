using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Region;
using NZWalks.API.Repositories;

namespace NZWalksAPI.Repositories.T_SQL
{
    public class TSQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _dbContext;

        public TSQLRegionRepository(NZWalksDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, UpdateRegionsRequestDTO updateRegionsRequestDTO)
        {
            var regionDomainSearched = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainSearched == null)
            {
                return null;
            }

            regionDomainSearched.Code = updateRegionsRequestDTO.Code;
            regionDomainSearched.Name = updateRegionsRequestDTO.Name;
            regionDomainSearched.RegionImageUrl = updateRegionsRequestDTO.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return regionDomainSearched;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionToDelete = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionToDelete == null)
                return null;

            _dbContext.Regions.Remove(regionToDelete);
            await _dbContext.SaveChangesAsync();

            return regionToDelete;
        }
    }
}
