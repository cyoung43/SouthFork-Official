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
    public class ProjectsController : Controller
    {
        private SouthForkDBContext db = new SouthForkDBContext();

        // GET: Projects
        public ActionResult Index()
        {
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Builders = db.Builders.ToList();
            ViewBag.Employees = db.Employees.ToList();
            //ViewBag.Results = results;

            return View(db.Projects.ToList());
        }

        //search function
        public ActionResult Search(string searching)
        {
            return View("Index", db.Projects.Where(x => x.ProjectName.Contains(searching) || searching == null).ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Builders = db.Builders.ToList();
            ViewBag.Employees = db.Employees.ToList();

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,BudgetedHours,ActualHours,BudgetedDays,ActualDays,DepositDate,BeginDate,CompleteDate,PayDate,DeliverDate,ProjectPrice,BidPrice,ClientPaid,SquareFootage,ClientID,BuilderID,EmployeeID")] Project project)
        {
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Builders = db.Builders.ToList();
            ViewBag.Employees = db.Employees.ToList();

            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();

                ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Project added to list.</div>";
                return View("Index", db.Projects.ToList());
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Builders = db.Builders.ToList();
            ViewBag.Employees = db.Employees.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,BudgetedHours,ActualHours,BudgetedDays,ActualDays,DepositDate,BeginDate,CompleteDate,PayDate,DeliverDate,ProjectPrice,BidPrice,ClientPaid,SquareFootage,ClientID,BuilderID,EmployeeID")] Project project)
        {
            ViewBag.Clients = db.Clients.ToList();
            ViewBag.Builders = db.Builders.ToList();
            ViewBag.Employees = db.Employees.ToList();

            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Project edited.</div>";
                return View("Index", db.Projects.ToList());
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();

            ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Project deleted.</div>";
            return View("Index", db.Projects.ToList());
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
