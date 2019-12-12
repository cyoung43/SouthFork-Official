using SouthFork.DAL;
using SouthFork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SouthFork.Controllers
{
    public class HomeController : Controller
    {
        private SouthForkDBContext db = new SouthForkDBContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CentralLogin()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            string email = form["Email address"].ToString();
            string password = form["Password"].ToString();

            var currentUser = db.Database.SqlQuery<Employee>(
                "SELECT * " +
                "FROM Employee " +
                "WHERE Email = '" + email + "' AND " +
                "Password = '" + password + "'");

            //string name = db.Database.SqlQuery<Employee>(
            //    "SELECT FirstName " +
            //    "FROM Employee " +
            //    "WHERE Email '" + email + "'"
            //    );

            if (currentUser.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(email, rememberMe);

                return RedirectToAction("Directory", "Home", new { userLogin = email });

            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ClientLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientLogin(FormCollection form, bool rememberMe = false)
        {
            string email = form["Email address"].ToString();
            string password = form["Password"].ToString();

            var currentUser = db.Database.SqlQuery<Client>(
                "SELECT * " +
                "FROM Client " +
                "WHERE Email = '" + email + "' AND " +
                "Password = '" + password + "'");

            IEnumerable<Client> User =
                db.Database.SqlQuery<Client>(
            "Select * " +
            "FROM Client " +
            "WHERE Email = '" + email + "' AND " +
            "Password = '" + password + "'");

            if (currentUser.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(email, rememberMe);

                return RedirectToAction("ClientHome", "Clients", User.FirstOrDefault());

            }
            else
            {
                ViewBag.Credentials = "Invalid credentials. Please create an account.";
                return RedirectToAction("CreateProfile", "Home");
            }
        }

        
        public ActionResult Directory(string userLogin)
        {
            ViewBag.user = userLogin;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //send thank you email after inquiry
        public ActionResult SendEmail(string Name, string Email, string Notes)
        {
            Contact contact = new Contact();

            contact.Email = Email;
            contact.FullName = Name;
            contact.Notes = Notes;

            if (contact.Email == null || contact.FullName == null || contact.Email == "" || contact.FullName == "")
            {
                ViewBag.Error = "Please enter your full name and email.";
                return View("Contact", contact);
            }

            else
            {
                db.Contacts.Add(contact);
                db.SaveChanges();

                return RedirectToAction("Email", contact);
            }
            
        }

        public ActionResult Email(Contact contact)
        {
            string body = contact.FullName + ",<br/><br/>Thank you for your interest in SouthFork Design Group. " +
                "We are excited to begin this process with you. We would love to chat on the phone with you regarding your potential project. " +
                "What time might work best for you?<br/><br/> " +
                "Best,<br/><br/>Gary Anderson<br/>Chief Draftsman<br/>SouthFork Design Group";

            //set up email message
            MailMessage mail = new MailMessage();
            mail.To.Add(contact.Email);
            mail.From = new MailAddress("profgaryanderson@gmail.com");
            mail.Subject = "SouthFork Design Group";
            mail.Body = body;
            mail.IsBodyHtml = true;

            //set up smtp client
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("profgaryanderson@gmail.com", "Magsungay17");
            smtp.EnableSsl = true;
            smtp.Send(mail);


            //
            ViewBag.Thanks = "Thanks for contacting us! We will be in touch with you soon.";
            return View();
        }

        [HttpGet]
        public ActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProfile([Bind(Include = "ClientID,FirstName,LastName,Email,Password")] Client client)
        {

            if (client.Password == null || client.Password == "")
            {
                ViewBag.Password = "Please enter your password below!";
                return View(client);
            }

            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("ClientLogin", "Home");
            }

            return View(client);
        }

        //public ActionResult ClientHome()
        //{
        //    return View();
        //}
    }
}