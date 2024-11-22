using Domain.Model;
using System;
using System.Collections.Generic;

namespace Domain;

public partial class TimeLog
{
    public int LogId { get; set; }

    public int TaskId { get; set; }

    public int LogTypeId { get; set; }

    public DateTime LogDate { get; set; }

    public virtual ProjectTask ProjectTask { get; set; }
    public virtual LogType LogType{ get; set; }
}
