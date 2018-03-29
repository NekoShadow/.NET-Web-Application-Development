using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{    
    public class UserController : Controller
    {
        private EFDbContext db = new EFDbContext();
        // GET: User       
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (EFDbContext db = new EFDbContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                Session["CurrentUser"] = user;
                return RedirectToAction("Login","User");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (EFDbContext db = new EFDbContext())
            {
                var usr = db.Users.SingleOrDefault(u => u.Name == user.Name && u.Password == user.Password);
                if (usr != null)
                {
                    Session["CurrentUser"] = user;
                    TempData["UserID"]=user.UserID;
                    
                    return RedirectToAction("List","Product", new { userName = user.Name });
                }
                else
                {
                    ModelState.AddModelError("", "用户名或密码错误。");
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LogOff(string returnUrl)
        {
            Session["CurrentUser"] = null;
            return RedirectToAction("Login","User");
        }

        public ActionResult ViewMyself(string UserName)
        {
            if (Session["CurrentUser"]!=null)
            {
                var users = from m in db.Users
                            select m;
                if (!String.IsNullOrEmpty(UserName))
                {
                    users = users.Where(s => s.Name.Contains(UserName));
                }
                return View(users);
            }
            else return RedirectToAction("Login");
        }

        public ActionResult help()
        {
            return View();
        }

    }
}