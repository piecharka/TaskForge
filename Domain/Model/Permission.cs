using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public int PermissionRank { get; set; }
        public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
    }
}
