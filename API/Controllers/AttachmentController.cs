using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost("upload/{taskId}")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, int taskId)
        {
            try
            {
                var addedBy = 4; // Załóżmy, że mamy ID użytkownika, który dodał plik (np. z sesji lub tokenu)
                var attachment = await _attachmentService.AddAttachmentAsync(file, taskId, addedBy);
                return Ok(new { attachment.AttachmentId, attachment.FilePath });
            }
            catch (Exception ex)
            {
                return BadRequest($"Błąd przesyłania pliku: {ex.Message}");
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var attachment = await _attachmentService.GetAttachmentByIdAsync(id);
            if (attachment == null)
            {
                return NotFound("Plik nie znaleziony.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(attachment.FilePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(attachment.FilePath));
        }

        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetAttachmentsForTask(int taskId)
        {
            // Pobierz wszystkie załączniki powiązane z danym taskId
            var attachments = await _attachmentService.GetAttachmentsByTaskIdAsync(taskId);
            if (attachments == null || !attachments.Any())
            {
                return NotFound("Brak załączników dla tego zadania.");
            }

            // Możemy zwrócić listę metadanych plików (np. FilePath, FileName)
            var attachmentDetails = attachments.Select(a => new
            {
                a.AttachmentId,
                a.FilePath,
                FileName = Path.GetFileName(a.FilePath)  // Możesz dodać inne metadane
            }).ToList();

            return Ok(attachmentDetails); // Zwróć szczegóły załączników
        }
    }
}
