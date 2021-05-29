using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        FlowEntities db = new FlowEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Submit(QuestionTagView model)
        {
            var res = (from tagText in db.Tags select tagText.tagText).ToList();
            Question question = new Question();
            
            question.title = model.title;
            question.body = model.body;
            question.imagePath = model.imagePath;
            question.date = DateTime.Now.ToString();
            question.creator_ID = 1;

            db.Questions.Add(question);
            db.SaveChanges();
            foreach (var tagtext in model.tag)
            {
                var tags = tagtext.Split(' ');
                foreach (var oneTag in tags)
                {
                    tag_question tag_Question = new tag_question();
                    if (res.Contains(oneTag.ToLower()))
                    {
                        var id = db.Tags.Single(x => x.tagText == oneTag.ToLower());
                        tag_Question.tag_ID = id.tag_ID;
                    }
                    else
                    {
                        if (oneTag.ToLower().Length > 0)
                        {
                            Tag tag = new Tag();

                            tag.tagText = oneTag.ToLower();
                            db.Tags.Add(tag);
                            db.SaveChanges();
                            tag_Question.tag_ID = tag.tag_ID;
                        }
                    }
                    tag_Question.question_ID = question.question_ID;
                    db.tag_question.Add(tag_Question);
                    db.SaveChanges();


                }
            }
       
            return RedirectToAction("Questions","Home");
        }
    }
}