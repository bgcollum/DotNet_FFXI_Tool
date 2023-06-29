using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCPresentation.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MVCPresentation.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        // private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;

        // GET: Admin
        public ActionResult Index()
        {
            //return View(db.ApplicationUsers.ToList());
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.DisplayName).ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            // Get a list of roles the user has and put them into a viewbag as roles
            // along with a list of roles that the user doesn't have as noRoles
            var usrMgr = new LogicLayer.UserManager();
            var allRoles = usrMgr.RetrieveUserRoles();

            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }
        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var applicationUser = userManager.Users.First(u => u.Id == id);    // Lambda to select first user that matches ID

            // Code to prevent removing the last Administrator
            if(role == "Administrator")
            {
                var adminUsers = userManager.Users.ToList()
                    .Where(u => userManager.IsInRole(u.Id, "Administrator"))
                    .ToList().Count();
                if(adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove the last Administrator.";
                    return RedirectToAction("Details", "Admin", new { id = applicationUser.Id });
                }
            }
            userManager.RemoveFromRole(id, role);

            if(applicationUser.UserID != null)
            {
                try
                {
                    var usrMgr = new LogicLayer.UserManager();
                    // Code here is different since RemoveRoleFromUser takes a User object
                    DataObjects.User dataObjectsUser = usrMgr.RetrieveUserFromEmail(applicationUser.Email);
                    usrMgr.RemoveRoleFromUser(dataObjectsUser, role);
                }
                catch (Exception)
                {
                    // Nothing to do
                }
            }
            return RedirectToAction("Details", "Admin", new { id = applicationUser.Id });
        }
        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var applicationUser = userManager.Users.First(u => u.Id == id);    // Lambda to select first user that matches ID

            userManager.AddToRole(id, role);

            if(applicationUser.UserID != null)
            {
                try
                {
                    var usrMgr = new LogicLayer.UserManager();
                    DataObjects.User dataObjectsUser = usrMgr.RetrieveUserFromEmail(applicationUser.Email);
                    usrMgr.AddRoleToUser(dataObjectsUser, role);
                }
                catch (Exception)
                {
                    // Nothing to do
                }
            }
            return RedirectToAction("Details", "Admin", new { id = applicationUser.Id });
        }
    }
}
