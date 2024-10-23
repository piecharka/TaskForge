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
    }
}
