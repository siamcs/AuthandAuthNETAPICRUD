using Practice7.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Practice7.Controllers
{
    public class StudentController : ApiController
    {
        AppDbContex db=new AppDbContex();

        [Authorize(Roles ="User,Admin")]
        public System.Object GetStudents()
        {
            var r = db.Students.ToList();
            return r.OrderBy(x=>x.StudentId);
        }

        [Authorize(Roles = "User,Admin")]
        public IHttpActionResult GetStudent(int id)
        {
            var st=(from a in db.Students where a.StudentId==id 
                    
                    select new
                    {
                        a.StudentId,
                        a.StudentName,
                        a.StudentNo,
                        a.BirthDate,
                        a.IsRegular,
                        a.ImageUrl,
                    })
                .FirstOrDefault();

            var stitem = (from a in db.CurriculumItems join b in db.Courses on a.CourseId equals b.CourseId
                      where a.StudentId == id

                      select new
                      {
                          a.StudentId,
                          a.CourseId,
                          b.CourseName,
                          b.CourseHour,
                          
                      })
               .ToList();
            return Ok(new { st, stitem });
        }


        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteStudent(int id)
        {
            var st = db.Students.Find(id);
            var stitem=db.CurriculumItems.Where(x=>x.CourseId==id);
            foreach(var item in stitem)
            {
                db.CurriculumItems.Remove(item);
            }
            db.Students.Remove(st);
            db.SaveChanges();
            return Ok("Deletd");
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostStudent( StudentRequest request)
        {
           if(request.Student == null)
            {
                return BadRequest("Data missing");

            }
            Student obj = request.Student;
            byte[] imageFile=request.ImageFile;
            if(imageFile !=null && imageFile.Length > 0 ) 
            
            { 
                string Fn=Guid.NewGuid().ToString()+".jpg";
                string FP = Path.Combine("~/Images/", Fn);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(FP), imageFile);
                obj.ImageUrl = FP;

            }
            Student student = new Student
            {
                StudentNo = obj.StudentNo,
                StudentName = obj.StudentName,
                BirthDate = obj.BirthDate,
                IsRegular = obj.IsRegular,
                ImageUrl = obj.ImageUrl,
            };

            db.Students.Add(student);
            db.SaveChanges();

            var stobj=db.Students.FirstOrDefault(c=>c.StudentNo==obj.StudentNo);
            if( stobj != null && obj.CurriculumItems !=null ) 
            {
            foreach(var item  in obj.CurriculumItems )
                {
                    CurriculumItem curriculumItem = new CurriculumItem
                    {
                         StudentId = stobj.StudentId,
                        CourseId = item.CourseId,  
                    };
                    db.CurriculumItems.Add(curriculumItem);
                }
            }
            db.SaveChanges();
            return Ok("Saved");
        }

        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutStudent(int id, StudentRequest request)
        {
            Student student=db.Students.FirstOrDefault(x=>x.StudentId==id);

            if ( id !=request.Student.StudentId)
            {
                return BadRequest();

            }

            if (student == null)
            {
                return NotFound();

            }

            if (request.Student == null)
            {
                return BadRequest("Data missing");

            }

            Student obj = request.Student;

            byte[] imageFile = request.ImageFile;
            if (imageFile != null && imageFile.Length > 0)
            {
                string Fn = Guid.NewGuid().ToString() + ".jpg";
                string FP = Path.Combine("~/Images/", Fn);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(FP), imageFile);
                obj.ImageUrl = FP;

            }

            student.StudentNo = obj.StudentNo;
            student.StudentName = obj.StudentName;
            student.BirthDate = obj.BirthDate;
            student.IsRegular = obj.IsRegular;
            student.ImageUrl = obj.ImageUrl;

            var exis = db.CurriculumItems.Where(x => x.StudentId == student.StudentId);
            db.CurriculumItems.RemoveRange(exis);
          
            if ( obj.CurriculumItems != null)
            {
                foreach (var item in obj.CurriculumItems)
                {
                    CurriculumItem curriculumItem = new CurriculumItem
                    {
                        StudentId = student.StudentId,
                        CourseId = item.CourseId,
                    };
                    db.CurriculumItems.Add(curriculumItem);
                }
            }
            db.SaveChanges();
            return Ok("Updated");
        }
    }
}
