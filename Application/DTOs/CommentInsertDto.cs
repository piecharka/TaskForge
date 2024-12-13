

namespace Application.DTOs
{
    public class CommentInsertDto
    {
        public int CommentId { get; set; }

        public int TaskId { get; set; }

        public int WrittenBy { get; set; }

        public string CommentText { get; set; }
    }
}
