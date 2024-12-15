using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public UserRepository(TaskForgeDbContext forgeDbContext)
        {
            _forgeDbContext = forgeDbContext;
        }

        public async Task<User> GetByNameAsync(string username)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetWholeUserObjectByIdAsync(int userId)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetWholeUserObjectByUsernameAsync(string username)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(User user)
        {
            await _forgeDbContext.Users
                .AddAsync(user);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _forgeDbContext.Users
                .Update(user);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId)
        {
            var user = await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(t => t.UserId == userId)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                _forgeDbContext.Users.Remove(user);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Team with id {userId} not found.");
            }
        }

        public async Task<IEnumerable<User>> GetTeamUsersAsync(int teamId)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Include(t => t.TeamUsers)
                .ThenInclude(tu => tu.Permission)
                .Include(u => u.ProjectTasks)
                .Where(u => u.Teams.Any(t => t.TeamId == teamId))
                .ToListAsync();
        }
    }
}
