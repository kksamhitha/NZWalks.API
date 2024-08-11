using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository //: IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>()
                {
                    new Region()
                    {
                        id = Guid.NewGuid(),
                        Code = "SAM",
                        Name = "New InMemory Region",
                        RegionImageURL = "someimage.jpg"
                    }
                };
        }
    }
}
