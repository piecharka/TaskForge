using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SprintDto
    {
        public int SprintId { get; set; }
        public int TeamId { get; set; }
        public string SprintName { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set; }
        public string GoalDescription { get; set; }
        public DateTime? SprintPlanning { get; set; }
        public DateTime? SprintReview { get; set; }
        public DateTime? SprintRetro { get; set; }
        public int CreatedBy { get; set; }
    }
}
