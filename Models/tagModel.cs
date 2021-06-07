using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow.Models
{
    public class tagModel
    {
        public int tag_ID { get; set; }
        public string text { get; set; }
        public int nbUsedInQuest { get; set; }
        public int nbPages { get; set; }
        public int pageIndex { get; set; }
    }
}