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
    }
}
