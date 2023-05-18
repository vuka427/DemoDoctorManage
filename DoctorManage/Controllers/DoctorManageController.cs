using AutoMapper;
using DoctorManage.Models.Database;
using DoctorManage.Models.DataTableModel;
using DoctorManage.Models.DoctorManage;
using DoctorManage.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorManage.Controllers
{
    public class DoctorManageController : Controller
    {
        private readonly DoctorDbContainer _dbContext;

        public DoctorManageController(DoctorDbContainer dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: DoctorManage
        public ActionResult Index()
        {
            var date = DateTime.Now;
            var dt = new List<DOCTORMODEL>();
            var de = _dbContext.DEPARTMENT.FirstOrDefault();

            dt.Add(new DOCTORMODEL()
            {
                DOCTORNAME = "Bác sĩ B",
                DEPARTMENTID = 1,
                DEPARTMENT = de,
                DOCTORADDRESS = "Cần thơ",
                DOCTORGENDER = true,
                DOCTORDATEOFBIRTH = date,
                DOCTORMOBILENO = "0123456789",
                WORKINGENDDATE = date,
                WORKINGSTARTDATE = date,
                CREATEBY = "vũ",
                CREATEDATE = date,
                UPDATEBY = "vũ",
                UPDATEDATE = date,
            }); ;

            // _dbContext.DOCTOR.AddRange(dt);
            //_dbContext.SaveChanges();

            return View();
        }
        
        public ActionResult LoadDoctorData(JqueryDatatableParam param)
        {
           var mapper = MapperServices.InitializeAutomapper();
            var doctor =_dbContext.DOCTOR.Include("DEPARTMENT").ToList();
            IEnumerable<DoctorViewModel> Doctors = doctor.Select(dt => mapper.Map<DOCTORMODEL, DoctorViewModel>(dt)).ToList();





            if (!string.IsNullOrEmpty(param.sSearch)) //tìm kiếm
            {
                Doctors = Doctors.Where(x => x.DOCTORNAME.ToLower().Contains(param.sSearch.ToLower())
                                              || x.DOCTORID.ToString().Contains(param.sSearch.ToLower())
                                              || x.DOCTORGENDER.ToLower().Contains(param.sSearch.ToLower())
                                              || x.DOCTORDATEOFBIRTH.ToLower().Contains(param.sSearch.ToLower())
                                              || x.DOCTORMOBILENO.ToString().Contains(param.sSearch.ToLower())
                                              || x.DOCTORADDRESS.ToLower().Contains(param.sSearch.ToLower())
                                               ).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 0)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.DOCTORID) : Doctors.OrderByDescending(c => c.DOCTORID);
            }
            else if (sortColumnIndex == 3)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.DOCTORDATEOFBIRTH) : Doctors.OrderByDescending(c => c.DOCTORDATEOFBIRTH);
            }
            else if (sortColumnIndex == 7)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.WORKINGSTARTDATE) : Doctors.OrderByDescending(c => c.WORKINGSTARTDATE);
            }
            else if (sortColumnIndex == 8)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.WORKINGENDDATE) : Doctors.OrderByDescending(c => c.WORKINGENDDATE);
            }
            else if (sortColumnIndex == 10)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.CREATEDATE) : Doctors.OrderByDescending(c => c.CREATEDATE);
            }
            else if (sortColumnIndex == 12)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.UPDATEDATE) : Doctors.OrderByDescending(c => c.CREATEDATE);
            }
            else
            {
                Func<DoctorViewModel, string> orderingFunction = e =>
                                                           sortColumnIndex == 1 ? e.DOCTORNAME :
                                                           sortColumnIndex == 2 ? e.DOCTORGENDER :
                                                           sortColumnIndex == 4 ? e.DOCTORMOBILENO :
                                                           sortColumnIndex == 5 ? e.DOCTORADDRESS :
                                                           sortColumnIndex == 6 ? e.DEPARTMENT :
                                                           sortColumnIndex == 9 ? e.CREATEBY :
                                                           e.UPDATEBY;//11

                Doctors = sortDirection == "asc" ? Doctors.OrderBy(orderingFunction) : Doctors.OrderByDescending(orderingFunction);

            }


            var displayResult = Doctors.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = Doctors.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);

        }
    }
}