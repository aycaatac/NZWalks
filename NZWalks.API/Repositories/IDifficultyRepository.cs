using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetDifficultyByIdAsync(Guid id); //ehy the ?
        Task<Difficulty> CreateDifficultyAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateDifficultyAsync(Guid id, Difficulty difficulty);
        Task<Difficulty?> DeleteDifficultyAsync(Guid id);
    }
}
