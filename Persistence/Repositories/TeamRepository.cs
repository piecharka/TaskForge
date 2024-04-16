using Domain;
using Domain.Interfaces.Repositories;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public sealed class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
       public TeamRepository(TaskForgeDbContext forgeDbContext) : base(forgeDbContext) { }
    }
}
