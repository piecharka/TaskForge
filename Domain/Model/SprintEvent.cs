using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SprintEvent
    {
        public int SprintEventId { get; set; }
        public string SprintEventName { get; set; }
        public int SprintId { get; set; }
        public int TeamId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime SprintEventDate { get; set; }
        public int SprintEventTypeId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual User CreatedByNavigation { get; set; }
        public virtual SprintEventType SprintEventType { get; set; }
    }
}
