using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorManage.Models.Database;
using Microsoft.Ajax.Utilities;

namespace DoctorManage.Controllers
{
    public class DEPARTMENTController : Controller
    {
        private readonly DoctorDbContainer _dbContext;

        public DEPARTMENTController(DoctorDbContainer dbContext)
        {
            _dbContext = dbContext;
            
        }

        // GET: DEPARTMENT
        public ActionResult Index()
        {
            return View(_dbContext.DEPARTMENT.Where(d=>d.DELETEFLAG == false).ToList());
        }

        // GET: DEPARTMENT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTMENT dEPARTMENT = _dbContext.DEPARTMENT.Find(id);
            if (dEPARTMENT == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTMENT);
        }

        // GET: DEPARTMENT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DEPARTMENT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DEPARTMENTID,DEPARTMENTNAME")] DEPARTMENT dEPARTMENT)
        {
            if (ModelState.IsValid)
            {
                dEPARTMENT.CREATEBY = "vũ";
                dEPARTMENT.CREATEDATE = DateTime.Now;
                dEPARTMENT.UPDATEBY = "vũ";
                dEPARTMENT.UPDATEDATE = DateTime.Now;
                dEPARTMENT.DELETEFLAG = false;

                _dbContext.DEPARTMENT.Add(dEPARTMENT);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dEPARTMENT);
        }

        // GET: DEPARTMENT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTMENT dEPARTMENT = _dbContext.DEPARTMENT.Find(id);
            if (dEPARTMENT == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTMENT);
        }

        // POST: DEPARTMENT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DEPARTMENTID,DEPARTMENTNAME,CREATEBY,CREATEDATE,UPDATEBY,UPDATEDATE,DELETEFLAG")] DEPARTMENT dEPARTMENT)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(dEPARTMENT).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dEPARTMENT);
        }

        // GET: DEPARTMENT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTMENT dEPARTMENT = _dbContext.DEPARTMENT.Find(id);
            if (dEPARTMENT == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTMENT);
        }

        // POST: DEPARTMENT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DEPARTMENT dEPARTMENT = _dbContext.DEPARTMENT.Include("DOCTORMODEL").Where(d=>d.DEPARTMENTID == id).FirstOrDefault();
            if (dEPARTMENT != null)
            {
                if (!(dEPARTMENT.DOCTORMODEL.Count > 0))
                {
                    dEPARTMENT.DELETEFLAG = true;
                    _dbContext.DEPARTMENT.AddOrUpdate(dEPARTMENT);
                    _dbContext.SaveChanges();
                }

            }
           
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
