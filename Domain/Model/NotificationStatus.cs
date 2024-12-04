using System;
using System.Collections.Generic;

namespace Domain;

public partial class NotificationStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
