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
            return View();
        }

        public ActionResult Questions(int nbPages = 1)
        {
            FlowEntities db = new FlowEntities();
            var questions = from question in db.Questions select question;
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
                var name = db.Profiles.Single(x => x.creator_ID == quest.creator_ID);
                tagView.creator.username = name.username;
                tagView.nbPages = (int) Math.Ceiling((double) questions.Count() / 6 );
                tagView.pageIndxer = nbPages;
                result.Add(tagView);
            }
          
            
            return View(result);
        }

        public ActionResult Journey()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult questionForm()
        {
            return View();
        }



    }
}