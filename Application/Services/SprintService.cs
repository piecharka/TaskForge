using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly ISprintEventRepository _sprintEventRepository;
        private readonly ITimeLogRepository _timeLogRepository;
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;

        public SprintService(ISprintRepository sprintRepository, 
            IMapper mapper, 
            ISprintEventRepository sprintEventRepository, 
            ITimeLogRepository timeLogRepository,
            IProjectTaskRepository projectTaskRepository)
        {
            _sprintRepository = sprintRepository;
            _mapper = mapper;
            _sprintEventRepository = sprintEventRepository;
            _timeLogRepository = timeLogRepository;
            _projectTaskRepository = projectTaskRepository;
        }

        public async Task<Sprint> GetSprintByIdAsync(int sprintId)
        {
            return await _sprintRepository.GetSprintByIdAsync(sprintId);
        }

        public async Task<SprintDto> GetCurrentSprintTeamAsync(int teamId)
        {
            var sprint = await _sprintRepository.GetCurrentTeamSprintAsync(teamId);
            
            return _mapper.Map<Sprint, SprintDto>(sprint);
        }

        public async Task<SprintDto> GetPreviousSprintAsync(int teamId)
        {
            var sprint = await _sprintRepository.GetPreviousTeamSprintAsync(teamId);
        
            return _mapper.Map<Sprint,SprintDto>(sprint);
        }

        public async Task<IEnumerable<SprintDto>> GetSprintsAsync(int teamId)
        {
            var sprints = await _sprintRepository.GetTeamSprintsAsync(teamId);
        
            return _mapper.Map<IEnumerable<Sprint> , IEnumerable<SprintDto>>(sprints);
        }

        public async Task AddSprintAsync(SprintDto sprintDto)
        {
            var sprint = _mapper.Map<Sprint>(sprintDto);

            await _sprintRepository.AddSprintAsync(sprint);

            if (sprintDto.SprintPlanning.HasValue)
            {
                await _sprintEventRepository.AddSprintEventAsync(new SprintEvent
                {
                    SprintEventName = sprintDto.SprintName + " Sprint planning",
                    SprintEventDate = sprintDto.SprintPlanning.Value,
                    SprintId = sprint.SprintId,
                    TeamId = sprintDto.TeamId,
                    CreatedBy = sprintDto.CreatedBy,
                    SprintEventTypeId = 2,
                });
            }

            if (sprintDto.SprintReview.HasValue)
            {
                await _sprintEventRepository.AddSprintEventAsync(new SprintEvent
                {
                    SprintEventName = sprintDto.SprintName + " Sprint review",
                    SprintEventDate = sprintDto.SprintReview.Value,
                    SprintId = sprint.SprintId,
                    TeamId = sprintDto.TeamId,
                    CreatedBy = sprintDto.CreatedBy,
                    SprintEventTypeId = 3,
                });
            }


            for (DateTime date = sprintDto.SprintStart; date <= sprintDto.SprintEnd; date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        continue;
                    }

                    DateTime eventDate = new DateTime(date.Year, date.Month, date.Day,
                        sprintDto.SprintStart.Hour, sprintDto.SprintStart.Minute, sprintDto.SprintStart.Second);

                    await _sprintEventRepository.AddSprintEventAsync(new SprintEvent
                        {
                            SprintEventName = sprintDto.SprintName + " Daily scrum",
                            SprintEventDate = eventDate,
                            SprintId = sprint.SprintId,
                            TeamId = sprintDto.TeamId,
                            CreatedBy = sprintDto.CreatedBy,
                            SprintEventTypeId = 1,
                    });
                }
            

            if (sprintDto.SprintRetro.HasValue) 
            {
                await _sprintEventRepository.AddSprintEventAsync(new SprintEvent
                {
                    SprintEventName = sprintDto.SprintName + " Sprint review",
                    SprintEventDate = sprintDto.SprintRetro.Value,
                    SprintId = sprint.SprintId,
                    TeamId = sprintDto.TeamId,
                    CreatedBy = sprintDto.CreatedBy,
                    SprintEventTypeId = 3,
                });
            }
        }

        public async Task<ICollection<SprintTaskCountDto>> GetTaskCountPerSprintDayAsync(int sprintId)
        {
            var sprint = await _sprintRepository.GetSprintByIdAsync(sprintId);
            var tasks = await _projectTaskRepository.GetAllTasksBySprintIdAsync(sprintId);

            var tasksDto = _mapper.Map<ICollection<ProjectTask>, ICollection<ProjectTaskDto>>(tasks);

            var sprintTaskCount = new List<SprintTaskCountDto>();
            var taskCount = tasks.Count();
            var doneTasks = new List<int>();
            for(var date = sprint.SprintStart; date <= sprint.SprintEnd; date = date.AddDays(1))
            {
                var timeLogs = await _timeLogRepository.GetSprintTimeLogByDateAsync(date, sprintId, 2);
                
                foreach(var timelog in timeLogs)
                {
                    if(IsInTaskList(tasksDto, timelog.TaskId) && !doneTasks.Contains(timelog.TaskId))
                    {
                        taskCount--;
                        doneTasks.Add(timelog.TaskId);
                    }
                }

                sprintTaskCount.Add(new SprintTaskCountDto
                {
                    Day = date,
                    TasksRemaining = taskCount,
                });
            }

            return sprintTaskCount;
        }

        public async Task DeleteSprintAsync(int sprintId)
        {
            await _sprintRepository.DeleteSprintAsync(sprintId);
        }

        private bool IsInTaskList(ICollection<ProjectTaskDto> projectTaskList, int taskId)
        {
            foreach(var task in projectTaskList)
            {
                if (task.TaskId == taskId) return true;
            }

            return false;
        }
    }
}
