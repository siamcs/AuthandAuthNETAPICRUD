using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice7.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentNo { get; set; }
        public bool IsRegular { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BirthDate { get; set;}
        public virtual ICollection<CurriculumItem> CurriculumItems { get; set; }
    }

    public class CurriculumItem
    {
        public int CurriculumItemId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseHour { get; set; }
       
    }
    public class StudentRequest
    {
        public Student Student {get; set;}
        public string ImageFileName {get; set;}
        public byte[] ImageFile  {get;set;}

    }
}