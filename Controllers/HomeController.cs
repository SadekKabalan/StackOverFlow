using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class HomeController : Controller
    {
        private StackDBContext db = new StackDBContext();

        public ActionResult Index()
        {   
            return View();
        }

        public ActionResult Questions()
        {
            var questions = from e in db.Questions
                            select e;
            return View();
        }

        public ActionResult Journey()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }




    }
}