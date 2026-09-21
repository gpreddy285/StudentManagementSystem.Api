using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Api.Data;
using StudentManagementSystem.Api.Models;

namespace StudentManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _context.Students.Include(c=>c.Teacher).FirstOrDefault(c=>c.StudentId==id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id,Student student)
        {
            var existing = _context.Students.Find(id);
            if(existing == null)
            {
                return NotFound();
            }
            existing.StudentName = student.StudentName;
            existing.StudentAge = student.StudentAge;
            existing.StudentEmail= student.StudentEmail;
            existing.CourseId = student.CourseId;
            existing.TeacherId = student.TeacherId;
            _context.SaveChanges();
            return Ok(existing);
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var student= _context.Students.Find(id);
            if(student==null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(student);
        }
    }
}
