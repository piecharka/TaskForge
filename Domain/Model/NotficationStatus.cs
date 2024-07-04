using System;
using System.Collections.Generic;

namespace Domain;

public partial class NotficationStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public virtual ICollection<Notfication> Notfications { get; set; } = new List<Notfication>();
}
