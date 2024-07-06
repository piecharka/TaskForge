using Application.DTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProjectTaskService
    {
        public Task<IEnumerable<ProjectTask>> GetAllProjectTasksInTeamAsync(int teamId);
        public Task AddProjectTaskAsync(ProjectTaskInsertDto projectTaskInsert);
        public Task DeleteProjectTaskAsync(int teamId);
    }
}
