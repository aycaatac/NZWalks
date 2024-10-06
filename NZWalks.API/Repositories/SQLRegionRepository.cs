using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public NZWalksDbContext DbContext { get; }

        public async Task<List<Region>> GetAllAsync()
        {
            return await DbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {

            return await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
        }

        //where does it check whether or not there is an entry with the same id???
        public async Task<Region> CreateRegionAsync(Region newRegion)
        {
            await DbContext.Regions.AddAsync(newRegion);
            await DbContext.SaveChangesAsync();
            return newRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null; // cant we just return region? will it not be the same
            }
            DbContext.Regions.Remove(region);
            await DbContext.SaveChangesAsync();
            return region;
        }

       //cannot use Task<Region> as Region???? it cannot be modified??
       //the input is a region without id? is it not a region dto??
        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regionn = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionn == null)
            {
                return null; //??????
            }
            // we cannot alter id right?? regionn.Id = region.Id;
            regionn.Code = region.Code;
            regionn.Name = region.Name;
            regionn.RegionImageUrl = region.RegionImageUrl;
            await DbContext.SaveChangesAsync();

            return regionn;
        }
    }
}
