using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAttachmentService
    {
        Task<Attachment> AddAttachmentAsync(IFormFile file, int taskId, int addedBy);
        Task<Attachment> GetAttachmentByIdAsync(int id);
        Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId);
    }
}
