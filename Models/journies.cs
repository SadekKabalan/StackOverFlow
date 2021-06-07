using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow.Models
{
    public class journies
    {
        FlowEntities db = new FlowEntities();
        public int journey_ID { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string username { get; set; }
        public int creator_ID { get; set; }
        public int nbPages {  get; set; }
        public int pageIndxer { get; set; }

        public string date { get; set; }
        public List<Comment> journeyComments { get; set; }
        public virtual Profile GetProfile(int? id)
        {
            Profile pf = new Profile();
            var res = db.Profiles.Single(x => x.creator_ID == id);
            pf = res;
            return pf;

        }
    }
}