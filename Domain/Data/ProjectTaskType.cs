using System;
using System.Collections.Generic;

namespace Domain;

public partial class ProjectTaskType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
}
