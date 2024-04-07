﻿using System;
using System.Collections.Generic;

namespace Domain.Data;

public partial class ProjectTask
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public int TaskStatusId { get; set; }

    public int TeamId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime TaskDeadline { get; set; }

    public int TaskTypeId { get; set; }

    public string? TaskDescription { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ProjectTaskStatus TaskStatus { get; set; } = null!;

    public virtual ProjectTaskType TaskType { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<UsersTask> UsersTasks { get; set; } = new List<UsersTask>();
}
