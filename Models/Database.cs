using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StackOverFlow.Models
{
    public class University
    {   [Key]
        public int university_ID { get; set; }
        public String name { get; set; }

    }

    public class Branch
    {   [Key]
        public int branch_ID { get; set; }
        public String number { get; set; }
    }

    public class Faculty
    {   [Key]
        public int faculty_ID { get; set; }
        public String name { get; set; }
    }

    public class uni_fac_branch
    {   [Key]
        public int id { get; set; }
        [ForeignKey("university_ID")]
        public virtual University University { get; set; }
        public int? university_ID { get; set; }

        [ForeignKey("branch_ID")]
        public virtual Branch Branch { get; set; }
        public int? branch_ID { get; set; }

        [ForeignKey("faculty_ID")]
        public virtual Faculty Faculty { get; set; }
        public int? faculty_ID { get; set; }

    }

     
    public class Profile
    {   [Key]
        public int creator_ID { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }
        public String major { get; set; }

        [ForeignKey("university_ID")]
        public virtual University University { get; set; }
        public int? university_ID { get; set; }

    }

    public class Question
    {   [Key]
        public int question_ID { get; set; }
        public String text { get; set; }
        public DateTime dateTime { get; set; }
        public String title { get; set; }
        public String imagePath { get; set; }
        [ForeignKey("creator_ID")]
        public virtual Profile Profile { get; set; }
        public int? creator_ID { get; set; }
    }

    public class Answer
    {   [Key]
        public int answer_ID { get; set; }
        [ForeignKey("question_ID")]
        public virtual Question Question { get; set; }
        public int? question_ID { get; set; }
        public String text { get; set; }
        public String imagePath { get; set; }
        public bool isCorrect { get; set; }
        public DateTime dateTime { get; set; }
        [ForeignKey("creator_ID")]
        public virtual Profile Profile { get; set; }
        public int? creator_ID { get; set; }
    }

    public class Comment
    {   [Key]
        public int comment_ID { get; set; }
        public String text { get; set; }
        public DateTime datetime { get; set; }
        [ForeignKey("creator_ID")]
        public virtual Profile Profile { get; set; }
        public int? creator_ID { get; set; }
        [ForeignKey("question_ID")]
        public virtual Question Question { get; set; }
        public int? question_ID { get; set; }
        [ForeignKey("answer_ID")]
        public virtual Answer Answer { get; set; }
        public int? answer_ID { get; set; }

    }

    public class Tag
    {   [Key]
        public int tag_ID { get; set; }
        public String text { get; set; }
    }

    public class tag_question
    {   [Key]
        public int id { get; set; }
        [ForeignKey("question_ID")]
        public virtual Question Question { get; set; }
        public int? question_ID { get; set; }
        [ForeignKey("tag_ID")]
        public virtual Tag Tag { get; set; }
        public int? tag_ID { get; set; }

    }

    public class StackDBContext : DbContext
    {
        public StackDBContext()
        { }
        public DbSet<University> Universities { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<uni_fac_branch> uni_Fac_Branches { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<tag_question> Tag_Questions{ get; set; }


    }

}