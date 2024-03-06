namespace Practice7.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Practice7.Models.AppDbContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Practice7.Models.AppDbContex context)
        {
            context.Users.AddOrUpdate(x => x.UserId, new Models.User()
            {
                UserId = 1,
                UserName = "User",
                Password = "1234",
                Email = "user@gmail.com",
                Roles = "User"
            },
             new Models.User()
             {
                 UserId = 2,
                 UserName = "Admin",
                 Password = "1234",
                 Email = "admin@gmail.com",
                 Roles = "Admin"
             }
            );
            context.Courses.AddOrUpdate(x => x.CourseId, new Models.Course()
            {
               
                CourseName = "C#",
                CourseHour = 1234,
               
            },
            new Models.Course()
            {
                CourseName = "DD",
                CourseHour = 1234,
            }
           );
        }
    }
}
