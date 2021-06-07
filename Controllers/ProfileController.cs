using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class ProfileController : Controller
    {
        FlowEntities db = new FlowEntities();

        public ActionResult Register()
        {
            var cookie = HttpContext.Request.Cookies.Get("username");
            if (cookie != null)
            {
                Session["username"] = cookie.Value;

                return RedirectToAction("Index", "Home");
            }
            Profile prof = new Profile();
            var res = (from uni in db.Universities select uni.Name).ToList();
            prof.Universities = res;
            return View(prof);
        }
        public ActionResult Login()
        {
            var cookie = HttpContext.Request.Cookies.Get("username");
            if (cookie != null)
            {
                Session["username"] = cookie.Value;

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Profile pro)
        {
            try
            {


                Profile profile = new Profile();
                profile.username = pro.username;
                profile.Email = pro.Email;
                profile.Password = pro.Password;
                profile.major = pro.major;
                profile.university_ID = null;

                db.Profiles.Add(profile);
                db.SaveChanges();
                Session["username"] = profile.username.ToString();
                Session["Email"] = profile.Email.ToString();
                if (pro.remember == true)
                {
                    var cookie = new HttpCookie("username", pro.username);
                    cookie.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Response.Cookies.Add(cookie);
                }
            }
            catch (Exception e)
            {

                ViewBag.DataExists = true;
                return RedirectToAction("Register", "Profile");
            }



            return RedirectToAction("Index", "Home");
        }

     

        [HttpPost]
        public ActionResult LoginSubmit(Profile user)
        {
            var  profile =db.Profiles.Where(x => x.Email == user.Email && x.Password == user.Password);
            if (profile.Count()>0)
            {
                var profileuser = profile.FirstOrDefault();
                if (user.remember == true)
                {
                   
                    var cookie = new HttpCookie("username", profileuser.username);
                    cookie.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Response.Cookies.Add(cookie);

                }
                if (profileuser != null)
                {
                    Session["username"] = profileuser.username.ToString();
                    Session["Email"] = profileuser.Email.ToString();
                    Console.WriteLine(Session["username"]);
                    return RedirectToAction("Index", "Home");

                }
            }



            return RedirectToAction("Login", "Profile");

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            if (Request.Cookies["username"] != null)
            {
                var cookie = new HttpCookie("username");
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(cookie);

            }

            return RedirectToAction("Login", "Profile");
        }
    }
}