﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SouthFork.DAL;
using SouthFork.Models;

namespace SouthFork.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private SouthForkDBContext db = new SouthForkDBContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        //search function
        public ActionResult Search(string searching)
        {
            return View("Index", db.Employees.Where(x => x.FirstName.Contains(searching) || searching == null).ToList());
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Positions = db.Positions.ToList();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FirstName,LastName,Email,Phone,Wage,Password,PositionID")] Employee employee)
        {
            ViewBag.Positions = db.Positions.ToList();

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();

                ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Employee added to list.</div>";
                return View("Index", db.Employees.ToList());
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Positions = db.Positions.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FirstName,LastName,Email,Phone,Wage,Password,PositionID")] Employee employee)
        {
            ViewBag.Positions = db.Positions.ToList();

            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Employee edited.</div>";
                return View("Index", db.Employees.ToList());
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Employee deleted.</div>";
            return View("Index", db.Employees.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
