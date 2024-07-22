using Domain;
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

        public async Task<IEnumerable<Comment>> GetAllTaskCommentsAsync(int taskId) 
        {
            return await _taskForgeDbContext.Comments
                .Include(c => c.Task)
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
                .Include(t => t.Task)
                .Where(t => t.CommentId == commentId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _taskForgeDbContext.Comments.Remove(entity);
                await _taskForgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"Comment with id {commentId} not found.");
            }
        }

    }
}
