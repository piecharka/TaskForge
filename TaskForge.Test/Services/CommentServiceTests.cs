using Application.Services;
using Application.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Model;
using Moq;
using NUnit.Framework;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace TaskForge.Test
{
    public class CommentServiceTests
    {
        private Mock<ICommentRepository> _mockCommentRepository;
        private Mock<IMapper> _mockMapper;
        private CommentService _commentService;

        [SetUp]
        public void Setup()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockMapper = new Mock<IMapper>();
            _commentService = new CommentService(
                _mockCommentRepository.Object,
                _mockMapper.Object);
        }

        [Test]
        public async Task GetTasksCommentsAsync_ReturnsMappedComments()
        {
            // Arrange
            var taskId = 1;
            var comments = new List<Comment> { new Comment { CommentId = 1, TaskId = taskId }, new Comment { CommentId = 2, TaskId = taskId } };
            var commentDtos = new List<CommentDto> { new CommentDto { CommentId = 1, TaskId = taskId }, new CommentDto { CommentId = 2, TaskId = taskId } };

            _mockCommentRepository.Setup(repo => repo.GetAllTaskCommentsAsync(taskId))
                .ReturnsAsync(comments);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments))
                .Returns(commentDtos);

            // Act
            var result = await _commentService.GetTasksCommentsAsync(taskId);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(commentDtos, result);
        }

        [Test]
        public async Task DeleteCommentAsync_DeletesComment()
        {
            // Arrange
            var commentId = 1;

            _mockCommentRepository.Setup(repo => repo.DeleteAsync(commentId))
                .Returns(Task.CompletedTask);

            // Act
            await _commentService.DeleteCommentAsync(commentId);

            // Assert
            _mockCommentRepository.Verify(repo => repo.DeleteAsync(commentId), Times.Once);
        }

        [Test]
        public async Task AddCommentAsync_AddsCommentWithTimestamp()
        {
            // Arrange
            var commentInsertDto = new CommentInsertDto
            {
                CommentId = 1,
                TaskId = 1,
                WrittenBy = 123,
                CommentText = "Test Comment"
            };

            var comment = new Comment
            {
                CommentId = 1,
                TaskId = 1,
                WrittenBy = 123,
                CommentText = "Test Comment",
                WrittenAt = DateTime.Now
            };

            _mockMapper.Setup(mapper => mapper.Map<CommentInsertDto, Comment>(commentInsertDto))
                .Returns(comment);

            _mockCommentRepository.Setup(repo => repo.AddCommentAsync(comment))
                .Returns(Task.CompletedTask);

            // Act
            await _commentService.AddCommentAsync(commentInsertDto);

            // Assert
            Assert.AreEqual(DateTime.Now.Date, comment.WrittenAt.Date);
            _mockCommentRepository.Verify(repo => repo.AddCommentAsync(comment), Times.Once);
        }

        [Test]
        public async Task UpdateCommentAsync_UpdatesComment()
        {
            // Arrange
            var commentUpdateDto = new CommentUpdateDto
            {
                CommentId = 1,
                CommentText = "Updated Comment"
            };

            var comment = new Comment
            {
                CommentId = 1,
                CommentText = "Updated Comment"
            };

            _mockMapper.Setup(mapper => mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto))
                .Returns(comment);

            _mockCommentRepository.Setup(repo => repo.UpdateAsync(comment))
                .Returns(Task.CompletedTask);

            // Act
            await _commentService.UpdateCommentAsync(comment.CommentId, commentUpdateDto);

            // Assert
            _mockCommentRepository.Verify(repo => repo.UpdateAsync(comment), Times.Once);
        }
    }
}
