using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.Interfaces;
using ProjectPierre.Models;

namespace ProjectPierre.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> IsValidUserId(string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            return user == null ? false : true;
        }


    }
}
