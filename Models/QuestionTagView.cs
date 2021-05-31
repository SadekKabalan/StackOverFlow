using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow.Models
{
    public class QuestionTagView
    {
         public QuestionTagView()
        {

        }

        public int question_ID { get; set; }
        public string title { get; set; }
        public string body { get; set; }

        public string imagePath { get; set; }
        
        public List<string> tag { get; set; }

        public List<Comment> questionComments { get; set; }
        public List<Answer> answers { get; set; }
        public List<List<Comment>> answersComments { get; set; }

        public string date { get; set; }

        public int nbAnswers { get; set; }
        public bool isCorrect { get; set; }

        public int nbPages { get; set; }

        public int pageIndxer { get;set; }

        public Profile creator { get; set; } = new Profile();
    }
}