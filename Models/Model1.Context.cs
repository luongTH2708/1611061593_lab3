﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _1611061593_lab3.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ThucHanhTMDTEntities : DbContext
    {
        public ThucHanhTMDTEntities()
            : base("name=ThucHanhTMDTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<View_Article> View_Article { get; set; }
        public virtual DbSet<View_Category> View_Category { get; set; }
        public virtual DbSet<View_News> View_News { get; set; }
        public virtual DbSet<View_news_cate_alias> View_news_cate_alias { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<News> News { get; set; }
    }
}
