using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Api.Data;
using StudentManagementSystem.Api.Models;

namespace StudentManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public AttendancesController(StudentDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,Teacher,Student")]
        public IActionResult GetAttendances()
        {
            var attendances = _context.Attendances.Include(a => a.Student).ToList();
            return Ok(attendances);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher")]
        public IActionResult CreateAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok(attendance);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher,Student")]

        public IActionResult GetAttendance(int id)
        {
            var attendance = _context.Attendances.Include(a => a.Student).FirstOrDefault(a => a.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher")]


        public IActionResult UpdateAttendance(int id, Attendance attendance)
        {
            var existingAttendance = _context.Attendances.Find(id);
            if (existingAttendance == null)
            {
                return NotFound();
            }
            existingAttendance.StudentId = attendance.StudentId;
            existingAttendance.AttendanceDate = attendance.AttendanceDate;
            existingAttendance.AttendanceStatus = attendance.AttendanceStatus;
            _context.SaveChanges();
            return Ok(existingAttendance);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Teacher")]

        public IActionResult DeleteAttendance(int id)
        {
            var attendance = _context.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(attendance);
        }
    }
}