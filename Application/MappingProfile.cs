using Application.DTOs;
using AutoMapper;
using Domain;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserCreateDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<TeamDto, Team>();
            CreateMap<ProjectTaskInsertDto, ProjectTask>();
            CreateMap<Team, TeamGetDto>();
            CreateMap<ProjectTask, UsersTasksToDoDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<CommentInsertDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
        }
    }
}
