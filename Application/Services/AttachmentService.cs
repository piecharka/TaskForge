using Application.Interfaces.Services;
using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public async Task<Attachment> AddAttachmentAsync(IFormFile file, int taskId, int addedBy)
        {
            return await _attachmentRepository.AddAttachmentAsync(file, taskId, addedBy);
        }
        
        public async Task<Attachment> GetAttachmentByIdAsync(int id)
        {
            return await _attachmentRepository.GetAttachmentByIdAsync(id);
        }

        public async Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId)
        {
            return await _attachmentRepository.GetAttachmentsByTaskIdAsync(taskId);
        }
    }
}
