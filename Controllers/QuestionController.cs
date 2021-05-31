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

        public ActionResult questionDetails(int id)
        {    
            var question = db.Questions.Single(x => x.question_ID == id);
            var answers =db.Answers.Where(x => x.question_ID == id).ToList();
            var questionComments = db.Comments.Where(x => x.question_ID == id).ToList();
            QuestionTagView questionTagView = new QuestionTagView();
            questionTagView.question_ID = question.question_ID;
            questionTagView.title = question.title;
            questionTagView.body = question.body;
            questionTagView.imagePath = question.imagePath;
            questionTagView.date = question.date;
            var name = db.Profiles.Single(x => x.creator_ID == question.creator_ID);
            questionTagView.creator.username = name.username;
            questionTagView.answers = answers;
            questionTagView.questionComments = questionComments;
            var res = (from tagId in db.tag_question.Where(x => x.question_ID == question.question_ID) join tag in db.Tags on tagId.tag_ID equals tag.tag_ID select tag.tagText).ToList();
            questionTagView.tag = res;
            foreach (var answer in answers)
            {
                List<Comment> comments = (from cmmnt in db.Comments.Where(x => x.answer_ID == answer.answer_ID) select cmmnt).ToList();
                questionTagView.answersComments.Add(comments);
            }
            List<QuestionTagView> result = new List<QuestionTagView>();
            result.Add(questionTagView);
            return View(result);
        }
    }
}