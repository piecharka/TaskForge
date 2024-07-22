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
                 .Include(t => t.CreatedByNavigation)
                 .Include(t => t.TaskStatus)
                 .Include(t => t.TaskType)
                 .Include(t => t.Team)
                 .Include(t => t.UsersTasks)
                 .ThenInclude(ut => ut.User)
                 .Include(t => t.UsersTasks)
                 .ThenInclude(ut => ut.TimeLogs)
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
                         WrittenAt = c.WrittenAt
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
                         TimeLogs = ut.TimeLogs.Select(tl => new TimeLogDto
                         {
                             LogId = tl.LogId,
                             UserTaskId = tl.LogId,
                             StartTime = tl.StartTime,
                             EndTime = tl.EndTime
                         }).ToList()
                     }).ToList()
                 })
                 .Where(pt => pt.TeamId == teamId)
                 .ToListAsync();
        }

        public async Task<ICollection<ProjectTask>> GetAllTasksByUserIdAsync(int userId)
        {
            return await _forgeDbContext.UsersTasks
                .Include(ut => ut.User)
                .Include(ut => ut.TimeLogs)
                .Where(ut => ut.UserId == userId)
                .Select(ut => ut.Task)
                .ToListAsync();
        }

        public async Task<ICollection<ProjectTask>> GetAllTasksByUsernameAsync(string username)
        {
            return await _forgeDbContext.UsersTasks
                .Include(ut => ut.User)
                .Include(ut => ut.TimeLogs)
                .Where(ut => ut.User.Username == username)
                .Select(ut => ut.Task)
                .ToListAsync();
        }

        public async Task<ProjectTaskDto> GetByIdAsync(int id)
        {
            return await _forgeDbContext.ProjectTasks
                 .Include(pt => pt.Attachments) 
                 .Include(pt => pt.Comments)
                 .Include(pt => pt.CreatedByNavigation)
                 .Include(pt => pt.TaskStatus)
                 .Include(pt => pt.TaskType)
                 .Include(pt => pt.Team)
                 .Include(pt => pt.UsersTasks)
                .ThenInclude(ut => ut.User)
                .Include(pt => pt.UsersTasks)
                .ThenInclude(ut => ut.TimeLogs)
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
                         WrittenAt = c.WrittenAt
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
                         TimeLogs = ut.TimeLogs.Select(tl => new TimeLogDto
                         {
                             LogId = tl.LogId,
                             UserTaskId = tl.LogId,
                             StartTime = tl.StartTime,
                             EndTime = tl.EndTime
                         }).ToList()
                     }).ToList()
                 })
                 .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _forgeDbContext.ProjectTasks
                .Include(pt => pt.Attachments)
                .Include(pt => pt.Comments)
                .Include(pt => pt.CreatedByNavigation)
                .Include(pt => pt.TaskStatus)
                .Include(pt => pt.TaskType)
                .Include(pt => pt.Team)
                .Include(pt => pt.UsersTasks)
                .ThenInclude(ut => ut.User)
                .Include(pt => pt.UsersTasks)
                .ThenInclude(ut => ut.TimeLogs)
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

        public async Task InsertAsync(ProjectTask projectTask)
        {
            await _forgeDbContext.ProjectTasks
                .AddAsync(projectTask);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTask projectTask)
        {
            _forgeDbContext
                .Update(projectTask);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TaskUserGetDto>> GetTaskUsersByTaskIdAsync(int taskId)
        {
            return await _forgeDbContext.UsersTasks
                .Include(ut => ut.User)
                .Include(ut => ut.TimeLogs)
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
