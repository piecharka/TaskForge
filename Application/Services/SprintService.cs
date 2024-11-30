using Application.Interfaces.Services;
using AutoMapper;
using Domain.DTOs;
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
        private readonly IMapper _mapper;

        public SprintService(ISprintRepository sprintRepository, IMapper mapper, ISprintEventRepository sprintEventRepository)
        {
            _sprintRepository = sprintRepository;
            _mapper = mapper;
            _sprintEventRepository = sprintEventRepository;
        }

        public async Task<Sprint> GetSprintByIdAsync(int sprintId)
        {
            return await _sprintRepository.GetSprintByIdAsync(sprintId);
        }

        public async Task<SprintDto> GetCurrentSprintTeamAsync(int teamId)
        {
            return await _sprintRepository.GetCurrentTeamSprintAsync(teamId);
        }

        public async Task<SprintDto> GetPreviousSprintAsync(int teamId)
        {
            return await _sprintRepository.GetPreviousTeamSprintAsync(teamId);
        }

        public async Task<IEnumerable<SprintDto>> GetSprintsAsync(int teamId)
        {
            return await _sprintRepository.GetTeamSprintsAsync(teamId);
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

        public async Task DeleteSprintAsync(int sprintId)
        {
            await _sprintRepository.DeleteSprintAsync(sprintId);
        }
    }
}
