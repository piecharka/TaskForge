using Domain;
using Domain.DTOs;
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

        public async Task<UserGetDto> GetByNameAsync(string username)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Select(u => new UserGetDto
                {
                    UserId = u.UserId,  
                    Username = u.Username,
                    Email = u.Email,
                    Birthday = u.Birthday,
                    LastLogin = u.LastLogin,
                    Teams = u.Teams.Select(t => new UserTeamDto
                    {
                        TeamId = t.TeamId,
                        TeamName = t.TeamName
                    }).ToList()
                })
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
        public async Task<UserGetDto> GetByEmailAsync(string email)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Select(u => new UserGetDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Birthday = u.Birthday,
                    LastLogin = u.LastLogin,
                    IsActive = u.IsActive,
                    Teams = u.Teams.Select(t => new UserTeamDto
                    {
                        TeamId = t.TeamId,
                        TeamName = t.TeamName
                    }).ToList()
                })
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserGetDto>> GetAllAsync()
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Select(u => new UserGetDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Birthday = u.Birthday,
                    LastLogin = u.LastLogin,
                    IsActive = u.IsActive,
                    Teams = u.Teams.Select(t => new UserTeamDto
                    {
                        TeamId = t.TeamId,
                        TeamName = t.TeamName
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<UserGetDto> GetByIdAsync(int userId)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Select(u => new UserGetDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Birthday = u.Birthday,
                    LastLogin = u.LastLogin,
                    IsActive = u.IsActive,
                    Teams = u.Teams.Select(t => new UserTeamDto
                    {
                        TeamId = t.TeamId,
                        TeamName = t.TeamName
                    }).ToList()
                })
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

        public async Task<IEnumerable<UserGetDto>> GetTeamUsersAsync(int teamId)
        {
            return await _forgeDbContext.Users
                .Include(t => t.Teams)
                .Where(u => u.Teams.Any(t => t.TeamId == teamId))
                .Select(u => new UserGetDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Birthday = u.Birthday,
                    LastLogin = u.LastLogin,
                    IsActive = u.IsActive,
                    PermissionId = u.TeamUsers
                        .Where(tu => tu.UserId == u.UserId && tu.TeamId == teamId)
                        .Select(tu => tu.PermissionId)
                        .FirstOrDefault(),
                    Teams = u.Teams.Select(t => new UserTeamDto
                    {
                        TeamId = t.TeamId,
                        TeamName = t.TeamName
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
