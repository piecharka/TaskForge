﻿using Application.DTOs;
using Domain;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasksInTeamAsync(int teamId, SortParams sortParams);
        public Task AddProjectTaskAsync(ProjectTaskInsertDto projectTaskInsert);
        Task AddUsersToTaskAsync(UserTasksInsertDto userTaskInsert);
        Task DeleteUserFromTaskAsync(int taskId, int userId);
        public Task DeleteProjectTaskAsync(int teamId);
        Task<IEnumerable<TaskUserGetDto>> GetTaskUsersAsync(int taskId);
        Task<IEnumerable<UsersTasksToDoDto>> GetToDoTasksAsync(string username);
        Task<ProjectTaskDto> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<ProjectTaskDto>> GetTasksBySprintIdAsync(int sprintId);
        Task<int> GetTasksCountInSprintAsync(int sprintId);
        Task<int> GetTodoTasksCountInSprintAsync(int sprintId);
        Task<int> GetInProgressTasksCountInSprintAsync(int sprintId);
        Task<int> GetDoneTasksCountInSprintAsync(int sprintId);
        Task<List<UserTaskCountDto>> GetAllUserTasksCountInSprintAsync(int teamId, int sprintId);
        Task UpdateProjectTaskStatusAsync(int taskId, int statusId);
    }
}
