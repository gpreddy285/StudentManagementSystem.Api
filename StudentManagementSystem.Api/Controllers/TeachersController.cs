using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Api.Data;
using StudentManagementSystem.Api.Models;

namespace StudentManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public TeachersController(StudentDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teacher=_context.Teachers.ToList();
            return Ok(teacher);
        }
        [HttpGet("{id}")]
        public IActionResult GetTeacher(int id)
        {
            var teacher=_context.Teachers.Find(id);
            if(teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }
        [HttpPost]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return Ok(teacher);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id,Teacher teacher)
        {
            var existing = _context.Teachers.Find(id);
            if(existing==null)
            {
                return NotFound();
            }
            existing.TeacherName = teacher.TeacherName;
            existing.TeacherEmail = teacher.TeacherEmail;
            existing.CourseId = teacher.CourseId;
            _context.SaveChanges();
            return Ok(existing);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Find(id);

            if (teacher == null)
            {
                return NotFound();
            }
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return Ok(teacher);
        }

    }
}
