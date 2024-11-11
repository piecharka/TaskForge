using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Sprint
    {
        public int SprintId { get; set; }
        public int TeamId { get; set; }
        public string SprintName { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set;}
        public string GoalDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}

        public virtual Team Team { get; set; }
        public virtual List<ProjectTask> ProjectTasks { get; set; }
        public virtual List<SprintEvent> SprintEvents { get; set; }

    }
}
