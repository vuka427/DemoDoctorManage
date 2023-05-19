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

                cfg.CreateMap<DoctorViewModel, DOCTORMODEL>()

                .ForMember(dest => dest.DOCTORGENDER, act => act.MapFrom(src => src.DOCTORGENDER ==  "Male" ? true : false))
                .ForMember(dest => dest.DEPARTMENTID, act => act.MapFrom(src => src.DEPARTMENTID))
                .ForMember(dest => dest.DOCTORDATEOFBIRTH, act => act.MapFrom(src => src.DOCTORDATEOFBIRTH))
                .ForMember(dest => dest.WORKINGENDDATE, act => act.MapFrom(src => src.WORKINGENDDATE))
                .ForMember(dest => dest.WORKINGSTARTDATE, act => act.MapFrom(src => src.WORKINGSTARTDATE))
                .ForMember(dest => dest.CREATEDATE, act => act.MapFrom(src => src.CREATEDATE))
                .ForMember(dest => dest.UPDATEDATE, act => act.MapFrom(src => src.UPDATEDATE))
                  ;


                cfg.CreateMap<DOCTORMODEL, DoctorEditModel>()
                .ForMember(dest => dest.DOCTORDATEOFBIRTH, act => act.MapFrom(src => src.DOCTORDATEOFBIRTH.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.WORKINGENDDATE, act => act.MapFrom(src => src.WORKINGENDDATE.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.WORKINGSTARTDATE, act => act.MapFrom(src => src.WORKINGSTARTDATE.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.DOCTORGENDER, act => act.MapFrom(src => src.DOCTORGENDER == true ? "Male" : "Female"))
                ;

            });

            var mapper = new Mapper(config);
            return mapper;

        }
    }
}