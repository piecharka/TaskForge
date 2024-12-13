using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly ISprintRepository _sprintRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITimeLogRepository _timeLogRepository;

        public AnalyticsService(IProjectTaskRepository projectTaskRepository, 
            ISprintRepository sprintRepository, 
            IUserRepository userRepository,
            ITimeLogRepository timeLogRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _sprintRepository = sprintRepository;
            _userRepository = userRepository;
            _timeLogRepository = timeLogRepository;
        }

        public async Task<int> AverageTasksCountPerSprint(int teamId)
        {
            var sprints = await _sprintRepository.GetTeamSprintsAsync(teamId);

            int sum = 0;
            foreach(var sprint in sprints)
            {
                sum += sprint.ProjectTasks.Count();
            }

            return sum / sprints.Count();
        }
        
        public async Task<int> AverageTaskCountPerUser(int teamId)
        {
            var users = await _userRepository.GetTeamUsersAsync(teamId);

            int sum = 0;
            foreach(var user in users)
            {
                sum += user.ProjectTasks.Where(t => t.TeamId == teamId).Count();
            }

            return sum / users.Count();
        }

        public async Task<double> AverageTimeForTask(int teamId)
        {
            var tasks = await _projectTaskRepository.GetAllInTeamAsync(teamId);

            var completedTasks = tasks.Where(task => task.TaskStatusId == 2).ToList();

            if (!completedTasks.Any())
            {
                return 0;
            }

            var timeDifferences = new List<TimeSpan>();
            foreach (var task in completedTasks)
            {
                var timelog = await _timeLogRepository.GetTimeLogByDoneTaskIdAsync(task.TaskId);

                if (timelog != null)
                {
                    timeDifferences.Add(timelog.LogDate - task.CreatedAt);
                }
            }

            if (!timeDifferences.Any())
            {
                return 0;
            }

            var averageTime = timeDifferences.Average(t => t.TotalDays);
            return averageTime;
        }
    }
}
