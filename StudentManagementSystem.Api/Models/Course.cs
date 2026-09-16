using System.Text.Json.Serialization;
namespace StudentManagementSystem.Api.Models
{
    public class Course
    {
        public int CourseId {  get; set; }
        public string? CourseName {  get; set; }
        public string? CourseDuration {  get; set; }
        public decimal CourseFee {  get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
