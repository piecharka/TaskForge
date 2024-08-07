﻿using Domain.DTOs;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTaskDto>> GetAllInTeamAsync(int teamId);
        Task<ProjectTaskDto> GetByIdAsync(int id);
        Task InsertAsync(ProjectTask user);
        Task UpdateAsync(ProjectTask user);
        Task DeleteAsync(int teamId);
        Task<ICollection<TaskUserGetDto>> GetTaskUsersByTaskIdAsync(int taskId);
        Task<ICollection<ProjectTask>> GetAllTasksByUserIdAsync(int userId);
        Task<ICollection<ProjectTask>> GetAllTasksByUsernameAsync(string username);
    }
}
