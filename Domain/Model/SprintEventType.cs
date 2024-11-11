using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SprintEventType
    {
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public virtual List<SprintEvent> SprintEvents { get; set; } = new List<SprintEvent>();
    }
}
