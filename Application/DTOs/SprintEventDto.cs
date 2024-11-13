using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SprintEventDto
    {
        public int SprintEventId { get; set; }
        public string SprintEventName { get; set; }
        public int SprintId { get; set; }
        public int TeamId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime SprintEventDate { get; set; }
        public int SprintEventTypeId { get; set; }
    }
}
