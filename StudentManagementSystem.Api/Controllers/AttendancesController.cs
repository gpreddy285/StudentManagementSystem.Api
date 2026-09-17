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
        public IActionResult GetAttendances()
        {
            var attendances = _context.Attendances.Include(a => a.Student).ToList();
            return Ok(attendances);
        }
        [HttpPost]
        public IActionResult CreateAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok(attendance);
        }
        [HttpGet("{id}")]
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