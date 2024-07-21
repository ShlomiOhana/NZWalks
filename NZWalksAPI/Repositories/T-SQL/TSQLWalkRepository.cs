using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalksAPI.Repositories.T_SQL
{
    public class TSQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext _dbContext;

        public TSQLWalkRepository(NZWalksDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filter by query
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkToDelete = await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

            if (walkToDelete == null)
                return null;

            _dbContext.Walks.Remove(walkToDelete);
            await _dbContext.SaveChangesAsync();

            return walkToDelete;
        }
    }
}
