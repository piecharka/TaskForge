
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllTaskCommentsAsync(int taskId);
        Task AddCommentAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int commentId);
    }
}
