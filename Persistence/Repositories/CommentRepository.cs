using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskForgeDbContext _taskForgeDbContext;
        public CommentRepository(TaskForgeDbContext context) 
        {
            _taskForgeDbContext = context;
        }

        public async Task<IEnumerable<CommentDto>> GetAllTaskCommentsAsync(int taskId) 
        {
            return await _taskForgeDbContext.Comments
                .Include(c => c.Task)
                .Include(c => c.WrittenByNavigation)
                .Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    TaskId = c.TaskId,
                    WrittenBy = c.WrittenBy,
                    CommentText = c.CommentText,
                    WrittenAt = c.WrittenAt,
                    WrittenByNavigation = new CommentUserDto
                    {
                        UserId = c.WrittenByNavigation.UserId,
                        Username = c.WrittenByNavigation.Username,
                        Email = c.WrittenByNavigation.Email,
                    }
                })
                .Where(c => c.TaskId == taskId)
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _taskForgeDbContext.Comments.AddAsync(comment);

            await _taskForgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _taskForgeDbContext.Comments
                .Update(comment);

            await _taskForgeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int commentId)
        {
            var entity = await _taskForgeDbContext.Comments
                .Include(c => c.Task)
                .Where(t => t.CommentId == commentId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _taskForgeDbContext.Comments.Remove(entity);
                await _taskForgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Comment with id {commentId} not found.");
            }
        }

    }
}
