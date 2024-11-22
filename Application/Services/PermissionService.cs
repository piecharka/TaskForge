using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _permissionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _permissionRepository.GetPermissionsAsync();
        }

        public async Task<Permission> GetPermissionByUserIdAsync(int userId, int teamId)
        {
            return await _permissionRepository.GetByUserIdAsync(userId, teamId);
        }

        public async Task<bool> UpdateUsersPermissionAsync(int userId, int teamId, int permissionId)
        {
            return await _permissionRepository.ChangeUsersPermissionAsync(userId, teamId, permissionId);
        }
    }
}
