using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUsersTaskRepository _usersTaskRepository;
        private readonly IMapper _mapper;
        public ProjectTaskService
            (IProjectTaskRepository projectTaskRepository, 
            IMapper mapper,
            ICommentRepository commentRepository,
            IUsersTaskRepository usersTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _usersTaskRepository = usersTaskRepository;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasksInTeamAsync(int teamId, SortParams sortParams)
        {
            var query = await _projectTaskRepository.GetAllInTeamAsync(teamId);

            query = sortParams.SortBy.ToLower() switch
            {
                "id" => sortParams.SortOrder == "asc" ? 
                    query.OrderBy(t => t.TaskId) : query.OrderByDescending(t => t.TaskId),
                "title" => sortParams.SortOrder == "asc" ? 
                    query.OrderBy(t => t.TaskName) : query.OrderByDescending(t => t.TaskName),
                "deadline" => sortParams.SortOrder == "asc" ? 
                    query.OrderBy(t => t.TaskDeadline) : query.OrderByDescending(t => t.TaskDeadline),
                "status" => sortParams.SortOrder == "asc" ? 
                    query.OrderBy(t => t.TaskStatus.StatusName) : query.OrderByDescending(t => t.TaskStatus.StatusName),
                "created by" => sortParams.SortOrder == "asc" ? 
                    query.OrderBy(t => t.CreatedByNavigation.Username) : query.OrderByDescending(t => t.CreatedByNavigation.Username),
                "type" => sortParams.SortOrder == "asc" ?
                    query.OrderBy(t => t.TaskType.TypeName) : query.OrderByDescending(t => t.TaskType.TypeName),
                "attached to" => sortParams.SortOrder == "asc" ?
                    query.OrderBy(t => t.UsersTasks.FirstOrDefault()?.User.Username) :
                    query.OrderByDescending(t => t.UsersTasks.FirstOrDefault()?.User.Username),
                _ => query // domyślnie bez sortowania
            };

            return query.ToList();
        }

        public async Task AddProjectTaskAsync(ProjectTaskInsertDto projectTaskInsert)
        {
            var projectTask = _mapper.Map<ProjectTaskInsertDto, ProjectTask>(projectTaskInsert);
            projectTask.CreatedAt= DateTime.Now;
            projectTask.UpdatedAt = DateTime.Now;
            await _projectTaskRepository.InsertAsync(projectTask, projectTaskInsert.UserIds);
        }

        public async Task DeleteProjectTaskAsync(int id)
        {
            var comments = await _commentRepository.GetAllTaskCommentsAsync(id);
            var usersTasks = await _usersTaskRepository.GetUsersTaskByTaskIdAsync(id);

            foreach (var comment in comments)
            {
                await _commentRepository.DeleteAsync(comment.CommentId);
            }
            foreach(var usersTask in usersTasks)
            {
                await _usersTaskRepository.DeleteUserTaskAsync(usersTask.UserTaskId);
            }

            await _projectTaskRepository.DeleteAsync(id);
        }

        public async Task<ProjectTaskDto> GetTaskByIdAsync(int taskId)
        {
            return await _projectTaskRepository.GetByIdAsync(taskId);
        }

        public async Task<IEnumerable<TaskUserGetDto>> GetTaskUsersAsync(int taskId)
        {
            return await _projectTaskRepository.GetTaskUsersByTaskIdAsync(taskId);
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetTasksBySprintIdAsync(int sprintId)
        {
            return await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);
        }

        public async Task<IEnumerable<UsersTasksToDoDto>> GetToDoTasksAsync(string username)
        {
            var tasks = await _projectTaskRepository.GetAllTasksByUsernameAsync(username);
            
            var todoTasks = tasks
                .Select(_mapper.Map<ProjectTask, UsersTasksToDoDto>)
                .Where(t => t.TaskStatusId == 1 || t.TaskStatusId == 3);

            return todoTasks;
        }

        public async Task<int> GetTasksCountInSprintAsync(int sprintId)
        {
            var tasks = await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);
            return tasks.Count();
        }

        public async Task<int> GetTodoTasksCountInSprintAsync(int sprintId)
        {
            var tasks = await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);
            return tasks.Count(t => t.TaskStatusId == 1);
        }

        public async Task<int> GetInProgressTasksCountInSprintAsync(int sprintId)
        {
            var tasks = await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);
            return tasks.Count(t => t.TaskStatusId == 3);
        }

        public async Task<int> GetDoneTasksCountInSprintAsync(int sprintId)
        {
            var tasks = await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);
            return tasks.Count(t => t.TaskStatusId == 2);
        }
    }
}
