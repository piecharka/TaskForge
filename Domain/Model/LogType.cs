using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class LogType
    {
        public int LogTypeId { get; set; }
        public string LogTypeName { get; set; }
        public virtual ICollection<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
