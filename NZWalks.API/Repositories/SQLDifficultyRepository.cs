using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;

        public SQLDifficultyRepository(NZWalksDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Difficulty> CreateDifficultyAsync(Difficulty difficulty)
        {
            await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteDifficultyAsync(Guid id)
        {
            var diff = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (diff == null)
            {
                return null;
            }
            dbContext.Difficulties.Remove(diff);
            await dbContext.SaveChangesAsync();
            return diff;
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await dbContext.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetDifficultyByIdAsync(Guid id) //is this okay??
        {
            var diff = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

            if(diff == null)
            {
                return null;
            }

            return diff; 
        }

        public async Task<Difficulty?> UpdateDifficultyAsync(Guid id, Difficulty difficulty)
        {
            var diff = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (diff == null)
            {
                return null;
            }

            //CAN I USE MAPPER IN HERE?????????????????
            diff.Name = difficulty.Name;
            await dbContext.SaveChangesAsync();
            return diff;
            //diff = mapper.Map<Difficulty>(difficulty); IS THIS OKAY??
        }
    }
}
