using System;
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
    public class ClientsController : Controller
    {
        private SouthForkDBContext db = new SouthForkDBContext();

        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        [ActionName("ClientHome")]
        public ActionResult ClientHome(Client user)
        {
            ViewBag.User = user;
            return View(user);
        }

        [ActionName("ClientHome2")]
        public ActionResult ClientHome(int? id)
        {
            Client user = db.Clients.Find(id);
            return View("ClientHome", user);
        }

        public ActionResult SeeProjects(int? id)
        {
            Client client = db.Clients.Find(id);

            IEnumerable<Project> projects =
                db.Database.SqlQuery<Project>(
                    "SELECT * " +
                    "FROM Project " +
                    "WHERE ClientID = '" + client.ClientID + "';"
                    );

            if (projects.Count() == 0)
            {
                ViewBag.User = client;
                ViewBag.Count = "You have no current projects. <br/>Please contact us to create a new project.";
                return View("ClientHome", client);
            }

            else
            {
                return View(projects);
            }
            
        }

        public ActionResult Search(string searching)
        {
            return View("Index", db.Clients.Where(x => x.FirstName.Contains(searching) || searching == null).ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,FirstName,LastName,Email,Phone,Address,City,State,Zip,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();

                ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Client added to list.</div>";
                return View("Index", db.Clients.ToList());
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,FirstName,LastName,Email,Phone,Address,City,State,Zip,Notes")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Client profile edited.</div>";
                return View("Index", db.Clients.ToList());
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            ViewBag.Results = "<div class='alert alert-success alert-dismissible'>" +
                                    "<button type = 'button' class='close' data-dismiss='alert'>&times;</button>" +
                                    "<strong>Success!</strong> Client deleted.</div>";
            return View("Index", db.Clients.ToList());
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
