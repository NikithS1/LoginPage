using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserSignupLogin.Models;

namespace UserSignupLogin.Controllers
{
    public class HomeController : Controller
    {
        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBLUserInfoes.ToList());
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(TBLUserInfo tBLUserInfo)
        {
            if (db.TBLUserInfoes.Any(x => x.Username == tBLUserInfo.Username))
            {
                ViewBag.Notification = "This account has already existed";
                return View();

            }
            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();

                Session["IdSS"] = tBLUserInfo.Id.ToString();
                Session["UsernameSS"] = tBLUserInfo.Username.ToString();
                return RedirectToAction("Index", "Home");

            }
        }
         public ActionResult Logout()
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
                }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLUserInfo tBLUserInfo)
        {
            var checkLogin = db.TBLUserInfoes.Where(x=>x.Username.Equals(tBLUserInfo.Username)&& x.Password.Equals(tBLUserInfo.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdSS"] = tBLUserInfo.Id.ToString();
                Session["UsernameSS"] = tBLUserInfo.Username.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }
        }
    }

