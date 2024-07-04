using Domain.Model;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; }

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    public virtual ICollection<User> Users { get; } = new List<User>();
    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
}
