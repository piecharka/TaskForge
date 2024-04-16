﻿using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TaskRepository : GenericRepository<ProjectTask>, ITaskRepository
    {
        public TaskRepository(TaskForgeDbContext forgeDbContext) : base(forgeDbContext) { }
    }
}
