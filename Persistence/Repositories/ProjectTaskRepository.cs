using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public ProjectTaskRepository(TaskForgeDbContext forgeDbContext) 
        { 
            _forgeDbContext = forgeDbContext;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllInTeamAsync(int teamId)
        {
            return await _forgeDbContext.ProjectTasks
                 .Include(t => t.Attachments) 
                 .Include(t => t.Comments)
                 .ThenInclude(c => c.WrittenByNavigation)
                 .Include(t => t.CreatedByNavigation)
                 .Include(t => t.TaskStatus)
                 .Include(t => t.TaskType)
                 .Include(t => t.Team)
                 .Include(t => t.UsersTasks)
                 .ThenInclude(ut => ut.User)
                 .Include(t => t.UsersTasks)
                 .Select(t => new ProjectTaskDto
                 {
                     TaskId = t.TaskId,
                     TaskName = t.TaskName,
                     TaskStatusId = t.TaskStatusId,
                     TeamId = t.TeamId,
                     CreatedBy = t.CreatedBy,
                     CreatedAt = t.CreatedAt,
                     TaskDeadline = t.TaskDeadline,
                     TaskTypeId = t.TaskTypeId,
                     TaskDescription = t.TaskDescription,
                     Attachments = t.Attachments.Select(a => new TaskAttachmentsDto
                     {
                         AttachmentId = a.AttachmentId,
                         TaskId = a.TaskId,
                         AddedBy = a.AddedBy,
                         FilePath = a.FilePath
                     }).ToList(),
                     Comments = t.Comments.Select(c => new TaskCommentDto
                     {
                         CommentId = c.CommentId,
                         TaskId = c.TaskId,
                         WrittenBy = c.WrittenBy,
                         CommentText = c.CommentText,
                         WrittenAt = c.WrittenAt,
                         WrittenByNavigation = new TaskCommentUserDto
                         {
                             UserId = c.WrittenByNavigation.UserId,
                             Username = c.WrittenByNavigation.Username,
                         },
                     }).ToList(),
                     CreatedByNavigation = new TaskUserGetDto
                     {
                         UserId = t.CreatedByNavigation.UserId,
                         Username = t.CreatedByNavigation.Username,
                         Email = t.CreatedByNavigation.Email,
                         LastLogin = t.CreatedByNavigation.LastLogin,
                     },
                     TaskStatus = t.TaskStatus,
                     TaskType = t.TaskType,
                     UsersTasks = t.UsersTasks.Select(ut => new UserTaskDto
                     {
                         UserTaskId = ut.UserTaskId,
                         UserId = ut.UserId,
                         TaskId = ut.TaskId,
                         User = new TaskUserGetDto
                         {
                             UserId = ut.User.UserId,
                             Username = ut.User.Username,
                             Email = ut.User.Email,
                             LastLogin = ut.User.LastLogin,
                         },
                     }).ToList()
                 })
                 .Where(pt => pt.TeamId == teamId)
                 .ToListAsync();
        }

        public async Task<ICollection<ProjectTask>> GetAllTasksByUserIdAsync(int userId)
        {
            return await _forgeDbContext.UsersTasks
                .Include(ut => ut.User)
                .Where(ut => ut.UserId == userId)
                .Select(ut => ut.Task)
                .ToListAsync();
        }

        public async Task<ICollection<ProjectTask>> GetAllTasksByUsernameAsync(string username)
        {
            return await _forgeDbContext.UsersTasks
                .Include(ut => ut.Task)
                    .ThenInclude(t => t.Team)
                .Include(ut => ut.Task)
                    .ThenInclude(t => t.TaskStatus)
                .Include(ut => ut.User)
                .Where(ut => ut.User.Username == username)
                .Select(ut => ut.Task)
                .ToListAsync();
        }

        public async Task<ICollection<ProjectTaskDto>> GetAllTasksBySprintIdAsync(int sprintId)
        {
            return await _forgeDbContext.ProjectTasks
                .Include(t => t.Attachments)
                .Include(t => t.Comments)
                .ThenInclude(c => c.WrittenByNavigation)
                .Include(t => t.CreatedByNavigation)
                .Include(t => t.TaskStatus)
                .Include(t => t.TaskType)
                .Include(t => t.Team)
                .Include(t => t.UsersTasks)
                .ThenInclude(ut => ut.User)
                .Include(t => t.UsersTasks)
                .Select(t => new ProjectTaskDto
                {
                    TaskId = t.TaskId,
                    TaskName = t.TaskName,
                    TaskStatusId = t.TaskStatusId,
                    TeamId = t.TeamId,
                    CreatedBy = t.CreatedBy,
                    CreatedAt = t.CreatedAt,
                    TaskDeadline = t.TaskDeadline,
                    TaskTypeId = t.TaskTypeId,
                    TaskDescription = t.TaskDescription,
                    Attachments = t.Attachments.Select(a => new TaskAttachmentsDto
                    {
                        AttachmentId = a.AttachmentId,
                        TaskId = a.TaskId,
                        AddedBy = a.AddedBy,
                        FilePath = a.FilePath
                    }).ToList(),
                    Comments = t.Comments.Select(c => new TaskCommentDto
                    {
                        CommentId = c.CommentId,
                        TaskId = c.TaskId,
                        WrittenBy = c.WrittenBy,
                        CommentText = c.CommentText,
                        WrittenAt = c.WrittenAt,
                        WrittenByNavigation = new TaskCommentUserDto
                        {
                            UserId = c.WrittenByNavigation.UserId,
                            Username = c.WrittenByNavigation.Username,
                        },
                    }).ToList(),
                    CreatedByNavigation = new TaskUserGetDto
                    {
                        UserId = t.CreatedByNavigation.UserId,
                        Username = t.CreatedByNavigation.Username,
                        Email = t.CreatedByNavigation.Email,
                        LastLogin = t.CreatedByNavigation.LastLogin,
                    },
                    TaskStatus = t.TaskStatus,
                    TaskType = t.TaskType,
                    UsersTasks = t.UsersTasks.Select(ut => new UserTaskDto
                    {
                        UserTaskId = ut.UserTaskId,
                        UserId = ut.UserId,
                        TaskId = ut.TaskId,
                        User = new TaskUserGetDto
                        {
                            UserId = ut.User.UserId,
                            Username = ut.User.Username,
                            Email = ut.User.Email,
                            LastLogin = ut.User.LastLogin,
                        },
                    }).ToList(),
                    Sprint = new SprintGetDto
                    {
                        SprintId = t.Sprint.SprintId,
                        SprintName = t.Sprint.SprintName,
                        SprintStart = t.Sprint.SprintStart,
                        SprintEnd = t.Sprint.SprintEnd,
                        GoalDescription = t.Sprint.GoalDescription,
                    },
                })
                .Where(t => t.Sprint.SprintId == sprintId)
                .ToListAsync();
        }

        public async Task<ProjectTaskDto> GetByIdAsync(int id)
        {
            return await _forgeDbContext.ProjectTasks
                 .Include(pt => pt.Attachments)
                 .Include(pt => pt.CreatedByNavigation)
                 .Include(pt => pt.Comments)
                 .ThenInclude(c => c.WrittenByNavigation)
                 .Include(pt => pt.TaskStatus)
                 .Include(pt => pt.TaskType)
                 .Include(pt => pt.Team)
                 .Include(pt => pt.UsersTasks)
                .ThenInclude(ut => ut.User)
                .Include(pt => pt.UsersTasks)
                 .Where(pt => pt.TaskId == id)
                 .Select(t => new ProjectTaskDto
                 {
                     TaskId = t.TaskId,
                     TaskName = t.TaskName,
                     TaskStatusId = t.TaskStatusId,
                     TeamId = t.TeamId,
                     CreatedBy = t.CreatedBy,
                     CreatedAt = t.CreatedAt,
                     TaskDeadline = t.TaskDeadline,
                     TaskTypeId = t.TaskTypeId,
                     TaskDescription = t.TaskDescription,
                     Attachments = t.Attachments.Select(a => new TaskAttachmentsDto
                     {
                         AttachmentId = a.AttachmentId,
                         TaskId = a.TaskId,
                         AddedBy = a.AddedBy,
                         FilePath = a.FilePath
                     }).ToList(),
                     Comments = t.Comments.Select(c => new TaskCommentDto
                     {
                         CommentId = c.CommentId,
                         TaskId = c.TaskId,
                         WrittenBy = c.WrittenBy,
                         CommentText = c.CommentText,
                         WrittenAt = c.WrittenAt,
                         WrittenByNavigation = new TaskCommentUserDto
                         {
                             UserId = c.WrittenByNavigation.UserId,
                             Username = c.WrittenByNavigation.Username,
                         },
                     }).ToList(),
                     CreatedByNavigation = new TaskUserGetDto
                     {
                         UserId = t.CreatedByNavigation.UserId,
                         Username = t.CreatedByNavigation.Username,
                         Email = t.CreatedByNavigation.Email,
                         LastLogin = t.CreatedByNavigation.LastLogin,
                     },
                     TaskStatus = t.TaskStatus,
                     TaskType = t.TaskType,
                     UsersTasks = t.UsersTasks.Select(ut => new UserTaskDto
                     {
                         UserTaskId = ut.UserTaskId,
                         UserId = ut.UserId,
                         TaskId = ut.TaskId,
                         User = new TaskUserGetDto
                         {
                             UserId = ut.User.UserId,
                             Username = ut.User.Username,
                             Email = ut.User.Email,
                             LastLogin = ut.User.LastLogin,
                         },
                     }).ToList()
                 })
                 .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _forgeDbContext.ProjectTasks
                .Include(pt => pt.Attachments)
                .Include(pt => pt.Comments)
                .ThenInclude(c => c.WrittenByNavigation)
                .Include(pt => pt.CreatedByNavigation)
                .Include(pt => pt.TaskStatus)
                .Include(pt => pt.TaskType)
                .Include(pt => pt.Team)
                .Include(pt => pt.UsersTasks)
                .ThenInclude(ut => ut.User)
                .Include(pt => pt.UsersTasks)
                .Where(t => t.TaskId == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _forgeDbContext.ProjectTasks.Remove(entity);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"ProjectTask with id {id} not found.");
            }
        }

        public async Task InsertAsync(ProjectTask projectTask, List<int> userIds)
        {
            await _forgeDbContext.ProjectTasks
                .AddAsync(projectTask);

            await _forgeDbContext.SaveChangesAsync();

            for (int i = 0; i < userIds.Count; i++)
            {
                bool exists = await _forgeDbContext.UsersTasks
                    .AnyAsync(ut => ut.TaskId == projectTask.TaskId && ut.UserId == userIds[i]);
                if (!exists)
                {
                    // Add the new UsersTask if it does not exist
                    await _forgeDbContext.UsersTasks.AddAsync(new UsersTask
                    {
                        TaskId = projectTask.TaskId,
                        UserId = userIds[i]
                    });
                }
            }

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTask projectTask)
        {
            _forgeDbContext
                .Update(projectTask);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task AddUsersToTaskAsync(int taskId, List<int> userIds)
        {
            bool isTask = await _forgeDbContext.ProjectTasks
                    .AnyAsync(t => t.TaskId == taskId );

            if (isTask)
            {
                for (int i = 0; i < userIds.Count; i++)
                {
                    bool exists = await _forgeDbContext.UsersTasks
                        .AnyAsync(ut => ut.TaskId == taskId && ut.UserId == userIds[i]);
                    if (!exists)
                    {
                        await _forgeDbContext.UsersTasks.AddAsync(new UsersTask
                        {
                            TaskId = taskId,
                            UserId = userIds[i]
                        });
                    }
                }
                await _forgeDbContext.SaveChangesAsync();
            }
            
        }

        public async Task<ICollection<TaskUserGetDto>> GetTaskUsersByTaskIdAsync(int taskId)
        {
            return await _forgeDbContext.UsersTasks
                .Include(ut => ut.User)
                .Where(ut => ut.Task.TaskId == taskId)
                .Select(ut => new TaskUserGetDto
                {
                    UserId = ut.User.UserId,
                    Username = ut.User.Username,
                    Email = ut.User.Email,
                    LastLogin = ut.User.LastLogin,
                })
                .ToListAsync();
        }
    }
}
