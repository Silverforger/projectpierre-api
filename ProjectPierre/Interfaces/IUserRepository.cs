using ProjectPierre.Models;

namespace ProjectPierre.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string userId);
        Task<bool> IsValidUserId(string userId);
    }
}
