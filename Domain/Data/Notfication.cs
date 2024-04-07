using System;
using System.Collections.Generic;

namespace Domain.Data;

public partial class Notfication
{
    public int NotficationId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public int NotficationStatusId { get; set; }

    public virtual NotficationStatus NotficationStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
