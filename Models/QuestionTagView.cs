using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverFlow.Models
{
    public class QuestionTagView
    {
        FlowEntities db = new FlowEntities();
         public QuestionTagView()
        {

        }
        
        public int question_ID { get; set; }
        [Required]
        [StringLength(maximumLength:255,ErrorMessage ="The title should be formed at least from 30 charecter",MinimumLength =30)]
        public string title { get; set; }
        [Required]
        [StringLength(5000000,ErrorMessage ="The body should be form at least from 255 charcter (one line)",MinimumLength =255)]
        public string body { get; set; }

        public string imagePath { get; set; }
        [Required(ErrorMessage ="Cannot be empty")]
        public List<string> tag { get; set; }

        public List<Comment> questionComments { get; set; }
        public List<Answer> answers { get; set; }
        public List<List<Comment>>answersComments { get; set; } = new List<List<Comment>>();

        public string date { get; set; }

        public int nbAnswers { get; set; }
        public bool isCorrect { get; set; }

        public int nbPages { get; set; }

        public int pageIndxer { get;set; }

        public HttpPostedFileBase imageFile { get; set; }

        public Profile creator { get; set; } = new Profile();
        public virtual Profile GetProfile(int? id)
        {
            Profile pf = new Profile();
            var res = db.Profiles.Single(x => x.creator_ID == id);
            pf = res;
            return pf;

        }

        public string GetDivColor(bool correct , int nb)
        {
            string result = "";
            if (correct == true && nb > 0)
                result = "lightgreen";
            else if (correct == false && nb > 0)
                result = "lightyellow";
            else
                result = "white";
            return result;
        }

        
    }


}