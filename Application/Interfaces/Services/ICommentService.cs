using Application.DTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetTasksCommentsAsync(int taskId);
        Task DeleteCommentAsync(int id);
        Task AddCommentAsync(CommentInsertDto commentInsert);
        Task UpdateCommentAsync(int commentId, CommentUpdateDto commentUpdate);
    }
}
