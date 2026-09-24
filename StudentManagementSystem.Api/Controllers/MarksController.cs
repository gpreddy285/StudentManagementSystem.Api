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
    public class MarksController : ControllerBase
    {
        private readonly StudentDbContext _context;
        public MarksController(StudentDbContext context)
        {
            _context=context;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,Teacher,Student")]
        public IActionResult GetMarks()
        {
            var marks = _context.Marks.Include(m => m.Student).ToList();
            return Ok(marks);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,Teacher")]
        public IActionResult CreateMarks(Mark mark)
        {
            _context.Marks.Add(mark);
            _context.SaveChanges();
            return Ok(mark);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher,Student")]

        public IActionResult GetMark(int id)
        {
            var mark = _context.Marks.Find(id);

            if (mark == null)
            {
                return NotFound();
            }

            return Ok(mark);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher")]

        public IActionResult UpdateMark(int id, Mark mark)
        {
            var existingMark = _context.Marks.Find(id);

            if (existingMark == null)
            {
                return NotFound();
            }

            existingMark.StudentId = mark.StudentId;
            existingMark.SubjectName = mark.SubjectName;
            existingMark.ExamType = mark.ExamType;
            existingMark.Marks = mark.Marks;

            _context.SaveChanges();

            return Ok(existingMark);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher")]

        public IActionResult DeleteMark(int id)
        {
            var mark = _context.Marks.Find(id);

            if (mark == null)
            {
                return NotFound();
            }

            _context.Marks.Remove(mark);
            _context.SaveChanges();

            return Ok(mark);
        }

    }
}
