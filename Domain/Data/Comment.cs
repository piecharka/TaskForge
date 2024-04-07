using System;
using System.Collections.Generic;

namespace Domain.Data;

public partial class Comment
{
    public int CommentId { get; set; }

    public int TaskId { get; set; }

    public int WrittenBy { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime WrittenAt { get; set; }

    public virtual ProjectTask Task { get; set; } = null!;

    public virtual User WrittenByNavigation { get; set; } = null!;
}
