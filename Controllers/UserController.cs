using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class UserController : Controller
    {
        private StackDBContext db = new StackDBContext();
       
        // GET: User
        public ActionResult Login()
        {
            
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}