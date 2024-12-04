using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; }

        public int NotificationStatusId { get; set; }

        public virtual NotificationStatusDto NotificationStatus { get; set; }
    }
}
