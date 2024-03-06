using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practice7.Models
{
    public class AppDbContex:DbContext
    {
        public AppDbContex():base("AppDbContex") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CurriculumItem> CurriculumItems { get; set; }
      

    }
}