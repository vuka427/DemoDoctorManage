using DoctorManage.Models.Database;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace DoctorManage
{
    public static class UnityConfig
    {
       
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, DoctorDbContainer>(TypeLifetime.Transient);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}