using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.DTOs;

namespace Persistence
{
    public sealed class TeamRepository : ITeamRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public TeamRepository(TaskForgeDbContext forgeDbContext) 
        { 
            _forgeDbContext = forgeDbContext;
        }
        public async Task<TeamDto> GetByIdAsync(int teamId)
        {
            return await _forgeDbContext.Teams
                .Include(t => t.Users)
                .Select(t => new TeamDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    Users = t.Users.Select(u => new TeamUserDto
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email,    
                    }).ToList()
                }).Where(t => t.TeamId == teamId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TeamDto>> GetAllAsync()
        {
            return await _forgeDbContext.Teams
                .Include(t => t.Users)
                .Select(t => new TeamDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    Users = t.Users.Select(u => new TeamUserDto
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task DeleteAsync(int teamId)
        {
            var entity = await _forgeDbContext.Teams
                .Include(t => t.Users)
                .Where(t => t.TeamId == teamId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _forgeDbContext.Teams.Remove(entity);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"Team with id {teamId} not found.");
            }
        }

        public async Task InsertAsync(Team entity)
        {
            await _forgeDbContext.Teams
                .AddAsync(entity);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team entity)
        {
            _forgeDbContext.Teams
                .Update(entity);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task AddUserToTeamAsync(int teamId, User user, Permission permission)
        {
            var team = await _forgeDbContext.Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);

            if (team == null)
            {
                throw new Exception($"Team with ID {teamId} not found.");
            }

            //team.Users.Add(user);
            team.TeamUsers.Add(new TeamUser
            {
                User = user,
                Permission = permission
               
            });

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task AddUsersToTeamAsync(int teamId, List<User> usersToAdd)
        {
            var team = await _forgeDbContext.Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
            
            foreach(var user in usersToAdd)
            {
                team.Users.Add(user);
            }
            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TeamDto>> GetTeamsByUsername(string username)
        {
            return await _forgeDbContext.Users
                .Include(u => u.Teams)
                .Where(u => u.Username == username)
                .SelectMany(u => u.Teams
                .Select(t => new TeamDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    Users = t.Users.Select(u => new TeamUserDto
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email
                    }).ToList()
                }))
                .ToListAsync();
        }
    }
}
