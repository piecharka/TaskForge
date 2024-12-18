﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CommentDto
    {
        public int CommentId { get; set; }

        public int TaskId { get; set; }

        public int WrittenBy { get; set; }

        public string CommentText { get; set; }

        public DateTime WrittenAt { get; set; }
        public virtual CommentUserDto WrittenByNavigation { get; set; }
    }
}
