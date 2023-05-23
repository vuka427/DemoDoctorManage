using AutoMapper;
using DoctorManage.Models.Database;
using DoctorManage.Models.DataTableModel;
using DoctorManage.Models.DoctorManage;
using DoctorManage.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
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
            ViewBag.department = new SelectList(_dbContext.DEPARTMENT.ToList(), "DEPARTMENTID", "DEPARTMENTNAME");

            return View();
        }
        
        public ActionResult LoadDoctorData(JqueryDatatableParam param)
        {
           var mapper = MapperServices.InitializeAutomapper();
            var doctor =_dbContext.DOCTOR.Where(d=> d.DELETEFLAG == false).Include("DEPARTMENT").ToList();
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

            if (sortColumnIndex == 1)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.DOCTORID) : Doctors.OrderByDescending(c => c.DOCTORID);
            }
            else if (sortColumnIndex == 4)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.DOCTORDATEOFBIRTH) : Doctors.OrderByDescending(c => c.DOCTORDATEOFBIRTH);
            }
            else if (sortColumnIndex == 8)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.WORKINGSTARTDATE) : Doctors.OrderByDescending(c => c.WORKINGSTARTDATE);
            }
            else if (sortColumnIndex == 9)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.WORKINGENDDATE) : Doctors.OrderByDescending(c => c.WORKINGENDDATE);
            }
            else if (sortColumnIndex == 11)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.CREATEDATE) : Doctors.OrderByDescending(c => c.CREATEDATE);
            }
            else if (sortColumnIndex == 13)
            {
                Doctors = sortDirection == "asc" ? Doctors.OrderBy(c => c.UPDATEDATE) : Doctors.OrderByDescending(c => c.CREATEDATE);
            }
            else
            {
                Func<DoctorViewModel, string> orderingFunction = e =>
                                                           sortColumnIndex == 2 ? e.DOCTORNAME :
                                                           sortColumnIndex == 3 ? e.DOCTORGENDER :
                                                           sortColumnIndex == 5 ? e.DOCTORMOBILENO :
                                                           sortColumnIndex == 6 ? e.DOCTORADDRESS :
                                                           sortColumnIndex == 7 ? e.DEPARTMENT :
                                                           sortColumnIndex == 10 ? e.CREATEBY :
                                                           e.UPDATEBY;//11

                Doctors = sortDirection == "asc" ? Doctors.OrderBy(orderingFunction) : Doctors.OrderByDescending(orderingFunction);
                //asc tăng dần  
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

        [HttpPost]
        public JsonResult CreateDoctor( DoctorViewModel model )
        {
            
            model.DOCTORNAME = model.DOCTORNAME!=null?  model.DOCTORNAME.Trim() : model.DOCTORNAME;
            model.DOCTORADDRESS = model.DOCTORADDRESS!=null?  model.DOCTORADDRESS.Trim() : model.DOCTORADDRESS;

            var formatString  = @"^[\p{L}\p{N}\s]*$"; // \|!#$%&/()=?»«@£§€{}.-;'<>_,


            if (String.IsNullOrEmpty(model.DOCTORNAME))
            {
                
                return Json(new { error = 1, msg =  "Doctor name is not null !"});
            }

            Match strname = Regex.Match(model.DOCTORNAME, formatString, RegexOptions.IgnoreCase);
            if (!strname.Success) 
            {
                return Json(new { error = 1, msg = $"Doctor name does not contain any special characters !" });
            }


            if (model.DOCTORNAME.Length>=50)
            {
                return Json(new { error = 1, msg = "Doctor name charater max lenght is 50!" });
            }
            if (String.IsNullOrEmpty(model.DOCTORGENDER) && (model.DOCTORGENDER != "Male" || model.DOCTORGENDER != "Female"))
            {
                
                return Json(new { error = 1, msg =  "Gender not match!"});
            }

            DateTime dateofbirth ;
            if(!DateTime.TryParse(model.DOCTORDATEOFBIRTH ,out dateofbirth)){
                return Json(new { error = 1, msg = "error date of birth !" });
            }
             
            if (dateofbirth > DateTime.Now)
            {
                return Json(new { error = 1, msg = "Date of birth smaller than current date !" });
            }

            if (!String.IsNullOrEmpty(model.DOCTORMOBILENO)){

                string patternMobile = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";
                Match m = Regex.Match(model.DOCTORMOBILENO, patternMobile, RegexOptions.IgnoreCase);
            
                if (!m.Success) //mobile
                {
                    return Json(new { error = 1, msg = $"Mobile No error !" });
                }  
            }
            else
            {
                return Json(new { error = 1, msg = "Mobile No is not null !" });

            }
            if (String.IsNullOrEmpty(model.DOCTORADDRESS))
            {

                return Json(new { error = 1, msg = "Address is not null !" });
            }
            if (model.DOCTORNAME.Length >= 265)
            {
                return Json(new { error = 1, msg = "Doctor address charater max lenght is 256 !" });
            }

            var mapper = MapperServices.InitializeAutomapper();
            DOCTORMODEL Doctors = mapper.Map<DoctorViewModel, DOCTORMODEL>(model);

            if (Doctors.WORKINGSTARTDATE >= Doctors.WORKINGENDDATE)
            {
                return Json(new { error = 1, msg = "Working start date smaller than Working end date !" });
            }


            var department = _dbContext.DEPARTMENT.Find(Doctors.DEPARTMENTID);
            if(department == null) { return Json(new { error = 1, msg = "Error ! Can`t find Department !" }); }
            Doctors.DEPARTMENT = department;

            var date = DateTime.Now;
            Doctors.CREATEBY = "vũ";
            Doctors.CREATEDATE = date;
            Doctors.UPDATEBY = "vũ";
            Doctors.UPDATEDATE = date;

            _dbContext.DOCTOR.Add(Doctors);
            _dbContext.SaveChanges();


            return Json(new {error = 0 , msg ="ok"});
        }

        [HttpPost]
        public JsonResult DeleteDoctor(int DoctorId)
        {
            if(DoctorId == 0)
            {
                return Json(new { error = 1, msg = "Error! do not delete doctor !" });
            }
            var doctor = _dbContext.DOCTOR.Find(DoctorId);
            if (doctor == null)
            {
                return Json(new { error = 1, msg = "Error! do not find doctor !" });
            }

            doctor.DELETEFLAG = true;

            _dbContext.DOCTOR.AddOrUpdate(doctor);
            _dbContext.SaveChanges();

            return Json(new { error = 0, msg = "ok" });
        }

        [HttpPost]
        public JsonResult UpdateDoctor(DoctorViewModel model)
        {

            model.DOCTORNAME = model.DOCTORNAME != null ? model.DOCTORNAME.Trim() : model.DOCTORNAME;
            model.DOCTORADDRESS = model.DOCTORADDRESS != null ? model.DOCTORADDRESS.Trim() : model.DOCTORADDRESS;

            var formatString = @"^[\p{L}\p{N}\s]*$"; //  \|!#$%&/()=?»«@£§€{}.-;'<>_,


            if (String.IsNullOrEmpty(model.DOCTORNAME))
            {

                return Json(new { error = 1, msg = "Doctor name is not null !" });
            }

            Match strname = Regex.Match(model.DOCTORNAME, formatString, RegexOptions.IgnoreCase);
            if (!strname.Success)
            {
                return Json(new { error = 1, msg = $"Doctor name does not contain any special characters !" });
            }

            if (String.IsNullOrEmpty(model.DOCTORGENDER) && (model.DOCTORGENDER != "Male" || model.DOCTORGENDER != "Female"))
            {

                return Json(new { error = 1, msg = "Gender not match!" });
            }

            DateTime dateofbirth;
            if (!DateTime.TryParse(model.DOCTORDATEOFBIRTH, out dateofbirth))
            {
                return Json(new { error = 1, msg = "error date of birth !" });
            }

            if (dateofbirth > DateTime.Now)
            {
                return Json(new { error = 1, msg = "Date of birth smaller than current date !" });
            }

            if (!String.IsNullOrEmpty(model.DOCTORMOBILENO))

            {
                string patternMobile = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";
                Match m = Regex.Match(model.DOCTORMOBILENO, patternMobile, RegexOptions.IgnoreCase);

                if (!m.Success) //mobile
                {
                    return Json(new { error = 1, msg = $"Mobile No error !" });
                }
            }
            else
            {
                return Json(new { error = 1, msg = "Mobile No is not null !" });

            }
            if (String.IsNullOrEmpty(model.DOCTORADDRESS))
            {

                return Json(new { error = 1, msg = "Address is not null !" });
            }

            var mapper = MapperServices.InitializeAutomapper();
            DOCTORMODEL Doctors = mapper.Map<DoctorViewModel, DOCTORMODEL>(model);

            if (Doctors.WORKINGSTARTDATE >= Doctors.WORKINGENDDATE)
            {
                return Json(new { error = 1, msg = "Working start date smaller than Working end date !" });
            }


            var Olddoctor = _dbContext.DOCTOR.Find(Doctors.DOCTORID);
            
            if(Olddoctor == null)
            {
                return Json(new { error = 1, msg = "Can`t find doctor !"});
            }

            var department = _dbContext.DEPARTMENT.Where(d => d.DELETEFLAG == false && d.DEPARTMENTID == Doctors.DEPARTMENTID).FirstOrDefault();
            if (department == null) { return Json(new { error = 1, msg = "Error ! Can`t find Department !" }); }

           // Doctors.DEPARTMENT = department;

            var date = DateTime.Now;
            Doctors.CREATEBY = Olddoctor.CREATEBY;
            Doctors.CREATEDATE = Olddoctor.CREATEDATE;
            Doctors.UPDATEBY = "vũ";
            Doctors.UPDATEDATE = date;
            try
            {
                _dbContext.DEPARTMENT.AddOrUpdate(department);
                _dbContext.DOCTOR.AddOrUpdate(Doctors);
                _dbContext.SaveChanges();

            }catch(Exception ex)
            {
                return Json(new { error = 1, msg = "Can`t update doctor !" });
            }
            


            return Json(new { error = 0, msg = "" });
        }

        [HttpPost]
        //load doctor data for edit
        public JsonResult LoadDoctor(int DoctorId)
        {
            if (DoctorId == 0)
            {
                return Json(new { error = 1, msg = "Error! do not delete doctor !" });
            }
            var doctor = _dbContext.DOCTOR.Find(DoctorId);
            if (doctor == null)
            {
                return Json(new { error = 1, msg = "Error! do not find doctor!" });
            }

            var mapper = MapperServices.InitializeAutomapper();
            DoctorEditModel dt = mapper.Map<DOCTORMODEL,DoctorEditModel >(doctor);

            return Json(new { error = 0, msg = "ok" , doctor = dt });
        }



    }
}