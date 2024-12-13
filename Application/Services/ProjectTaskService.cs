using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
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
        private readonly IUserRepository _userRepository;
        private readonly ITimeLogRepository _timeLogRepository;
        private readonly IMapper _mapper;
        public ProjectTaskService
            (IProjectTaskRepository projectTaskRepository, 
            IMapper mapper,
            ICommentRepository commentRepository,
            IUsersTaskRepository usersTaskRepository,
            IUserRepository userRepository,
            ITimeLogRepository timeLogRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _usersTaskRepository = usersTaskRepository;
            _userRepository = userRepository;
            _timeLogRepository = timeLogRepository;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasksInTeamAsync
            (int teamId, SortParams sortParams, Dictionary<string, string> filters)
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
                _ => query 
            };

            foreach (var filter in filters)
            {
                query = filter.Key.ToLower() switch
                {
                    "id" => int.TryParse(filter.Value, out var idValue)
                    ? query.Where(t => t.TaskId == idValue)
                    : query, 
                    "title" => query.Where(t => t.TaskName.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)),
                    "deadline" => DateTime.TryParse(filter.Value, out var deadlineValue)
                        ? query.Where(t => t.TaskDeadline.Date == deadlineValue.Date)
                        : query,
                    "sprint" => query.Where(t => t.Sprint.SprintName.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)),
                    "status" => query.Where(t => t.TaskStatus.StatusName.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)),
                    "created by" => query.Where(t => t.CreatedByNavigation.Username.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)),
                    "type" => query.Where(t => t.TaskType.TypeName.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)),
                    "attached to" => query.Where(t => t.UsersTasks.Any(ut => ut.User.Username.Contains(filter.Value, StringComparison.OrdinalIgnoreCase))),
                    _ => query
                };
            }

            var taskDto = _mapper.Map<IEnumerable<ProjectTask>, IEnumerable<ProjectTaskDto>>(query);

            return taskDto;
        }

        public async Task AddProjectTaskAsync(ProjectTaskInsertDto projectTaskInsert)
        {
            var projectTask = _mapper.Map<ProjectTaskInsertDto, ProjectTask>(projectTaskInsert);
            projectTask.CreatedAt= DateTime.Now;
            projectTask.UpdatedAt = DateTime.Now;
            await _projectTaskRepository.InsertAsync(projectTask, projectTaskInsert.UserIds);
        }

        public async Task AddUsersToTaskAsync(UserTasksInsertDto userTaskInsert)
        {
            await _projectTaskRepository.AddUsersToTaskAsync(userTaskInsert.TaskId, userTaskInsert.UserIds);
        }

        public async Task DeleteUserFromTaskAsync(int taskId, int userId)
        {
            await _usersTaskRepository.DeleteUserTaskAsync(taskId, userId);
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
                await _usersTaskRepository.DeleteUserTaskByIdAsync(usersTask.UserTaskId);
            }

            await _projectTaskRepository.DeleteAsync(id);
        }

        public async Task<ProjectTaskDto> GetTaskByIdAsync(int taskId)
        {
            var task = await _projectTaskRepository.GetByIdAsync(taskId);

            return _mapper.Map<ProjectTask, ProjectTaskDto>(task);
        }

        public async Task<IEnumerable<TaskUserGetDto>> GetTaskUsersAsync(int taskId)
        {
            var user = await _projectTaskRepository.GetTaskUsersByTaskIdAsync(taskId);

            return _mapper.Map<IEnumerable<User>, IEnumerable<TaskUserGetDto>>(user);
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetTasksBySprintIdAsync(int sprintId)
        {
            var task = await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);

            return _mapper.Map<IEnumerable<ProjectTask>, IEnumerable<ProjectTaskDto>>(task);
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

        public async Task<List<UserTaskCountDto>> GetAllUserTasksCountInSprintAsync(int teamId, int sprintId)
        {
            var usersInTeam = await _userRepository.GetTeamUsersAsync(teamId);
            var userTaskCountList = new List<UserTaskCountDto>();
            foreach(var user in usersInTeam)
            {
                var userTasks = await _projectTaskRepository.GetTasksAssignedInSprintAsync(sprintId, user.UserId);
                userTaskCountList.Add(new UserTaskCountDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    TaskCount = userTasks.Count(),
                });
            }

            return userTaskCountList;   
        }

        public async Task UpdateProjectTaskStatusAsync(int taskId, int statusId)
        {
            await _projectTaskRepository.UpdateProjectTaskStatusAsync(taskId, statusId);
            if(statusId == 2)
            {
                await _timeLogRepository.AddTimeLog(new TimeLog
                {
                    LogDate = DateTime.Now,
                    LogTypeId = 2,
                    TaskId = taskId,
                });
            }
        }
    }
}
