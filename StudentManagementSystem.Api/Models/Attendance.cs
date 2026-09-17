using System.Text.Json.Serialization;

namespace StudentManagementSystem.Api.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        public int StudentId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string AttendanceStatus { get; set; }
        

        public Student? Student { get; set; }
    }
}
