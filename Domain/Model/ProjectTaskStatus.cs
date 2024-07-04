using System;
using System.Collections.Generic;

namespace Domain;
public partial class ProjectTaskStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
