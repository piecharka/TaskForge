using System;
using System.Collections.Generic;

namespace Domain;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; }

    public DateTime SentAt { get; set; }

    public int NotificationStatusId { get; set; }

    public virtual NotificationStatus NotificationStatus { get; set; }

    public virtual User User { get; set; }
}
