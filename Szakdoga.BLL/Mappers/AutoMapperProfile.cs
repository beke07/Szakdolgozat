using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdoga.BLL.Models;
using Szakdoga.DAL;
using Szakdoga.Model;

namespace Szakdoga.BLL.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserData>()
                .ForMember(e => e.Password, opt => opt.Ignore())
                .ForMember(e => e.IsAdmin, opt => opt.MapFrom(e => e.IsAdmin))
                .ForMember(e => e.RankName, opt => opt.MapFrom(e => e.Rank.Name))
                .ReverseMap();

            CreateMap<User, UserHierarchyDetailsData>()
                .ForMember(e => e.BossId, opt => opt.MapFrom(e => e.BossId))
                .ForMember(e => e.ProfilePicture, opt => opt.MapFrom(e => e.ProfilePicture))
                .ForMember(e => e.Employees, opt => opt.MapFrom(e => e.Employees));

            CreateMap<Project, ProjectData>()
                .ForMember(e => e.Deadline, opt => opt.MapFrom(e => e.Deadline))
                .ForMember(e => e.Title, opt => opt.MapFrom(e => e.Title))
                .ForMember(e => e.SumWorkHours, opt => opt.MapFrom(e => e.SumWorkHours))
                .ForMember(e => e.Workers, opt => opt.MapFrom(e => e.Workers.Select(w => w.WorkerId)));

            CreateMap<ProjectData, Project>()
                .ForMember(e => e.Workers, opt => opt.Ignore());

            CreateMap<Project, ProjectDetailsData>()
                .IncludeBase<Project, ProjectData>()
                .ForMember(e => e.SumTaskHours, opt => opt.MapFrom(e => e.Tasks.SelectMany(t => t.TaskActivities).Sum(t => t.Hours)))
                .ForMember(e => e.WorkersData, opt => opt.Ignore());

            CreateMap<ProjectTask, TaskData>()
                .ForMember(e => e.BeingInChargeOfName, opt => opt.MapFrom(e => e.Worker.FirstName + " " + e.Worker.LastName));

            CreateMap<TaskData, ProjectTask>();

            CreateMap<TaskActivity, ActivityData>()
                .ForMember(e => e.ActivityName, opt => opt.MapFrom(e => e.Activity.Name));

            CreateMap<ActivityData, TaskActivity>();

            CreateMap<ProjectTask, TaskDetailsData>()
                .IncludeBase<ProjectTask, TaskData>()
                .ForMember(e => e.Activities, opt => opt.MapFrom(e => e.TaskActivities));
        }
    }
}
