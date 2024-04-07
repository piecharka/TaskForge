using System;
using System.Collections.Generic;

namespace Domain.Data;

public partial class ProjectTaskStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
