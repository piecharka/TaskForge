using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task<Permission> GetByIdAsync(int permissionId);
        Task<Permission> GetByUserIdAsync(int userId, int teamId);
        Task<bool> ChangeUsersPermissionAsync(int userId, int teamId, int permissionId);
        Task<IEnumerable<Permission>> GetPermissionsAsync();
    }
}
