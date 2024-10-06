using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if(walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }


        //aynı anda iki seyi nasıl filterlıycam
        //use include to return navigation properties through their IDs
        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber = 1, int pageSize = 1000) //we dont write bool? ????????
        {
            //can be written as .Include(x => x.Difficulty) makes it type safe
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            //retrieve walks as queryable but not async??
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if((string.IsNullOrWhiteSpace(filterOn) || string.IsNullOrWhiteSpace(filterQuery))){
                if (string.IsNullOrWhiteSpace(sortBy))
                {
                    return await walks.ToListAsync();
                }
            } 

            else
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                } 
                else if(filterOn.Equals("Difficulty", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Difficulty.Name.Contains(filterQuery));
                }
            }

            //sorting 
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            //cannot check whether page no and size are null??

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if(walk == null)
            {
                return null;
            }
            return walk;
        }

        //do we use mapping in repositories?
        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walkI)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null; //we put the null condition here but we dont need to put it in get by id because in here we are performing operations and it shouldnt be null
            }

            //WHY DONT WE USE MAPPING IN HERE?????????????????????
            walk.Name = walkI.Name;
            walk.Description = walkI.Description;
            walk.LengthInKm = walkI.LengthInKm;
            walk.WalkImageUrl = walkI.WalkImageUrl;
            walk.RegionId = walkI.RegionId;
            walk.DifficultyId = walkI.DifficultyId;

            await dbContext.SaveChangesAsync();
           
            //walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            
            //do either one of these to see the updated navigation properties in your return

            //await dbContext.Entry(walk).Reference(w => w.Difficulty).LoadAsync();
            //await dbContext.Entry(walk).Reference(w => w.Region).LoadAsync();
            return walk;
        }
    }
}
