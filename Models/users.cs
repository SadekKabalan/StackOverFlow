using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow.Models
{
    public class users
    {
        FlowEntities db = new FlowEntities();
        public int creator_ID { get; set; }
        public string username { get; set; }
        public int nbQuestions { get; set; }
        public int nbAnswers { get; set; }
        public int nbPages { get; set; }
        public int pageIndex { get; set; }

        public List<Question> questions { get; set; }
        public virtual Profile GetProfile(int? id)
        {
            Profile pf = new Profile();
            var res = db.Profiles.Single(x => x.creator_ID == id);
            pf = res;
            return pf;

        }
        public virtual University GetUniversity(int? id)
        {
            University uni = new University();
            var res = db.Universities.Single(x => x.university_ID == id);
            uni = res;
            return uni;
        }
    }
}
