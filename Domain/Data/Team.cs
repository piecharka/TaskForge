using System;
using System.Collections.Generic;

namespace Domain.Data;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
