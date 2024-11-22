using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPermissionService
    {
        Task<Permission> GetPermissionByIdAsync(int id);
        Task<Permission> GetPermissionByUserIdAsync(int userId, int teamId);
        Task<bool> UpdateUsersPermissionAsync(int userId, int teamId, int permissionId);
        Task<IEnumerable<Permission>> GetPermissionsAsync();
    }
}
