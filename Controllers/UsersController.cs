using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult searchUser(string searchUser,int nbPages=1)
        {
            string searchUsers = "";
            if (Session["searchedUsers"] == null && searchUser.Length > 0)
                Session["searchedUsers"] = searchUser;

            if (Session["searchedUsers"] != null && searchUser != null)
                Session["searchedUsers"] = searchUser;
            if (Session["searchedUsers"] != null)
            {
                searchUsers = Session["searchedUsers"].ToString();
            }
            FlowEntities db = new FlowEntities();
            var allProfs = from prof in db.Profiles where prof.username.Contains(searchUsers) select prof;
            List<users> result = new List<users>();
            foreach (var pro in allProfs)
            {
                users userModel = new users();
                userModel.creator_ID = pro.creator_ID;
                userModel.username = pro.username;
                userModel.nbAnswers = (db.Answers.Where(x => x.creator_ID == pro.creator_ID)).Count();
                userModel.nbQuestions = (db.Questions.Where(x => x.creator_ID == pro.creator_ID)).Count();
                userModel.nbPages = (int)Math.Ceiling((double)allProfs.Count() / 24);
                userModel.pageIndex = nbPages;
                result.Add(userModel);
            }
            List<users> resultList = result.OrderByDescending(x => x.nbQuestions).Skip((nbPages - 1) * 24).Take(24).ToList();
            return View(resultList);

        }

        public ActionResult userDetails(int nbPages, string name)
        {
            FlowEntities db = new FlowEntities();
            var prof = db.Profiles.Single(x => x.username == name);
            users model = new users();
            model.creator_ID = prof.creator_ID;
            model.username = prof.username;
            model.nbAnswers= (db.Answers.Where(x => x.creator_ID == prof.creator_ID)).Count();
            var res = db.Questions.Where(x => x.creator_ID == prof.creator_ID).ToList();
            model.nbQuestions = res.Count();
            model.questions = res.OrderByDescending(x => x.date).Take(4).ToList();
            return View(model);
        }
    }
}