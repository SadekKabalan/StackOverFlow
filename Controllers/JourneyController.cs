using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class JourneyController : Controller
    {
        // GET: Journey
        FlowEntities db = new FlowEntities();
        public ActionResult journeyDetails(int id ,int pageIndxer =1 )
        {
            var journey = db.Journeys.Single(x => x.journey_ID == id);
            var journeyComments = db.Comments.Where(x => x.journey_ID == id).ToList();
            journies journies = new journies();
            journies.journey_ID= journey.journey_ID;
            journies.title = journey.title;
            journies.body = journey.body;
            journies.date = journey.date;
            var name = db.Profiles.Single(x => x.creator_ID == journey.creator_ID);
            journies.username = name.username;
            journies.creator_ID = name.creator_ID;
            journies.pageIndxer = pageIndxer;
            journies.journeyComments = journeyComments;
            return View(journies);
        }

        public ActionResult addJourney()
        {
            if (Session["username"] != null)
                return View();
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult Submit(Journey model)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                Journey jour = new Journey();
               
                    jour.title = model.title;
              
               
                    jour.body = model.body;
                
               
                jour.date = DateTime.Now.ToString();
                jour.creator_ID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                db.Journeys.Add(jour);
               
                    db.SaveChanges();
                    return RedirectToAction("Journey", "Home");
                }
              
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult AddCommentQuestion(int id, string cmnt)
        {
            if (Session["username"] != null)
            {
                string user = Session["username"].ToString();
                Comment comment = new Comment();
                comment.creator_ID = db.Profiles.Single(x => x.username.Equals(user)).creator_ID;
                if (cmnt.Equals("") && cmnt != null)
                    return RedirectToAction("journeyDetails", "Journey", new { id = id  });
                comment.text = cmnt;
                comment.journey_ID = id;
                comment.date = DateTime.Now.ToString();
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("journeyDetails", "Journey", new { id = id });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult edit(int id)
        {
            if (Session["username"] != null)
            {
                journies journey = new journies();
                var res = db.Journeys.Single(x => x.journey_ID == id);
                journey.journey_ID = id;
                journey.title = res.title;
                journey.body = res.body;
                return View(journey);
            }
            else
                return RedirectToAction("Login", "Profile");
        }



        public ActionResult deleteJourney(int id, int pageIndxer)
        {
            if (Session["username"] != null)
            {
                Journey journey = db.Journeys.Single(x => x.journey_ID == id);
                db.Journeys.Remove(journey);
                var cmnts = db.Comments.Where(x => x.journey_ID == id).ToList();
                foreach (var cmnt in cmnts)
                {
                    db.Comments.Remove(cmnt);
                }
                db.SaveChanges();
                return RedirectToAction("Journey", "Home", new { nbPages = pageIndxer });
            }
            else
                return RedirectToAction("Login", "Profile");
        }

        public ActionResult SubmitEdit(int id,journies model)
        {
            if (Session["username"] != null)
            {
                var res = (from tagText in db.Tags select tagText.tagText).ToList();

                var journey = db.Journeys.Single(x => x.journey_ID == id);
                if (model.title != null)
                    journey.title = model.title;
                else
                    return RedirectToAction("Questions", "Home");
                if (model.body != null)
                    journey.body = model.body;
                else
                    return RedirectToAction("Questions", "Home");
                db.SaveChanges();

                return RedirectToAction("journeyDetails", "Journey", new { id = id });
            }
            else
                return RedirectToAction("Login", "Profile");
        }
    }
    
}