namespace Practice7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        CourseHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.CurriculumItems",
                c => new
                    {
                        CurriculumItemId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CurriculumItemId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        StudentNo = c.String(),
                        IsRegular = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurriculumItems", "StudentId", "dbo.Students");
            DropForeignKey("dbo.CurriculumItems", "CourseId", "dbo.Courses");
            DropIndex("dbo.CurriculumItems", new[] { "CourseId" });
            DropIndex("dbo.CurriculumItems", new[] { "StudentId" });
            DropTable("dbo.Users");
            DropTable("dbo.Students");
            DropTable("dbo.CurriculumItems");
            DropTable("dbo.Courses");
        }
    }
}
