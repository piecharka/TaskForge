﻿using Domain;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DTOs
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public ICollection<TeamUserDto> Users { get; set; }
    }

    public class TeamUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

    }
}
