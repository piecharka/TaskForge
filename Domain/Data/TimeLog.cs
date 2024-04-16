using System;
using System.Collections.Generic;

namespace Domain;

public partial class TimeLog
{
    public int LogId { get; set; }

    public int UserTaskId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual UsersTask UserTask { get; set; } = null!;
}
