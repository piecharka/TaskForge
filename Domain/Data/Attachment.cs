using System;
using System.Collections.Generic;

namespace Domain.Data;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public int TaskId { get; set; }

    public int AddedBy { get; set; }

    public string FilePath { get; set; } = null!;

    public virtual User AddedByNavigation { get; set; } = null!;

    public virtual ProjectTask Task { get; set; } = null!;
}
