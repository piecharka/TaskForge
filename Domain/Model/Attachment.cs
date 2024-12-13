using System;
using System.Collections.Generic;

namespace Domain;

public class Attachment
{
    public int AttachmentId { get; set; }

    public int TaskId { get; set; }

    public int AddedBy { get; set; }

    public string FilePath { get; set; }

    public virtual User AddedByNavigation { get; set; }

    public virtual ProjectTask Task { get; set; }
}
