using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TaskTypeRepository : GenericRepository<ProjectTaskType>, ITaskTypeRepository
    {
        public TaskTypeRepository(TaskForgeDbContext context) : base(context) { }
    }
}
