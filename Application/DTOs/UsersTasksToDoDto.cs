﻿using Domain;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UsersTasksToDoDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int TaskStatusId { get; set; }


        public int CreatedBy { get; set; }

        public DateTime TaskDeadline { get; set; }

        public int TaskTypeId { get; set; }

        public string TaskDescription { get; set; }

        public TeamGetDto Team{ get; set; }
        public ProjectTaskStatus TaskStatus { get; set; }
    }

    public class TeamGetDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
