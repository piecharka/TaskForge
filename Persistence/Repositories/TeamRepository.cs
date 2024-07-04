﻿using Domain;
using Domain.Interfaces.Repositories;
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
        public async Task<TeamDto> GetByIdAsync(int id)
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
                        PasswordHash = u.PasswordHash,
                        Birthday = u.Birthday,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt,
                        LastLogin = u.LastLogin,
                        IsActive = u.IsActive,
                        Attachments = u.Attachments,
                        Comments = u.Comments,
                        Notfications = u.Notfications,
                        ProjectTasks = u.ProjectTasks,
                        UsersTasks = u.UsersTasks,
                    }).ToList()
                }).Where(t => t.TeamId == id)
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
                        PasswordHash = u.PasswordHash,
                        Birthday = u.Birthday,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt,
                        LastLogin = u.LastLogin,
                        IsActive = u.IsActive,
                        Attachments = u.Attachments,
                        Comments = u.Comments,
                        Notfications = u.Notfications,
                        ProjectTasks = u.ProjectTasks,
                        UsersTasks = u.UsersTasks,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _forgeDbContext.Teams
                .Include(t => t.Users)
                .Where(t => t.TeamId == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _forgeDbContext.Teams.Remove(entity);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"Team with id {id} not found.");
            }
        }

        public async Task InsertAsync(Team entity)
        {
            await _forgeDbContext.Set<Team>().AddAsync(entity);
            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team entity)
        {
            _forgeDbContext.Set<Team>().Update(entity);
            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task AddUserToTeamAsync(int teamId, User user)
        {
            var team = await _forgeDbContext.Teams.Include(t => t.Users).FirstOrDefaultAsync(t => t.TeamId == teamId);

            if (team == null)
            {
                throw new Exception($"Team with ID {teamId} not found.");
            }

            team.Users.Add(user);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task AddUsersToTeamAsync(int id, List<User> usersToAdd)
        {
            var team = await _forgeDbContext.Set<Team>().FindAsync(id);
            
            foreach(var user in usersToAdd)
            {
                //team.Users.Add(user);
            }
            await _forgeDbContext.SaveChangesAsync();
        }
    }
}
