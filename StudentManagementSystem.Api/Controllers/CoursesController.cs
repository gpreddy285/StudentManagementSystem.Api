using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Api.Data;
using StudentManagementSystem.Api.Models;

namespace StudentManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public CoursesController(StudentDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses=_context.Courses.ToList();
            return Ok(courses);
        }

        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Ok(course);
        }
        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var course = _context.Courses.Include(c=>c.Students).FirstOrDefault(c=>c.CourseId==id);
            if(course==null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id,Course course)
        {
            var existing = _context.Courses.Find(id);
            if(existing==null)
            {
                return NotFound();
            }
            existing.CourseName = course.CourseName;
            existing.CourseDuration = course.CourseDuration;
            existing.CourseFee = course.CourseFee;
            _context.SaveChanges();
            return Ok(course);
        }
        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            var course=_context.Courses.Find(id);
            if( course==null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return Ok(course);
                
        }

    }
}
