using System;
using System.Collections.Generic;

namespace Domain;

public partial class UsersTask
{
    public int UserTaskId { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }

    public virtual ProjectTask Task { get; set; }

    public virtual User User { get; set; }
}
