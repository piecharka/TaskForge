using System;
using System.Collections.Generic;

namespace Domain;

public partial class Notfication
{
    public int NotficationId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; }

    public DateTime SentAt { get; set; }

    public int NotficationStatusId { get; set; }

    public virtual NotficationStatus NotficationStatus { get; set; }

    public virtual User User { get; set; }
}
