using Domain;
using Domain.Interfaces.Repositories;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public PermissionRepository(TaskForgeDbContext forgeDbContext)
        {
            _forgeDbContext = forgeDbContext;
        }

        public async Task<Permission> GetByIdAsync(int permissionId)
        {
            return await _forgeDbContext.Permissions
                .Where(p => p.PermissionId == permissionId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _forgeDbContext.Permissions.ToListAsync();
        }

        public async Task<Permission> GetByUserIdAsync(int userId, int teamId)
        {
            return await _forgeDbContext.Teams
                .SelectMany(team => team.TeamUsers)
                .Where(tu => tu.UserId == userId && tu.TeamId == teamId)
                .Select(tu => tu.Permission)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ChangeUsersPermissionAsync(int userId, int teamId, int permissionId)
        {
            var team = await _forgeDbContext.Teams
                .Include(t => t.TeamUsers)       // Dołączamy TeamUsers
                .FirstOrDefaultAsync(t => t.TeamId == teamId);

            if (team == null)
            {
                return false; 
            }

            var teamUser = team.TeamUsers.FirstOrDefault(tu => tu.UserId == userId);
            if (teamUser == null)
            {
                return false; 
            }

            teamUser.PermissionId = permissionId;

            // Zapisujemy zmiany
            await _forgeDbContext.SaveChangesAsync();

            return true; // Zwracamy true, jeśli aktualizacja się powiodła
        }
    }
}
