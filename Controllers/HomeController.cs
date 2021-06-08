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
        public ActionResult Index()
        {
            var cookie = HttpContext.Request.Cookies.Get("username");
            if (cookie != null)
            {
                Session["username"] = cookie.Value;

            }
            FlowEntities db = new FlowEntities();
            var journies = from journey in db.Journeys select journey;
            var journeyList = journies.OrderByDescending(x => x.date).Take(3);
            List<journies> result = new List<journies>();
            foreach (var journ in journeyList)
            {
                journies jour = new journies();
                jour.journey_ID = journ.journey_ID;
                jour.title = journ.title;
                jour.body = journ.body;
                jour.date = journ.date;

                var name = db.Profiles.Single(x => x.creator_ID == journ.creator_ID);
                jour.username = name.username;
                jour.creator_ID = name.creator_ID;

                result.Add(jour);
            }
            return View(result);
        }

        public ActionResult Questions(int nbPages = 1)
        {
            FlowEntities db = new FlowEntities();
            var questions = from question in db.Questions select question;
            var resNames = (from uni in db.Universities select uni.Name).ToList();
            var questionList = questions.OrderByDescending(x => x.date ).Skip((nbPages - 1) * 6).Take(6);
            List<QuestionTagView> result=new List<QuestionTagView>();
            foreach(var quest in questionList)
            {
                QuestionTagView tagView = new QuestionTagView();
                tagView.question_ID = quest.question_ID;
                tagView.title = quest.title;
                tagView.body = quest.body;
                tagView.date = quest.date;
                var res= (from tagId in db.tag_question.Where(x => x.question_ID == quest.question_ID) join tag in db.Tags on tagId.tag_ID equals tag.tag_ID select tag.tagText ).ToList();
                tagView.tag= res;
                List<int> nbAnswers =(from answer in db.Answers.Where(x => x.question_ID == quest.question_ID) select answer.answer_ID).ToList();
                tagView.nbAnswers = nbAnswers.Count();
                var correct = db.Answers.Where(x => x.question_ID == quest.question_ID).Where(x => x.isApproved == true);
                if (correct.Count() != 0)
                    tagView.isCorrect = true;
                else
                    tagView.isCorrect = false;
                var name = db.Profiles.Single(x => x.creator_ID == quest.creator_ID);
                tagView.universities = resNames;
                tagView.creator.username = name.username;
                tagView.nbPages = (int) Math.Ceiling((double) questions.Count() / 6 );
                tagView.pageIndxer = nbPages;
                result.Add(tagView);
            }
          
            
            return View(result);
        }
        public ActionResult MyQuestions(int nbPages =1)
        {
            FlowEntities db = new FlowEntities();
            if (Session["username"] != null) {
                string user = Session["username"].ToString();
                int cretID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                var questions = db.Questions.Where(x => x.creator_ID == cretID);
                var questionList = questions.OrderByDescending(x => x.date).Skip((nbPages - 1) * 6).Take(6);
                List<QuestionTagView> result = new List<QuestionTagView>();
                foreach (var quest in questionList)
                {
                    QuestionTagView tagView = new QuestionTagView();
                    tagView.question_ID = quest.question_ID;
                    tagView.title = quest.title;
                    tagView.body = quest.body;
                    tagView.date = quest.date;
                    var res = (from tagId in db.tag_question.Where(x => x.question_ID == quest.question_ID) join tag in db.Tags on tagId.tag_ID equals tag.tag_ID select tag.tagText).ToList();
                    tagView.tag = res;
                    List<int> nbAnswers = (from answer in db.Answers.Where(x => x.question_ID == quest.question_ID) select answer.answer_ID).ToList();
                    tagView.nbAnswers = nbAnswers.Count();
                    var correct = db.Answers.Where(x => x.question_ID == quest.question_ID).Where(x => x.isApproved == true);
                    if (correct.Count() != 0)
                        tagView.isCorrect = true;
                    else
                        tagView.isCorrect = false;
                    var name = db.Profiles.Single(x => x.creator_ID == quest.creator_ID);
                    tagView.creator.username = name.username;
                    tagView.nbPages = (int)Math.Ceiling((double)questions.Count() / 6);
                    tagView.pageIndxer = nbPages;
                    result.Add(tagView);
                }
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Profile");
            }

            

        }

        public ActionResult SavedQustions(int nbPages=1)
        {
            FlowEntities db = new FlowEntities();
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                int cretID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                var questions = (from questionID in db.savedQuestions.Where(x => x.creator_ID == cretID) join question in db.Questions on questionID.question_ID equals question.question_ID select question);
                var questionList = questions.OrderByDescending(x => x.date).Skip((nbPages - 1) * 6).Take(6);
                List<QuestionTagView> result = new List<QuestionTagView>();
                foreach (var quest in questionList)
                {
                    QuestionTagView tagView = new QuestionTagView();
                    tagView.question_ID = quest.question_ID;
                    tagView.title = quest.title;
                    tagView.body = quest.body;
                    tagView.date = quest.date;
                    var res = (from tagId in db.tag_question.Where(x => x.question_ID == quest.question_ID) join tag in db.Tags on tagId.tag_ID equals tag.tag_ID select tag.tagText).ToList();
                    tagView.tag = res;
                    List<int> nbAnswers = (from answer in db.Answers.Where(x => x.question_ID == quest.question_ID) select answer.answer_ID).ToList();
                    tagView.nbAnswers = nbAnswers.Count();
                    var correct = db.Answers.Where(x => x.question_ID == quest.question_ID).Where(x => x.isApproved == true);
                    if (correct.Count() != 0)
                        tagView.isCorrect = true;
                    else
                        tagView.isCorrect = false;
                    var name = db.Profiles.Single(x => x.creator_ID == quest.creator_ID);
                    tagView.creator.username = name.username;
                    tagView.nbPages = (int)Math.Ceiling((double)questions.Count() / 6);
                    tagView.pageIndxer = nbPages;
                    result.Add(tagView);
                }
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Profile");
            }

        }

        public ActionResult Journey(int nbPages=1)
        {
            FlowEntities db = new FlowEntities();
            var journies = from journey in db.Journeys select journey;
            var journeyList = journies.OrderByDescending(x => x.date).Skip((nbPages - 1) * 6).Take(6);
            List<journies> result = new List<journies>();
            foreach (var journ in journeyList)
            {
                journies jour = new journies();
                jour.journey_ID = journ.journey_ID;
                jour.title = journ.title;
                jour.body = journ.body;
                jour.date = journ.date;

                var name = db.Profiles.Single(x => x.creator_ID == journ.creator_ID);
                jour.username= name.username;
                jour.creator_ID = name.creator_ID;
                jour.nbPages = (int)Math.Ceiling((double)journies.Count() / 6);
                jour.pageIndxer = nbPages;
                result.Add(jour);
            }
                return View(result);
            
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Tag(int nbPages =1)
        {
            FlowEntities db = new FlowEntities();
            var tags = (from tag in db.Tags select tag).ToList();
            List<tagModel> result = new List<tagModel>();
            foreach(var tag in tags)
            {
                tagModel tagModel = new tagModel();
                tagModel.tag_ID = tag.tag_ID;
                tagModel.text = tag.tagText;
                tagModel.nbUsedInQuest = (db.tag_question.Where(x => x.tag_ID == tag.tag_ID)).Count();
                tagModel.nbPages = (int)Math.Ceiling((double)tags.Count() / 24);
                tagModel.pageIndex =nbPages;
                result.Add(tagModel);
            }
            List<tagModel> resultList = result.OrderByDescending(x => x.nbUsedInQuest).Skip((nbPages - 1) * 24).Take(24).ToList();
            return View(resultList);
        }
        public ActionResult questionForm()
        {
            return View();
        }

        public ActionResult Users(int nbPages = 1)
        {
            FlowEntities db = new FlowEntities();
            var allProfs = from prof in db.Profiles select prof;
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



    }
}