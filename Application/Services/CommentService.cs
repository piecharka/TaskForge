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

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> GetTasksCommentsAsync(int taskId)
        {
            return await _commentRepository.GetAllTaskCommentsAsync(taskId);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        public async Task AddCommentAsync(CommentInsertDto commentInsert)
        {
            var comment = _mapper.Map<CommentInsertDto, Comment>(commentInsert);

            comment.WrittenAt = DateTime.Now;

            await _commentRepository.AddCommentAsync(comment);
        }

        public async Task UpdateCommentAsync(int commentId, CommentUpdateDto commentUpdate)
        {
            var comment = _mapper.Map<CommentUpdateDto, Comment>(commentUpdate);

            await _commentRepository.UpdateAsync(comment);
        }
    }
}
