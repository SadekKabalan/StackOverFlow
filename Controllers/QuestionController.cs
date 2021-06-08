using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;
using System.IO;
using System.Web.UI;

namespace StackOverFlow.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        FlowEntities db = new FlowEntities();
       
      
    public ActionResult Index()
        {   if(Session["username"]!=null)
            return View();
           else
            return RedirectToAction("Login", "Profile");
        }
        public ActionResult Submit(QuestionTagView model)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                var res = (from tagText in db.Tags select tagText.tagText).ToList();
                Question question = new Question();
                if(model.title!=null)
                question.title = model.title;
                else
                    return RedirectToAction("Questions", "Home");
                if(model.body!=null)
                question.body = model.body;
                else
                    return RedirectToAction("Questions", "Home");
                if (model.imageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(model.imageFile.FileName);
                    string extension = Path.GetExtension(model.imageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    model.imagePath = "~/uploads/" + filename;
                    question.imagePath = model.imagePath;
                    string path = Path.Combine(Server.MapPath("~/uploads/"), filename);
                    model.imageFile.SaveAs(path);
                }
                question.date = DateTime.Now.ToString();
                question.creator_ID =db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                db.Questions.Add(question);
                if (model.tag!=null)
                {
                    foreach (var tagtext in model.tag)
                    {
                        if (tagtext.Count() > 0)
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

                                        tag_Question.tag_ID = tag.tag_ID;
                                    }
                                }
                                tag_Question.question_ID = question.question_ID;
                                db.tag_question.Add(tag_Question);



                            }
                        }
                        else
                            return RedirectToAction("Questions", "Home");
                    }
                    db.SaveChanges();
                    return RedirectToAction("Questions", "Home");
                }
                else
                    return RedirectToAction("Questions", "Home");

               
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult questionDetails(int id,int pageIndex =1)
        {
           
            var question = db.Questions.Single(x => x.question_ID == id);
            
            var answers =db.Answers.Where(x => x.question_ID == id).OrderByDescending(x => x.isApproved).ThenByDescending(x => x.date).ToList();
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
            questionTagView.nbAnswers = answers.Count();
            questionTagView.pageIndxer = pageIndex;
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

        public ActionResult AddCommentQuestion(int id,string cmnt)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                Comment comment = new Comment();
                comment.creator_ID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                if (cmnt.Equals("") && cmnt != null)
                    return RedirectToAction("questionDetails", "Question", new { id = id });
                comment.text = cmnt;
                comment.question_ID = id;
                comment.date = DateTime.Now.ToString();
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("questionDetails", "Question", new { id = id });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult AddCommentAnswer(int id, string cmnt)
        {
            int? reid = db.Answers.Single(x => x.answer_ID == id).question_ID;
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                Comment comment = new Comment();
                comment.creator_ID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                if(cmnt.Equals("") && cmnt!=null)
                    return RedirectToAction("questionDetails", "Question", new { id = reid });
                comment.text = cmnt;
                comment.answer_ID = id;
                comment.date = DateTime.Now.ToString();
                db.Comments.Add(comment);
                db.SaveChanges();
                
                return RedirectToAction("questionDetails", "Question", new { id = reid });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult AddAnswer(int id, HttpPostedFileBase image ,string answer)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                Answer ans = new Answer();
                ans.question_ID = id;
                ans.date = DateTime.Now.ToString();
                ans.creator_ID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                ans.text = answer;
                string filename = "";
                if (image != null)
                {
                    filename = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    ans.imagePath = "~/uploads/" + filename;
                    string path = Path.Combine(Server.MapPath("~/uploads/"), filename);
                    image.SaveAs(path);
                }
                ans.isApproved = false;
                ans.votes = 0;
                db.Answers.Add(ans);
                db.SaveChanges();
                return RedirectToAction("questionDetails", "Question", new { id = id });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult voteUp(int id)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                int cretID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                var answer = db.Answers.Single(x => x.answer_ID == id);
                var creatorQuestionId = db.Questions.Single(x => x.question_ID == answer.question_ID).creator_ID;
                var ifVoted = db.isVoteds.Where(x => x.answer_ID == answer.answer_ID && x.creator_ID == cretID);
                if (ifVoted.Count() == 0)
                {
                    isVoted voted = new isVoted();
                    voted.creator_ID = cretID;
                    voted.answer_ID = answer.answer_ID;
                    db.isVoteds.Add(voted);
                    if (creatorQuestionId == cretID)
                        answer.isApproved = true;
                    answer.votes++;

                    db.SaveChanges();
                }

                int? reid = answer.question_ID;
                return RedirectToAction("questionDetails", "Question", new { id = reid });
            }
            else
                return RedirectToAction("Login", "Profile");
        }
        public ActionResult voteDown(int id)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                int cretID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                var answer = db.Answers.Single(x => x.answer_ID == id);
                var creatorQuestionId = db.Questions.Single(x => x.question_ID == answer.question_ID).creator_ID;
                var ifVoted = db.isVotedDowns.Where(x => x.answer_ID == answer.answer_ID && x.creator_ID == cretID);
                if (ifVoted.Count() == 0)
                {
                    isVotedDown voted = new isVotedDown();
                    voted.creator_ID = cretID;
                    voted.answer_ID = answer.answer_ID;
                    db.isVotedDowns.Add(voted);

                    if (creatorQuestionId == cretID)
                        answer.isApproved = false;
                    answer.votes--;
                    db.SaveChanges();
                }
                int? reid = answer.question_ID;
                return RedirectToAction("questionDetails", "Question", new { id = reid });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult save(int id)
        {
            if (Session["username"] != null)
            { string user = Session["username"].ToString();
                savedQuestion saved = new savedQuestion();
                saved.question_ID = id;
                saved.creator_ID = db.Profiles.Single(x => x.username == user).creator_ID;
                var res = db.savedQuestions.Where(x => x.creator_ID == saved.creator_ID && x.question_ID == id);
                if (res.Count() == 0)
                {

                    db.savedQuestions.Add(saved);
                    db.SaveChanges();
                }
                return RedirectToAction("questionDetails", "Question", new { id = id });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult removeSave(int id, int pageIndex = 1)
        {
            if (Session["username"] != null)
            {
                string user= Session["username"].ToString();
                int crtId=  db.Profiles.Single(x => x.username == user).creator_ID;
                savedQuestion saved = db.savedQuestions.Single(x => x.question_ID == id && x.creator_ID == crtId);
                db.savedQuestions.Remove(saved);
                db.SaveChanges();
                return RedirectToAction("SavedQustions", "Home",new { pageIndex=pageIndex});
            }
            return null;
        }
        public ActionResult deleteQuestion(int id,int pageIndex)
        {
            if (Session["username"] != null)
            {
                Question quest = db.Questions.Single(x => x.question_ID == id);
                db.Questions.Remove(quest);
                var tags = db.tag_question.Where(x => x.question_ID == id).ToList();
                foreach (var tag in tags)
                    db.tag_question.Remove(tag);
                var answers = db.Answers.Where(x => x.question_ID == id).ToList();
                foreach (var answer in answers)
                    db.Answers.Remove(answer);
                var cmnts = db.Comments.Where(x => x.question_ID == id).ToList();
                foreach (var cmnt in cmnts)
                {
                    db.Comments.Remove(cmnt);
                }
                var savedQuestions = db.savedQuestions.Where(x => x.question_ID == id).ToList();
                foreach (var question in savedQuestions)
                    db.savedQuestions.Remove(question);
                db.SaveChanges();
                return RedirectToAction("Questions", "Home", new { nbPages = pageIndex });
            }
            else
                return RedirectToAction("Login", "Profile");

        }
        public ActionResult edit(int id)
        {
            if (Session["username"] != null)
            {
                QuestionTagView questionTagView = new QuestionTagView();
                var res = db.Questions.Single(x => x.question_ID == id);
                questionTagView.question_ID = id;
                questionTagView.title = res.title;
                questionTagView.imagePath = res.imagePath;
                questionTagView.body = res.body;
                var tags = (from tagId in db.tag_question.Where(x => x.question_ID == res.question_ID) join tag in db.Tags on tagId.tag_ID equals tag.tag_ID select tag.tagText).ToList();
                questionTagView.tag = tags;
                return View(questionTagView);
            }
            else
                return RedirectToAction("Login", "Profile");
        }
        public ActionResult SubmitEdit(int id, QuestionTagView model, string tag)
        {
            if (Session["username"] != null)
            {
                var res = (from tagText in db.Tags select tagText.tagText).ToList();

                var question = db.Questions.Single(x => x.question_ID == id);
                if (model.title != null)
                    question.title = model.title;
                else
                    return RedirectToAction("Questions", "Home");
                if (model.body != null)
                    question.body = model.body;
                else
                    return RedirectToAction("Questions", "Home");
                if (model.imageFile != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(model.imageFile.FileName);
                    string extension = Path.GetExtension(model.imageFile.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    model.imagePath = "~/uploads/" + filename;
                    question.imagePath = model.imagePath;
                    string path = Path.Combine(Server.MapPath("~/uploads/"), filename);
                    model.imageFile.SaveAs(path);
                }
                db.SaveChanges();
                if (tag.Length > 0)
                {
                    var tags = tag.Split(' ');
                    foreach (var oneTag in tags)
                    {
                        tag_question tag_Question = new tag_question();
                        if (res.Contains(oneTag.ToLower()))
                        {
                            var tagid = db.Tags.Single(x => x.tagText == oneTag.ToLower());

                            tag_Question.tag_ID = tagid.tag_ID;
                        }
                        else
                        {
                            if (oneTag.ToLower().Length > 0)
                            {
                                Tag tagr = new Tag();

                                tagr.tagText = oneTag.ToLower();
                                db.Tags.Add(tagr);
                                db.SaveChanges();
                                tag_Question.tag_ID = tagr.tag_ID;
                            }
                        }
                        tag_Question.question_ID = question.question_ID;
                        var check = db.tag_question.Where(x => x.tag_ID == tag_Question.tag_ID && x.question_ID == id);
                        if (check.Count() == 0)
                        {
                            db.tag_question.Add(tag_Question);
                            db.SaveChanges();
                        }

                        

                    }
                    return RedirectToAction("questionDetails", "Question", new { id = id });
                }
                else
                    return RedirectToAction("questionDetails", "Question", new { id = id });

                
            }
            else
                return RedirectToAction("Login", "Profile");

        }
        
        public ActionResult deleteAnswer(int id , int ansID)
        {
            if (Session["username"] != null)
            {
                Answer ans = db.Answers.Single(x => x.answer_ID == ansID && x.question_ID == id);
                db.Answers.Remove(ans);
                var cmnts = db.Comments.Where(x => x.answer_ID == ansID).ToList();
                foreach (var cmnt in cmnts)
                {
                    db.Comments.Remove(cmnt);
                }
                db.SaveChanges();
                return RedirectToAction("questionDetails", "Question", new { id = id });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult editAnswer(int id, int ansID)
        {
            if (Session["username"] != null)
            {
                var answer = db.Answers.Single(x => x.answer_ID == ansID && x.question_ID == id);
                if (answer == null)
                {
                    return RedirectToAction("questionDetails", "Question", new { id = id });
                }
                else
                {
                    Profile pf = db.Profiles.Single(x => x.creator_ID == answer.creator_ID);
                    if (!pf.username.Equals(Session["username"]))
                    {
                        return RedirectToAction("questionDetails", "Question", new { id = id });
                    }
                    else
                    {
                        Answer ans = new Answer();
                        ans.answer_ID = ansID;
                        ans.question_ID = id;
                        ans.text = answer.text;
                        ans.imagePath = answer.imagePath;
                        return View(ans);
                    }
                }
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult SubmitEditAnswer(int id,int ansID,Answer model, HttpPostedFileBase image)
        {
            var answer = db.Answers.Single(x => x.answer_ID == ansID && x.question_ID == id);
            if(answer == null)
            {
                return RedirectToAction("questionDetails", "Question", new { id = id });
            }
            else
            { Profile pf = db.Profiles.Single(x => x.creator_ID == answer.creator_ID);
                if (!pf.username.Equals(Session["username"]))
                {
                   return  RedirectToAction("questionDetails", "Question", new { id = id });
                }
                else
                {
                    string filename = "";
                    if(model.text==null)
                        return RedirectToAction("questionDetails", "Question", new { id = id });
                    answer.text = model.text;
                    if (image != null)
                    {
                        filename = Path.GetFileNameWithoutExtension(image.FileName);
                        string extension = Path.GetExtension(image.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        answer.imagePath = "~/uploads/" + filename;
                        string path = Path.Combine(Server.MapPath("~/uploads/"), filename);
                        image.SaveAs(path);
                    }
                    db.SaveChanges();
                   return  RedirectToAction("questionDetails", "Question", new { id = id });

                }
                
            }
        }
        public ActionResult searchQuestions(string search,int nbPages=1)
        {
            string searchQuest = "";
            if (Session["searched"] == null && search.Length>0)
                Session["searched"] = search;

            if (Session["searched"] != null && search !=null) 
                Session["searched"] = search;
            if (Session["searched"] != null) {
                searchQuest = Session["searched"].ToString();
            }
                FlowEntities db = new FlowEntities();
                var questions = (from quest in db.Questions where quest.title.Contains(searchQuest) select quest).ToList();
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
        public ActionResult tagDetails(int nbPages, string tag)
        {
            var res= (from question in db.Questions join tagID in db.tag_question on question.question_ID equals tagID.question_ID join tagText in db.Tags.Where(x => x.tagText == tag) on tagID.tag_ID equals tagText.tag_ID select question);
       
            var questionList = res.OrderByDescending(x => x.date).Skip((nbPages - 1) * 6).Take(6);
            List<QuestionTagView> result = new List<QuestionTagView>();
            foreach (var quest in questionList)
            {
                QuestionTagView tagView = new QuestionTagView();
                tagView.question_ID = quest.question_ID;
                tagView.title = quest.title;
                tagView.body = quest.body;
                tagView.date = quest.date;
                var reso = (from tagId in db.tag_question.Where(x => x.question_ID == quest.question_ID) join tagi in db.Tags on tagId.tag_ID equals tagi.tag_ID select tagi.tagText).ToList();
                tagView.tag = reso;
                List<int> nbAnswers = (from answer in db.Answers.Where(x => x.question_ID == quest.question_ID) select answer.answer_ID).ToList();
                tagView.nbAnswers = nbAnswers.Count();
                var correct = db.Answers.Where(x => x.question_ID == quest.question_ID).Where(x => x.isApproved == true);
                if (correct.Count() != 0)
                    tagView.isCorrect = true;
                else
                    tagView.isCorrect = false;
                var name = db.Profiles.Single(x => x.creator_ID == quest.creator_ID);
                tagView.creator.username = name.username;
                tagView.nbPages = (int)Math.Ceiling((double)res.Count() / 6);
                tagView.pageIndxer = nbPages;
                result.Add(tagView);
            }
            return View(result);
        }

        public ActionResult searchTags(string searchtg,int nbPages=1)
        {
            string searchTag = "";
            if (Session["searchedTags"] == null && searchtg.Length > 0)
                Session["searchedTags"] = searchtg;

            if (Session["searchedTags"] != null && searchtg != null)
                Session["searchedTags"] = searchtg;
            if (Session["searchedTags"] != null)
            {
                searchTag = Session["searchedTags"].ToString();
            }
            FlowEntities db = new FlowEntities();
            var tags = (from tag in db.Tags where tag.tagText.Contains(searchTag) select tag).ToList();
            List<tagModel> result = new List<tagModel>();
            foreach (var tag in tags)
            {
                tagModel tagModel = new tagModel();
                tagModel.tag_ID = tag.tag_ID;
                tagModel.text = tag.tagText;
                tagModel.nbUsedInQuest = (db.tag_question.Where(x => x.tag_ID == tag.tag_ID)).Count();
                tagModel.nbPages = (int)Math.Ceiling((double)tags.Count() / 24);
                tagModel.pageIndex = nbPages;
                result.Add(tagModel);
            }
            List<tagModel> resultList = result.OrderByDescending(x => x.nbUsedInQuest).Skip((nbPages - 1) * 24).Take(24).ToList();
            return View(resultList);
        }

        public ActionResult filter(string university,string asker, int nbPages=1)
        {
            string searchUni = "";
            string searchAsker = "";
            if ( Session["searchedAsk"] == null && asker.Length > 0 || Session["searchedUni"] == null && university.Length > 0)
            {
                Session["searchedUni"] = university;
                Session["searchedAsk"] = asker;

            }
            if (Session["searchedUni"] != null && university != null || Session["searchedAsk"] != null && asker != null)
            {
                Session["searchedUni"] = university;
                Session["searchedAsk"] = asker;
            }
            if (Session["searchedUni"] != null || Session["searchedAsk"]!=null)
            {
                searchUni = Session["searchedUni"].ToString();
                searchAsker = Session["searchedAsk"].ToString();
            }
            FlowEntities db = new FlowEntities();
            List<Question> filteredList;
            var resNames = (from uni in db.Universities select uni.Name).ToList();
        
            filteredList = (from qus in db.Questions join prof in db.Profiles.Where(x => x.university_ID == (db.Universities.FirstOrDefault(y => y.Name == searchUni).university_ID) && x.username==searchAsker ) on qus.creator_ID equals prof.creator_ID select qus).ToList();
            var questionList = filteredList.OrderByDescending(x => x.date).Skip((nbPages - 1) * 6).Take(6);
            List<QuestionTagView> result = new List<QuestionTagView>();
            foreach (var quest in questionList)
            {
                QuestionTagView tagView = new QuestionTagView();
                tagView.question_ID = quest.question_ID;
                tagView.title = quest.title;
                tagView.body = quest.body;
                tagView.date = quest.date;
                var res = (from tagId in db.tag_question.Where(x => x.question_ID == quest.question_ID) join tagi in db.Tags on tagId.tag_ID equals tagi.tag_ID select tagi.tagText).ToList();
                tagView.tag = res;
                List<int> nbAnswers = new List<int>();
                nbAnswers = (from answer in db.Answers.Where(x => x.question_ID == quest.question_ID) select answer.answer_ID).ToList();
                tagView.nbAnswers = nbAnswers.Count();
                var correct1 = db.Answers.Where(x => x.question_ID == quest.question_ID).Where(x => x.isApproved == true);
                if (correct1.Count() != 0)
                    tagView.isCorrect = true;
                else
                    tagView.isCorrect = false;
                var name = db.Profiles.Single(x => x.creator_ID == quest.creator_ID);
                tagView.universities = resNames;
                tagView.creator.username = name.username;
                tagView.nbPages = (int)Math.Ceiling((double)filteredList.Count() / 6);
                tagView.pageIndxer = nbPages;
                result.Add(tagView);

            }
                return View(result);

        }

    }
}