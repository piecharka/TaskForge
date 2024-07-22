using Application.Interfaces.Services;
using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetTasksCommentsAsync(int taskId)
        {
            return await _commentRepository.GetAllTaskCommentsAsync(taskId);
        }
    }
}
