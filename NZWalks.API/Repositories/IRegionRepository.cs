using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
      Task< List<Region>> GetAllAsync(); //definition
      Task<Region?> GetByIdAsync(Guid Id);
      Task<Region> CreateAsync(Region region);
      Task<Region?> UpdateAsync(Guid id, Region region);
      Task<Region?> DeleteAsync(Guid id);
    }
}
