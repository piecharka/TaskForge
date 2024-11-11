using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SprintEventTypeService : ISprintEventTypeService
    {
        private readonly ISprintEventTypeRepository _sprintEventTypeRepository;
        public SprintEventTypeService(ISprintEventTypeRepository sprintEventTypeRepository) 
        {
            _sprintEventTypeRepository = sprintEventTypeRepository;
        }

        public async Task<IEnumerable<SprintEventType>> GetAllEventTypes()
        {
            return await _sprintEventTypeRepository.GetAllAsync();
        }
    }
}
