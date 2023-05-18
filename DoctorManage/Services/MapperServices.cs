using AutoMapper;
using DoctorManage.Models.Database;
using DoctorManage.Models.DoctorManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorManage.Services
{
    public class MapperServices
    {

        public static Mapper InitializeAutomapper()
        {


            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<DOCTORMODEL, DoctorViewModel>()

                .ForMember(dest => dest.DOCTORGENDER, act => act.MapFrom(src => src.DOCTORGENDER == true? "Male" : "Female" ))

                .ForMember(dest => dest.DEPARTMENT, act => act.MapFrom(src => src.DEPARTMENT != null ? src.DEPARTMENT.DEPARTMENTNAME : "không có"))
                .ForMember(dest => dest.DOCTORDATEOFBIRTH, act => act.MapFrom(src => src.DOCTORDATEOFBIRTH.ToShortDateString()))
                .ForMember(dest => dest.WORKINGENDDATE, act => act.MapFrom(src => src.WORKINGENDDATE.ToShortDateString()))
                .ForMember(dest => dest.WORKINGSTARTDATE, act => act.MapFrom(src => src.WORKINGSTARTDATE.ToShortDateString()))
                .ForMember(dest => dest.CREATEDATE, act => act.MapFrom(src => src.CREATEDATE.ToShortDateString()))
                .ForMember(dest => dest.UPDATEDATE, act => act.MapFrom(src => src.UPDATEDATE.ToShortDateString()))
                  ;

            });

            var mapper = new Mapper(config);
            return mapper;

        }
    }
}