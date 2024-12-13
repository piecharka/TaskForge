using Application.DTOs;
using AutoMapper;
using Domain;
using Domain.Model;
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
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentInsertDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<SprintEventDto, SprintEvent>();
            CreateMap<SprintDto, Sprint>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationStatus, NotificationStatusDto>();
            CreateMap<User, UserDto>();
            CreateMap<Team, UserTeamDto>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<ProjectTask, ProjectTaskDto>();
            CreateMap<Attachment, AttachmentDto>();
            CreateMap<Team, TeamGetDto>();
            CreateMap<ProjectTaskStatus, ProjectTaskStatusDto>();
            CreateMap<ProjectTaskType, ProjectTaskTypeDto>();
            CreateMap<UsersTask, UserTaskDto>();
            CreateMap<UsersTask, TaskUserDto>();
            CreateMap<Sprint, SprintDto>();
            CreateMap<Sprint, SprintGetDto>();
            CreateMap<User, TaskUserGetDto>();
            CreateMap<User, CommentUserDto>();
            
        }
    }
}
