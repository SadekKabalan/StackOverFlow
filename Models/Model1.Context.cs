﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StackOverFlow.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FlowEntities : DbContext
    {
        public FlowEntities()
            : base("name=FlowEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Factulty> Factulties { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<tag_question> tag_question { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<uni_fact_branch> uni_fact_branch { get; set; }
        public virtual DbSet<University> Universities { get; set; }
    }
}
