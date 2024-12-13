using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public AttachmentRepository(TaskForgeDbContext forgeDbContext) 
        {
            _forgeDbContext = forgeDbContext;
        }
        public async Task<Attachment> AddAttachmentAsync(IFormFile file, int taskId, int addedBy)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Brak pliku do przesłania.");
            }

            // Uzyskujemy ścieżkę do katalogu głównego projektu (w przykładzie D:\code\taskforge)
            var projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            // Ścieżka do folderu "Uploads" w katalogu Persistence
            var persistenceFolder = Path.Combine(projectDirectory, "Persistence", "Uploads");

            // Sprawdzenie czy folder istnieje, jeśli nie to go tworzymy
            if (!Directory.Exists(persistenceFolder))
            {
                Directory.CreateDirectory(persistenceFolder);
            }

            // Nazwa pliku z unikalnym identyfikatorem
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(persistenceFolder, uniqueFileName);

            // Zapisz plik na dysku
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Zapisz metadane pliku w bazie danych
            var attachment = new Attachment
            {
                FilePath = filePath,
                TaskId = taskId,
                AddedBy = addedBy,
            };

            // Zapisz dane w bazie danych
            _forgeDbContext.Attachments.Add(attachment);
            await _forgeDbContext.SaveChangesAsync();

            return attachment;
        }


        public async Task<Attachment> GetAttachmentByIdAsync(int id)
        {
            return await _forgeDbContext.Attachments
                .Include(a => a.Task) 
                .Include(a => a.AddedByNavigation)  
                .FirstOrDefaultAsync(a => a.AttachmentId == id);
        }

        public async Task<IEnumerable<Attachment>> GetAttachmentsByTaskIdAsync(int taskId)
        {
            return await _forgeDbContext.Attachments
                .Include(a => a.Task)
                .Include(a => a.AddedByNavigation)
                .Where(a => a.TaskId == taskId)
                .ToListAsync();
        }
    }
}
