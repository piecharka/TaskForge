using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AttachmentDto
    {
        public int AttachmentId { get; set; }

        public int TaskId { get; set; }

        public int AddedBy { get; set; }

        public string FilePath { get; set; }
    }
}
