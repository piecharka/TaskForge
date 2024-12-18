﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TeamUser
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public int PermissionId { get; set; }
        public User User { get; set; }
        public Team Team { get; set; }
        public Permission Permission { get; set; }
    }
}
