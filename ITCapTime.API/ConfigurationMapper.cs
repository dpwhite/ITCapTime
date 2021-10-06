using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITCapTime.Data;

namespace ITCapTime.API
{
    public static class ConfigurationMapper
    {
        public static MapperConfiguration config;

        public static void ConfigureMapper()
        {
            config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.ProjectType, act => act.MapFrom(src => src.ProjectType != ProjectType.Capital ? ProjectType.Capital : ProjectType.Expense))
            );
        }
    }
}
